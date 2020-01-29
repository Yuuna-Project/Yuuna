// Author: Orlys
// Github: https://github.com/Orlys
namespace Yuuna.Common.Artisan.Cascade
{
    using System.ComponentModel;

    /// <summary>
    /// 提供物件基本的級聯能力。這是 <see langword="abstract"/> 類別。
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public abstract class Cascadable
    {
        internal IInternalSession Session;

        protected Cascadable()
        {
        }
    }
}