using NlpHub;
using NUnit.Framework;
using SharpTestsEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NplHubTests.SecuencialUtteranceAnalyzerTests
{
    public class AnalyzeTests
    {
		public class ExceptionUtteranceAnalyzer : IUtteranceAnalyzer
		{
			public Task<IEnumerable<AnalyzedResult>> Analyze(string utterance)
			{
				throw new NotImplementedException();
			}
		}

		[Test]
		public async Task WhenEmptyStoreThenNoMatch()
		{
			var analyzer = new SecuencialUtteranceAnalyzer(new SortedUtteranceAnalyzersStore(), x=> false);
			IEnumerable<AnalyzedResult> actual = await analyzer.Analyze("whatever");
			actual.Should().Be.Empty();
		}

		[Test]
		public async Task WhenThresholdThenDoNotAnalyzeOthers()
		{
			var store = new SortedUtteranceAnalyzersStore()
				.Register(new UtteranceAnalyzerStub(new[] { new AnalyzedResult { Intent = new Intent { Score = 0.6f } } }))
				.Register(new ExceptionUtteranceAnalyzer());

			var analyzer = new SecuencialUtteranceAnalyzer(store, r => r.Any(x => x.Intent.Score > 0.5f));

			IEnumerable<AnalyzedResult> actual = await analyzer.Analyze("whatever");
			actual.Should().Not.Be.Empty();
		}

		[Test]
		public async Task WhenNullClauseThenAnalyzeAll()
		{
			var store = new SortedUtteranceAnalyzersStore()
				.Register(new UtteranceAnalyzerStub(new[] { new AnalyzedResult() }))
				.Register(new UtteranceAnalyzerStub(new[] { new AnalyzedResult() }));

			var analyzer = new SecuencialUtteranceAnalyzer(store, null);

			var actual = await analyzer.Analyze("whatever");
			actual.Should().Have.Count.EqualTo(2);
		}

		[Test]
		public async Task WhenNullResultThenIgnoreJustNull()
		{
			var store = new SortedUtteranceAnalyzersStore()
				.Register(new UtteranceAnalyzerStub(null))
				.Register(new UtteranceAnalyzerStub(new[] { new AnalyzedResult() }));

			var analyzer = new SecuencialUtteranceAnalyzer(store, null);

			var actual = await analyzer.Analyze("whatever");
			actual.Should().Have.Count.EqualTo(1);
		}

		[Test]
		public async Task WhenNullElementsInResultThenIgnoreNull()
		{
			var store = new SortedUtteranceAnalyzersStore()
				.Register(new UtteranceAnalyzerStub(new[] {null, new AnalyzedResult() }));

			var analyzer = new SecuencialUtteranceAnalyzer(store, null);

			var actual = await analyzer.Analyze("whatever");
			actual.Should().Have.Count.EqualTo(1);
		}
	}
}
