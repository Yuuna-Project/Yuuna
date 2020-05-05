// Author: Yuuna-Project@Orlys
// Github: github.com/Orlys
// Contact: orlys@yuuna-project.com

namespace Yuuna.Contracts.Interaction
{
    using System;
    using System.ComponentModel;
    using System.Reflection;

    /// <summary>
    /// 回應物件，表示插件回覆的資訊。
    /// </summary>
    [DefaultMember("Message")]
    public struct Response
    {
        /// <summary>
        /// 訊息。
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// 情緒。
        /// </summary>
        public Mood Mood { get; }

        /// <summary>
        /// 建立新回應物件實體。
        /// </summary>
        /// <param name="mood">情緒。</param>
        /// <param name="message">訊息。</param>
        public Response(Mood mood, string message)
        {
            this.Mood = mood;
            this.Message = message;
        }

        public static implicit operator Response(ValueTuple<Mood, string> full)
        {
            return new Response(full.Item1, full.Item2);
        }

        public static implicit operator Response(string message)
        {
            return new Response(Moods.Normal, message);
        }

        public static Response operator +(Response r, string s)
        {
            return new Response(r.Mood, r.Message + s);
        }

        public static Response operator +(string s, Response r)
        {
            return new Response(r.Mood, s + r.Message);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Deconstruct(out Mood mood, out string message)
        {
            mood = this.Mood;
            message = this.Message;
        }

        public override string ToString() => this.Message;
    }
}