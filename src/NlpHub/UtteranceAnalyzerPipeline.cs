using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NlpHub
{
	/// <summary>
	/// Templete class to create a pipeline of analyzers
	/// </summary>
	public abstract class UtteranceAnalyzerPipeline : IUtteranceAnalyzer
	{
		private readonly IUtteranceAnalyzer innerAnalyzer;

		protected UtteranceAnalyzerPipeline()
			:this(new NoOpUtteranceAnalyzer())
		{
		}

		protected UtteranceAnalyzerPipeline(IUtteranceAnalyzer innerAnalyzer)
		{
			this.innerAnalyzer = innerAnalyzer ?? throw new ArgumentNullException(nameof(innerAnalyzer));
		}

		protected IUtteranceAnalyzer InnerAnalyzer => innerAnalyzer;

		public abstract Task<IEnumerable<AnalyzedResult>> Analyze(string utterance);
	}

	/// <summary>
	/// Do nothing analyzer to use as the end of the pipeline
	/// </summary>
	public class NoOpUtteranceAnalyzer : IUtteranceAnalyzer
	{
		private static Task<IEnumerable<AnalyzedResult>> empty = Task.FromResult<IEnumerable<AnalyzedResult>>(new AnalyzedResult[0]);
		public Task<IEnumerable<AnalyzedResult>> Analyze(string utterance) => empty;
	}
}
