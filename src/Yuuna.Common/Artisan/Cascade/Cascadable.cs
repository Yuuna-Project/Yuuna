// Author: Yuuna-Project@Orlys
// Github: github.com/Orlys
// Contact: orlys@yuuna-project.com

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