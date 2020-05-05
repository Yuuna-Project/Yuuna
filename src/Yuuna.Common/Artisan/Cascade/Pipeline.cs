// Author: Yuuna-Project@Orlys
// Github: github.com/Orlys
// Contact: orlys@yuuna-project.com

namespace Yuuna.Common.Artisan.Cascade
{
    using System.Runtime.CompilerServices;

    /// <summary>
    /// 管道級聯器。 將繼承自這個級聯器的類別標示為整個級聯模式中的中間級級聯物件，用以連結下一個級聯物件。這是 <see langword="abstract"/> 類別。
    /// </summary>
    /// <typeparam name="TNext">下個級聯類型。</typeparam>
    /// <typeparam name="T"></typeparam>
    public abstract class Pipeline<TNext, T> : Cascadable
        where TNext : Cascadable, new()
    {
        protected Pipeline()
        {
        }

        /// <summary>
        /// 將資料存入內部工作階段(Session)物件中後建立並傳回下一個 <see cref="TNext"/> 類型的新物件實體。
        /// </summary>
        /// <param name="data">資料。</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected TNext MoveNext(T data)
        {
            this.Session.Store(data);
            return new TNext { Session = this.Session };
        }
    }
}