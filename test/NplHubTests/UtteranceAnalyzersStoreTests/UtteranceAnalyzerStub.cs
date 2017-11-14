using NlpHub;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NplHubTests
{
	public class UtteranceAnalyzerStub : IUtteranceAnalyzer
	{
		private IEnumerable<AnalyzedResult> result;

		public UtteranceAnalyzerStub(IEnumerable<AnalyzedResult> result)
		{
			this.result = result;
		}

		public Task<IEnumerable<AnalyzedResult>> Analyze(string utterance)
		{
			return Task.FromResult(result);
		}
	}
}
