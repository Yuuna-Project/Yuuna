namespace Yuuna
{
    using System.Collections.Generic;

    public abstract class Content : IContent
    {
        public Content()
        {
            this.Synonyms = new List<Word>();
            this.Vector = new List<Value>();
        }

        public List<Word> Synonyms { get; }

        public List<Value> Vector { get; }
    }
}
