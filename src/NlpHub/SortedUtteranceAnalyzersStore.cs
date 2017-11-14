using System.Collections.Generic;

namespace NlpHub
{
	public class SortedUtteranceAnalyzersStore : IUtteranceAnalyzersStore
	{
		private List<IUtteranceAnalyzer> analyzers = new List<IUtteranceAnalyzer>();

		public IUtteranceAnalyzersStore Register(IUtteranceAnalyzer analyzer)
		{
			if(analyzer != null)
			{
				analyzers.Add(analyzer);
			}
			return this;
		}

		public IEnumerable<IUtteranceAnalyzer> Registered() => analyzers;
	}
}
