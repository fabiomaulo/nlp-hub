using NlpHub;
using NplHub;
using NUnit.Framework;
using SharpTestsEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NplHubTests.UtteranceAnalyzersStoreTests
{
	public class SequenceAnalyzeTests
	{
		public class ExceptionUtteranceAnalyzer : IUtteranceAnalyzer
		{
			public Task<IEnumerable<AnalyzedResult>> Analyze(string utterance)
			{
				throw new NotImplementedException();
			}
		}

		[Test]
		public async Task WhenThresholdThenDoNotAnalyzeOthers()
		{
			IUtteranceAnalyzers analyzers = new UtteranceAnalyzers();
			analyzers.Register(new UtteranceAnalyzerStub(new[] { new AnalyzedResult { Intent = new Intent { Score = 0.6f } } }));
			analyzers.Register(new ExceptionUtteranceAnalyzer());
			Func<IEnumerable<AnalyzedResult>, bool> isEnough = r => r.Any(x => x.Intent.Score > 0.5f);
			IEnumerable<AnalyzedResult> actual = await analyzers.SequenceAnalyze("whatever", isEnough);
		}

		[Test]
		public async Task WhenNullClauseThenAnalyzeAll()
		{
			IUtteranceAnalyzers analyzers = new UtteranceAnalyzers();
			analyzers.Register(new UtteranceAnalyzerStub(new[] { new AnalyzedResult() }));
			analyzers.Register(new UtteranceAnalyzerStub(new[] { new AnalyzedResult() }));
			var actual = await analyzers.SequenceAnalyze("whatever", null);
			actual.Should().Have.Count.EqualTo(2);
		}

		[Test]
		public async Task WhenNullResultThenIgnoreJustNull()
		{
			IUtteranceAnalyzers analyzers = new UtteranceAnalyzers();
			analyzers.Register(new UtteranceAnalyzerStub(null));
			analyzers.Register(new UtteranceAnalyzerStub(new[] { new AnalyzedResult() }));
			var actual = await analyzers.SequenceAnalyze("whatever", null);
			actual.Should().Have.Count.EqualTo(1);
		}

		[Test]
		public async Task WhenNullElementsInResultThenIgnoreNull()
		{
			IUtteranceAnalyzers analyzers = new UtteranceAnalyzers();
			analyzers.Register(new UtteranceAnalyzerStub(new AnalyzedResult[] { null, new AnalyzedResult() }));
			var actual = await analyzers.SequenceAnalyze("whatever", null);
			actual.Should().Have.Count.EqualTo(1);
		}
	}
}
