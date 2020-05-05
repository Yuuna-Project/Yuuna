// Author: Yuuna-Project@Orlys
// Github: github.com/Orlys
// Contact: orlys@yuuna-project.com

namespace Yuuna.Core
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Threading.Tasks;

    using Yuuna.Contracts.Evaluation;
    using Yuuna.Contracts.Patterns;
    using Yuuna.Interaction.AspNetCore;

    public class Strategy2 : IStrategy
    {
        public Alternative FindBest(IEnumerable<IRule> psc, IImmutableList<string> feed)
        {
            //var s = (from ps in psc
            //         let patterns = ps.ToImmutable()
            //         let matched = feed.SequenceMatchCount(patterns,
            //             (string s, IPattern p) => p.ToImmutable().Any(g => g.Equals(s))).ToArray()
            //         where matched.Length > 0
            //         let weight = matched.Length == patterns.Count ? int.MaxValue : patterns.Count
            //         orderby weight descending
            //         let j = Match.Create()
            //         select new { PatternSet = ps, Weight = weight }).ToImmutableArray();

            return Alternative.Create(default);
        }
    }

    internal static class EntryPoint
    {
        public static async Task Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler((sender, e) =>
            {
                Console.WriteLine(e.ExceptionObject);
            });

            await Startup.RunAsync();
        }
    }
}