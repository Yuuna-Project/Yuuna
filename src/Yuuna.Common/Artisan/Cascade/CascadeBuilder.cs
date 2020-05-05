// Author: Yuuna-Project@Orlys
// Github: github.com/Orlys
// Contact: orlys@yuuna-project.com

namespace Yuuna.Common.Artisan.Cascade
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 級聯生成器，用以建立完整的級聯生成器模式。此類別可用於替代 <see cref="Initial{TNext}"/> 類別。
    /// </summary>
    /// <typeparam name="TNext">下個級聯類型。</typeparam>
    public sealed class CascadeBuilder<TNext> : Initial<TNext>
        where TNext : Cascadable, new()
    {
        private readonly Action<Queue<object>> _onCompleted;

        private CascadeBuilder(Action<Queue<object>> onCompleted)
        {
            this._onCompleted = onCompleted;
        }

        /// <summary>
        /// 建立新的 <typeparamref name="TNext"/> 級聯器實體。
        /// </summary>
        /// <param name="onCompleted"></param>
        /// <returns></returns>
        public static TNext Build(Action<Queue<object>> onCompleted)
        {
            if (onCompleted is null)
                throw new ArgumentNullException(nameof(onCompleted));
            var init = new CascadeBuilder<TNext>(onCompleted);
            return init.MoveNext();
        }

        protected override void OnCompleted(Queue<object> session) => this._onCompleted(session);
    }
}