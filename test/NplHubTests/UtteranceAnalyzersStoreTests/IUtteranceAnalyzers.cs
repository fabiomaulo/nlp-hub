using System.Collections.Generic;
using NlpHub;

namespace NplHub
{
	public interface IUtteranceAnalyzers
	{
		IEnumerable<AnalyzedResult> Analyze(string utterance);
		void Register(IUtteranceAnalyzer analyzer);
	}
}