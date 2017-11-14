using NlpHub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NplHub
{
	public class UtteranceAnalyzers : IUtteranceAnalyzers
	{
		private static Func<IEnumerable<AnalyzedResult>, bool> neverEnough = x => false;
		private List<IUtteranceAnalyzer> analyzers = new List<IUtteranceAnalyzer>();

		public void Register(IUtteranceAnalyzer analyzer)
		{
			if(analyzer == null)
			{
				return;
			}
			analyzers.Add(analyzer);
		}

		public async Task<IEnumerable<AnalyzedResult>> Analyze(string utterance)
		{
			var analyzerTasks = analyzers.Select(x => x.Matchs(utterance));
			return (await Task.WhenAll(analyzerTasks))
				.Where(x => x != null)
				.SelectMany(x => x)
				.Where(x => x != null);
		}

		public async Task<IEnumerable<AnalyzedResult>> SequenceAnalyze(string utterance, Func<IEnumerable<AnalyzedResult>, bool> isEnough = null)
		{
			var results = new List<AnalyzedResult>();
			var safeEnough = isEnough ?? neverEnough;
			foreach (var a in analyzers)
			{
				var r = await a.Matchs(utterance);
				results.AddRange(r);
				if(safeEnough(r))
				{
					return results;
				}
			}
			return results;
		}
	}
}