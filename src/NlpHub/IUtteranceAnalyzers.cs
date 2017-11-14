using NlpHub;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace NplHub
{
	public interface IUtteranceAnalyzers
	{
		IUtteranceAnalyzers Register(IUtteranceAnalyzer analyzer);
		Task<IEnumerable<AnalyzedResult>> Analyze(string utterance);
		Task<IEnumerable<AnalyzedResult>> SequenceAnalyze(string utterance, Func<IEnumerable<AnalyzedResult>, bool> isEnough);
	}
}