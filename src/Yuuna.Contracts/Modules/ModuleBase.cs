// Author: Yuuna-Project@Orlys
// Github: github.com/Orlys
// Contact: orlys@yuuna-project.com

namespace Yuuna.Contracts.Modules
{
    using System;
    using System.Dynamic;

    using Yuuna.Common.Configuration;
    using Yuuna.Contracts.Patterns;
    using Yuuna.Contracts.Semantics;
    using Yuuna.Contracts.TextSegmention;

    public abstract class ModuleBase
    {
        private readonly ConfigProxy _configProxy;

        private bool _initialized;

        public Guid Id { get; }

        /// <summary>
        /// 中繼資料。
        /// </summary>
        public IModuleMetadata Metadata { get; }

        internal IRule Patterns { get; private set; }

        public ModuleBase()
        {
            var t = this.GetType();
            this.Id = t.GUID;// Guid.NewGuid();
            this.Metadata = ModuleMetadataAttribute.GetMetadata(t);
            this._configProxy = new ConfigProxy(this);
            this._configProxy.PropertyChanged += (sender, e) => (sender as ConfigProxy).Save();
        }

        /// <summary>
        /// 初始化模組
        /// </summary>
        /// <param name="textSegmenter">分詞器</param>
        /// <param name="groupManager">群組管理</param>
        internal void Initialize(ITextSegmenter textSegmenter, IGroupManager groupManager, out IRule patterns)
        {
            patterns = null;
            if (!this._initialized)
            {
                try
                {
                    var session = new ExpandoObject();

                    this.BeforeInitialize(this._configProxy, session);
                    patterns = new PatternFactory(this);
                    this.BuildPatterns(groupManager, patterns as IPatternBuilder, this._configProxy, session);
                    textSegmenter.Load(groupManager);
                    this.AfterInitialize(this._configProxy, session);
                    this._initialized = true;
                    this.Patterns = patterns;
                }
                catch (Exception e)
                {
                    throw new TypeInitializationException(this.GetType().FullName, e);
                }
            }
        }

        /// <summary>
        /// 在初始化後引發。
        /// </summary>
        protected virtual void AfterInitialize(dynamic config, dynamic session)
        {
        }

        /// <summary>
        /// 在初始化前引發。
        /// </summary>
        protected virtual void BeforeInitialize(dynamic config, dynamic session)
        {
        }

        /// <summary>
        /// 建立模式規則。
        /// </summary>
        /// <param name="g"></param>
        /// <param name="p"></param>
        /// <param name="config"></param>
        protected abstract void BuildPatterns(IGroupManager g, IPatternBuilder p, dynamic config, dynamic session);
    }
}