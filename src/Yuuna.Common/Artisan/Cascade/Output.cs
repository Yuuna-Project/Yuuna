// Author: Yuuna-Project@Orlys
// Github: github.com/Orlys
// Contact: orlys@yuuna-project.com

namespace Yuuna.Common.Artisan.Cascade
{
    using System.Runtime.CompilerServices;

    /// <summary>
    /// 輸出級聯器。 將繼承自這個級聯器的類別標示為最後一個級聯物件。這是 <see langword="abstract"/> 類別。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Output<T> : Cascadable
    {
        protected Output()
        {
        }

        /// <summary>
        /// 將資料存入內部工作階段(Session)物件中，並通知所屬的級聯生成器已完成此次工作階段。
        /// </summary>
        /// <param name="data">資料。</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void MoveNext(T data)
        {
            this.Session.Store(data);
            this.Session.OnComplete();
        }
    }
}