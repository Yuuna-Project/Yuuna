// Author: Yuuna-Project@Orlys
// Github: github.com/Orlys
// Contact: orlys@yuuna-project.com

namespace Yuuna.Common.Artisan.Cascade
{
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// 初始級聯器。 將繼承自這個級聯器的類別標示為整個級聯模式中的第一個級聯類型，用以連結下一個級聯物件。這是 <see langword="abstract"/> 類別。
    /// </summary>
    /// <typeparam name="TNext">下個級聯類型。</typeparam>
    public abstract class Initial<TNext, T> : Cascadable
        where TNext : Cascadable, new()
    {
        protected Initial()
        {
        }

        /// <summary>
        /// 將資料存入內部工作階段(Session)物件中後建立並傳回下一個 <see cref="TNext"/> 類型的新物件實體。
        /// </summary>
        /// <param name="data">資料。</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected TNext MoveNext(T data)
        {
            var session = new Session() as IInternalSession;
            session.Completed += this.OnCompleted;
            session.Store(data);
            return new TNext { Session = session };
        }

        protected abstract void OnCompleted(Queue<object> session);
    }

    /// <summary>
    /// 初始級聯器。 將繼承自這個級聯器的類別標示為整個級聯模式中的第一個級聯類型，用以連結下一個級聯物件。這是 <see langword="abstract"/> 類別。
    /// </summary>
    /// <typeparam name="TNext">下個級聯類型。</typeparam>
    public abstract class Initial<TNext> : Cascadable
        where TNext : Cascadable, new()
    {
        protected Initial()
        {
        }

        /// <summary>
        /// 傳回下一個 <see cref="TNext"/> 類型的新物件實體。
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected TNext MoveNext()
        {
            var session = new Session() as IInternalSession;
            session.Completed += this.OnCompleted;
            return new TNext { Session = session };
        }

        protected abstract void OnCompleted(Queue<object> session);
    }
}