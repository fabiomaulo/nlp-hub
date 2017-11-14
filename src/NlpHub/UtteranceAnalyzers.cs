using NlpHub;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NplHub
{
	public class UtteranceAnalyzers : IUtteranceAnalyzers
	{
		private ConcurrentBag<IUtteranceAnalyzer> analyzers = new ConcurrentBag<IUtteranceAnalyzer>();

		public async Task<IEnumerable<AnalyzedResult>> Analyze(string utterance)
		{
			var analyzerTasks = analyzers.Select(x => x.Matchs(utterance));
			return (await Task.WhenAll(analyzerTasks))
				.Where(x => x != null)
				.SelectMany(x=> x)
				.Where(x => x != null);
		}

		public void Register(IUtteranceAnalyzer analyzer)
		{
			analyzers.Add(analyzer);
		}
	}
}