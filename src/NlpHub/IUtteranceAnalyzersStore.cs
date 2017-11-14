using System.Collections.Generic;

namespace NlpHub
{
	public interface IUtteranceAnalyzersStore
	{
		IUtteranceAnalyzersStore Register(IUtteranceAnalyzer analyzer);
		IEnumerable<IUtteranceAnalyzer> Registered();
	}
}
