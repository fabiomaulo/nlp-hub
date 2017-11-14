using NlpHub;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NplHubTests.UtteranceAnalyzerPipelineTests
{
	public class Example01
	{
		internal class FirstInPipeline : UtteranceAnalyzerPipeline
		{
			protected FirstInPipeline(IUtteranceAnalyzer innerAnalyzer) : base(innerAnalyzer)
			{
				// required inner
			}

			public async override Task<IEnumerable<AnalyzedResult>> Analyze(string utterance)
			{
				// Analyze first and then analyze the inner
				var analyzed = new[] { new AnalyzedResult() };
				return analyzed.Concat(await InnerAnalyzer.Analyze(utterance));
			}
		}

		internal class SecondInPipeline : UtteranceAnalyzerPipeline
		{
			public SecondInPipeline(IUtteranceAnalyzer innerAnalyzer = null) : base(innerAnalyzer ?? new NoOpUtteranceAnalyzer())
			{
				// optional inner
			}

			public async override Task<IEnumerable<AnalyzedResult>> Analyze(string utterance)
			{
				// Analyze the inner first and then analyze this
				var analyzedByInner = await InnerAnalyzer.Analyze(utterance);
				var analyzed = new[] { new AnalyzedResult() };
				return analyzedByInner.Concat(analyzed);
			}
		}
	}
}
