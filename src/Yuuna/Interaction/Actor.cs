// Author: Orlys
// Github: https://github.com/Orlys

namespace Yuuna
{
    using System;
    using System.Text;

    using Yuuna.Contracts.Optimization;
    using Yuuna.Contracts.TextSegmention;
    using Yuuna.Semantics;
    using Yuuna.TextSegmention;
    using Yuuna.Common.Utilities;
    using System.Linq;
    using Yuuna.Contracts.Interaction;
    using System.Diagnostics;
    using System.Collections.Generic;



    internal sealed class Antonym
    {
        public enum TypeKinds
        {
            Unknown = default,
            Positive,
            Negative,
            //Question
        }

        private struct Word
        {
            public Word(string value, TypeKinds condition)
            {
                this.Value = value;
                this.Condition = condition;
                this.Priority = value.Length;
            }
            internal string Value { get; }
            internal TypeKinds Condition { get; }
            internal int Priority { get; }
        }

        private readonly Word[] _words;

        public Antonym(string positive, string negative, string question)
        {
            this._words = new[]
            {
                new Word(positive, TypeKinds.Positive),
                new Word(negative, TypeKinds.Negative),
                //new Word(question, TypeKinds.Question)
            };
            Array.Sort(this._words, new Comparison<Word>((x, y) => y.Priority - x.Priority));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="positive"></param>
        /// <param name="negative"></param>
        public Antonym(string positive, string negative) : this(positive, negative, positive + negative)
        {
        }

        public bool Judge(string sentence, out TypeKinds condition)
        {
            foreach (var w in this._words)
            {
                if (sentence.Contains(w.Value))
                {
                    condition = w.Condition;
                    return true;
                }
            }
            condition = default;
            return false;
        } 
    }

    public class Actor
    {
        private IEnumerable<Response> _canResponses;
        private Antonym[] _antonyms;
        private ITextSegmenter _segmenter;
        private IStrategy _strategy;
        private IReadOnlyList<ModuleCoupler> _moduleProxies;
        private readonly Stack<Match> _session;
        private readonly object _lock = new object();
        private volatile bool _initialized;

        public Actor()
        {
            this._session = new Stack<Match>();
        }
        public Actor(ITextSegmenter segmenter, IEnumerable<Response> canResponses, IStrategy strategy, IReadOnlyList<ModuleCoupler> moduleProxies) : this()
        {
            this.Initialize(segmenter, canResponses, strategy, moduleProxies);
        }
        public void Initialize(ITextSegmenter segmenter, IEnumerable<Response> canResponses, IStrategy strategy, IReadOnlyList<ModuleCoupler> moduleProxies)
        {
            if (!this._initialized)
                lock (this._lock)
                {
                    if (this._initialized)
                        return;
                    this._segmenter = segmenter ?? throw new ArgumentNullException(nameof(segmenter));
                    this._canResponses = canResponses ?? throw new ArgumentNullException(nameof(canResponses));
                    this._strategy = strategy ?? new Strategy();

                    this._antonyms = new[]
                    {
                        new Antonym("是", "不是"),
                        new Antonym("對", "不對"),
                        new Antonym("是", "否"),
                    };

                    this._moduleProxies = moduleProxies;
                    foreach (var plugin in this._moduleProxies)
                    {
                        if (plugin.Initialize(this._segmenter, new GroupManager()))
                            Debug.WriteLine("已載入模組: " + plugin.Metadata.Name);
                    }
                    this._initialized = true;
                }
        }

        public bool Accept(string text, out Response response)
        {
            if (!this._initialized)
            {
                lock (this._lock)
                {
                    if (!this._initialized)
                    {
                        var t = this.GetType();
                        throw new TypeInitializationException(t.FullName, null);
                    }
                }
            }
            if (string.IsNullOrWhiteSpace(text))
                return false;

            response = this.OnAccept(text);
            return true;
        }

        protected virtual Response OnAccept(string text)
        {
            lock (this._lock)
            {
                var cutted = this._segmenter.Cut(text);
                //Debug.WriteLine($"來自分詞器 {this._segmenter.Name} 的分詞結果: [ {string.Join(", ", cutted)} ]");

                if (this._session.Count > 0)
                {
                    var single = this._session.Pop();

                    foreach (var antonym in this._antonyms)
                    {
                        if (antonym.Judge(text, out var type))
                        {
                            if (type.Equals(Antonym.TypeKinds.Positive))
                            {
                                this._moduleProxies.First(x=>x.Id.Equals(single.Pattern.Owner)).Patterns.TryGet(single.Pattern, out var bag);
                                var r = bag.Invoke(single);
                                this._session.Clear();
                                return r;
                            }
                            else if (type.Equals(Antonym.TypeKinds.Negative))
                            {
                                break;
                            }
                        }
                    }

                    if (this._session.Count == 0)
                    {
                        return this._canResponses.RandomTakeOne();
                    }
                    else
                    {
                        var sb = new StringBuilder("還是你是想");
                        var peek = this._session.Peek();
                        foreach (var g in peek.Pattern.ToImmutable())
                            sb.Append(g.RandomTakeOne().RandomTakeOne());
                        sb.Append("?");
                        return sb.ToString();
                    }
                }

                var k = this._moduleProxies.Select(x => x.Patterns).ToArray(); 
                var alternative = this._strategy.FindBest(k, cutted);
                //Debug.WriteLine(alternative.Status);
                switch (alternative.Status)
                {
                    case AlternativeStatus.Invalid:
                        return this._canResponses.RandomTakeOne();

                    case AlternativeStatus.Optimal:
                        {
                            var single = alternative.Matches[0];
                            this._moduleProxies.First(x=>x.Id.Equals(single.Pattern.Owner)).Patterns.TryGet(single.Pattern, out var bag);
                            var r = bag.Invoke(single);
                            return r;
                        }

                    case AlternativeStatus.Condition:
                        {
                            var sb = new StringBuilder();

                            sb.Append("你是想要");
                            foreach (var g in alternative.Matches[0].Pattern.ToImmutable())
                                sb.Append(g.RandomTakeOne().RandomTakeOne());
                            sb.Append("嗎?");

                            this._session.Push(alternative.Matches[0]);
                            return sb.ToString();
                        }

                    case AlternativeStatus.Proposition:
                        {
                            this._session.Push(alternative.Matches[1]);
                            goto case AlternativeStatus.Condition;
                        }

                    case AlternativeStatus.Paradox:
                        goto case AlternativeStatus.Proposition;

                    //case AlternativeStatus.NoModuleInstalled:
                    //    interactor.OnReceive((Moods.Sad, "無效的模組"));
                    //    break;

                    default:
                        throw new InvalidOperationException();


                }

            }
        }
    }
}