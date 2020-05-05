// Author: Yuuna-Project@Orlys
// Github: github.com/Orlys
// Contact: orlys@yuuna-project.com

namespace Yuuna.Contracts.Interaction
{
    using System;
    using System.ComponentModel;

    /// <summary>
    /// 情緒。
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct Mood : IEquatable<Mood>
    {
        private const string NormalString = "Normal";
        private readonly string _name;

        /// <summary>
        /// 取得是否為預設情緒。
        /// </summary>
        public bool IsNormal => this._name is null;

        /// <summary>
        /// 情緒名稱。
        /// </summary>
        public string Name
        {
            get
            {
                if (this.IsNormal)
                    return NormalString;
                return this._name;
            }
        }

        /// <summary>
        /// 建立新情緒結構實體。
        /// </summary>
        /// <param name="name">情緒的名稱。</param>
        public Mood(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            this._name = name;
        }

        public bool Equals(Mood other) => this.GetHashCode() == other.GetHashCode();

        public override int GetHashCode() => this.Name.GetHashCode();

        public override string ToString() => this.Name;
    }
}