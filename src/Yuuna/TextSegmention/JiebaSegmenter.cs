// Author: Yuuna-Project@Orlys
// Github: github.com/Orlys
// Contact: orlys@yuuna-project.com

namespace Yuuna.TextSegmention
{
    using JiebaNet.Segmenter;

    using System.Collections.Immutable;
    using System.Diagnostics;
    using System.Globalization;

    using Yuuna.Contracts.Semantics;
    using Yuuna.Contracts.TextSegmention;

    public sealed class JiebaTextSegmenter : ITextSegmenter
    {
        private readonly JiebaSegmenter _jieba;

        public CultureInfo Culture { get; }

        string ITextSegmenter.Name => "Jieba.Net";

        public JiebaTextSegmenter()
        {
            this._jieba = new JiebaSegmenter();
            this.Culture = CultureInfo.GetCultureInfo("zh-TW");
        }

        public IImmutableList<string> Cut(string text) => this._jieba.Cut(text, true, true).ToImmutableArray();

        public void Load(IGroupManager manager)
        {
            if (manager == null)
                return;

            foreach (var name in manager.Keys)
            {
                Debug.WriteLine($"name: {name}");
                foreach (var synonym in manager[name].ToImmutable())
                {
                    foreach (var w in synonym.ToImmutable())
                    {
                        this._jieba.AddWord(w);
                        Debug.WriteLine($"added: {w}");
                    }
                }
            }
        }

        public void Unload(IGroupManager manager)
        {
            if (manager == null)
                return;

            foreach (var name in manager.Keys)
            {
                Debug.WriteLine($"name: {name}");
                foreach (var synonym in manager[name].ToImmutable())
                {
                    foreach (var w in synonym.ToImmutable())
                    {
                        this._jieba.DeleteWord(w);
                        Debug.WriteLine($"deleted: {w}");
                    }
                }
            }
        }
    }
}