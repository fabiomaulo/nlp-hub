using NlpHub;
using System.Collections.Generic;

namespace NplHub
{
	public class UtteranceAnalyzers : IUtteranceAnalyzers
	{
		public IEnumerable<AnalyzedResult> Analyze(string utterance)
		{
			return null;
		}

		public void Register(IUtteranceAnalyzer analyzer)
		{
			
		}
	}
}