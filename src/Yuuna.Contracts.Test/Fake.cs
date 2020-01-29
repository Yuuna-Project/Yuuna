// Author: Orlys
// Github: https://github.com/Orlys

namespace Yuuna.Contracts.Test
{
    using System;

    using Yuuna.Contracts.Interaction;
    using Yuuna.Contracts.Patterns;
    using Yuuna.Contracts.Modules;
    using Yuuna.Contracts.Semantics;
    using Yuuna.Common.Utilities;
    using System.Diagnostics;
    using Yuuna.Common.Configuration;

    public sealed class Fake : ModuleBase
    {
        private volatile bool _doorState;
        private volatile bool _lightState;
        public Fake()
        {
            this._doorState = new[] { true, false, true, false }.RandomTakeOne();
            Debug.WriteLine("門的狀態是: " + (this._doorState ? "開著的" : "關著的"));

            this._lightState = new[] { true, false, true, false }.RandomTakeOne();
            Debug.WriteLine("燈的狀態是: " + (this._lightState ? "開著的" : "關著的"));
        }

        

        protected override void BuildPatterns(IGroupManager g, IPatternBuilder p, dynamic c)
        {
            g.Define("open").AppendOrCreate(new[] { "打開", "開" });
            g.Define("close").AppendOrCreate(new[] { "關上", "關" });
            g.Define("door").AppendOrCreate(new[] { "門", "房間門", "房門" });
            g.Define("light").AppendOrCreate(new[] { "燈", "檯燈" });
            
            p.Build(g["open"], g["door"]).OnInvoke(score =>
            {
                Debug.WriteLine("門的狀態是: " + (this._doorState ? "開著的" : "關著的"));
                // 開門
                if (!this._doorState)
                {
                    this._doorState = !this._doorState;
                    return (Moods.Happy, "已經開好門囉 <3");
                }
                else
                    return new Response[]
                    {
                        (Moods.Sad,  "可是門本來就是開的欸 QAQ"  ),
                        (Moods.Normal,  "喔是喔，不過就是一扇門，早就開好了"  ),
                        (Moods.Sad,  "可是門本來就是開的欸 QAQ"  ),
                    }
                    .RandomTakeOne();
            });

            p.Build(g["close"], g["door"]).OnInvoke(score =>
            {
                Debug.WriteLine("門的狀態是: " + (this._doorState ? "開著的" : "關著的"));
                // 開門
                if (this._doorState)
                {
                    this._doorState = !this._doorState;
                    return (Moods.Happy, "已經關好門囉 <3");
                }
                else
                    return new Response[]
                    {
                        (Moods.Sad,  "可是門本來就是關的欸 QAQ"  ),
                        (Moods.Sad,  "門本來就是關著的，北七逆?"  ),
                        (Moods.Sad,  "我不想動，因為本來就沒開過"  ),
                    }
                    .RandomTakeOne();
            });

            p.Build(g["open"], g["light"]).OnInvoke(score =>
            {
                Debug.WriteLine("燈的狀態是: " + (this._lightState ? "開著的" : "關著的"));
                // 開燈
                if (!this._lightState)
                {
                    this._lightState = !this._lightState;
                    return (Moods.Happy, "已經開好燈囉 <3");
                }
                else
                    return (Moods.Sad, "可是燈本來就是開的欸 QAQ");
            });

            p.Build(g["close"], g["light"]).OnInvoke(score =>
            {
                Debug.WriteLine("燈的狀態是: " + (this._lightState ? "開著的" : "關著的"));
                // 開燈 
                if (this._lightState)
                {
                    this._lightState = !this._lightState;
                    return (Moods.Happy, "已經關好燈囉 <3");
                }
                else
                    return (Moods.Sad, "可是燈本來就是關的欸 QAQ");
            });

        }
    }
}