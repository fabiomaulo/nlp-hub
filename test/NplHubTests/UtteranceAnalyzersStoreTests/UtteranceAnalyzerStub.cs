using NlpHub;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NplHubTests.UtteranceAnalyzersStoreTests
{
	public class UtteranceAnalyzerStub : IUtteranceAnalyzer
	{
		private IEnumerable<AnalyzedResult> result;

		public UtteranceAnalyzerStub(IEnumerable<AnalyzedResult> result)
		{
			this.result = result;
		}

		public Task<IEnumerable<AnalyzedResult>> Matchs(string utterance)
		{
			return Task.FromResult(result);
		}
	}
}
