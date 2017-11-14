using System.Collections.Generic;
using System.Linq;

namespace NlpHub
{
	/// <summary>
	/// The utterance analyzer
	/// </summary>
	public interface IUtteranceAnalyzer
	{
		/// <summary>
		/// Matchs of an analysis
		/// </summary>
		/// <param name="utterance">The utterance to analyze</param>
		/// <returns>The collection of matching result</returns>
		IEnumerable<AnalyzedResult> Matchs(string utterance);
	}
}
