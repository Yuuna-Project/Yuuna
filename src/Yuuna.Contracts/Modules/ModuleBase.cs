// Author: Orlys
// Github: https://github.com/Orlys

namespace Yuuna.Contracts.Modules
{
    using System;
    using System.Collections.Immutable;
    using System.Runtime.Loader;
    using Yuuna.Common.Configuration;
    using Yuuna.Contracts.Optimization;
    using Yuuna.Contracts.Patterns;
    using Yuuna.Contracts.Semantics;
    using Yuuna.Contracts.TextSegmention;
    public abstract class ModuleBase 
    {
        /// <summary>
        /// 中繼資料。
        /// </summary> 
        public IModuleMetadata Metadata { get; }

        private readonly ConfigProxy _configProxy;

        public Guid Id { get; }

        public ModuleBase()
        {
            var t = this.GetType();
            this.Id = t.GUID;// Guid.NewGuid();
            this.Metadata = ModuleMetadataAttribute.GetMetadata(t);
            this._configProxy = new ConfigProxy(this);
            this._configProxy.PropertyChanged += (sender, e) => (sender as ConfigProxy).Save();
        }

        private bool _initialized;

        /// <summary>
        /// 初始化模組
        /// </summary>
        /// <param name="textSegmenter">分詞器</param>
        /// <param name="groupManager">群組管理</param>
        internal void Initialize(ITextSegmenter textSegmenter, IGroupManager groupManager, out IPatternSet patterns)
        {
            patterns = null;
            if (!this._initialized)
            {
                try
                {
                    this.BeforeInitialize(this._configProxy);
                    patterns = new PatternFactory(this);
                    this.BuildPatterns(groupManager, patterns as IPatternBuilder, this._configProxy);
                    textSegmenter.Load(groupManager);
                    this.AfterInitialize();
                    this._initialized = true;
                    this.Patterns = patterns;
                }
                catch (Exception e)
                {
                    throw new TypeInitializationException(this.GetType().FullName, e);
                }
            }
        }

        internal IPatternSet Patterns { get; private set; }

        /// <summary>
        /// 在初始化前引發。
        /// </summary>
        protected virtual void BeforeInitialize(dynamic config)
        {
        }
        /// <summary>
         /// 在初始化後引發。
         /// </summary>
        protected virtual void AfterInitialize()
        {
        }

        /// <summary>
        /// 建立模式規則。
        /// </summary>
        /// <param name="g"></param>
        /// <param name="p"></param>
        /// <param name="config"></param>
        protected abstract void BuildPatterns(IGroupManager g, IPatternBuilder p, dynamic config);
    }
}