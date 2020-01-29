// Author: Orlys
// Github: https://github.com/Orlys

namespace Yuuna.Contracts.Interaction
{
    /// <summary>
    /// 提供多個預設靜態情緒結構實體。
    /// </summary>
    public static class Moods
    {
        //public static Mood Random(Mood first, Mood second, params Mood[] rest)
        //{
        //    var seed = Guid.NewGuid().GetHashCode();
        //    var random = new Random(seed);
        //    if (rest is Mood[] && rest.Length > 0)
        //    {
        //        var rnd = random.Next(0, rest.Length + 2);
        //        if (rnd == 0) return first;
        //        if (rnd == 1) return second;

        //        return rest[rnd - 2];
        //    }
        //    else
        //    {
        //        var v = random.Next(0, 2);
        //        if (v == 0)
        //            return first;
        //        return second;
        //    }
        //}

        /// <summary>
        /// 混亂。
        /// </summary>
        public static readonly Mood Confuse = new Mood(nameof(Confuse));

        /// <summary>
        /// 快樂。
        /// </summary>
        public static readonly Mood Happy = new Mood(nameof(Happy));

        /// <summary>
        /// 一般。
        /// </summary>
        public static readonly Mood Normal = default;

        /// <summary>
        /// 難過。
        /// </summary>
        public static readonly Mood Sad = new Mood(nameof(Sad));

        //public static readonly Mood Angry = new Mood(nameof(Angry));
        //public static readonly Mood Tender = new Mood(nameof(Tender));
        //public static readonly Mood Excited = new Mood(nameof(Excited));
        //public static readonly Mood Scared = new Mood(nameof(Scared));
    }
}