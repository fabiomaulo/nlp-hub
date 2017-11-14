using NlpHub;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NplHub
{
	public interface IUtteranceAnalyzers
	{
		Task<IEnumerable<AnalyzedResult>> Analyze(string utterance);
		void Register(IUtteranceAnalyzer analyzer);
	}
}