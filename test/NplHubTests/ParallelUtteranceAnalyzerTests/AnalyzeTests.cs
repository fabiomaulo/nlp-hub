using NlpHub;
using NUnit.Framework;
using SharpTestsEx;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NplHubTests.ParallelUtteranceAnalyzerTests
{
	public class AnalyzeTests
	{
		[Test]
		public async Task WhenEmptyStoreThenNoMatch()
		{
			var analyzer = new ParallelUtteranceAnalyzer(new SortedUtteranceAnalyzersStore());
			IEnumerable<AnalyzedResult> actual = await analyzer.Analyze("whatever");
			actual.Should().Be.Empty();
		}

		[Test]
		public async Task WhenRegisteredWithMatchThenHaveMatch()
		{
			var analyzer = new ParallelUtteranceAnalyzer(
				new SortedUtteranceAnalyzersStore()
				.Register(new UtteranceAnalyzerStub(new[] { new AnalyzedResult() })));

			IEnumerable<AnalyzedResult> actual = await analyzer.Analyze("whatever");
			actual.Should().Not.Be.Null().And.Not.Be.Empty();
		}

		[Test]
		public async Task WhenRegisteredTwoThenHaveMatchs()
		{
			var analyzer = new ParallelUtteranceAnalyzer(
				new SortedUtteranceAnalyzersStore()
				.Register(new UtteranceAnalyzerStub(new[] { new AnalyzedResult() }))
				.Register(new UtteranceAnalyzerStub(new[] { new AnalyzedResult(), new AnalyzedResult() })));

			IEnumerable<AnalyzedResult> actual = await analyzer.Analyze("whatever");
			actual.Should().Have.Count.EqualTo(3);
		}

		[Test]
		public async Task WhenNullMatchThenEmptyResult()
		{
			var analyzer = new ParallelUtteranceAnalyzer(
				new SortedUtteranceAnalyzersStore()
				.Register(new UtteranceAnalyzerStub(null)));

			IEnumerable<AnalyzedResult> actual = await analyzer.Analyze("whatever");
			actual.Should().Be.Empty();
		}

		[Test]
		public async Task WhenNullMatchElementThenEmptyResult()
		{
			var analyzer = new ParallelUtteranceAnalyzer(
				new SortedUtteranceAnalyzersStore()
				.Register(new UtteranceAnalyzerStub(new AnalyzedResult[] { null })));

			IEnumerable<AnalyzedResult> actual = await analyzer.Analyze("whatever");
			actual.Should().Be.Empty();
		}

		[Test]
		public async Task WhenMatchNullElementThenIgnoreEmpty()
		{
			var analyzer = new ParallelUtteranceAnalyzer(
				new SortedUtteranceAnalyzersStore()
				.Register(new UtteranceAnalyzerStub(new [] { new AnalyzedResult(), null })));

			IEnumerable<AnalyzedResult> actual = await analyzer.Analyze("whatever");
			actual.Satisfies(x => x.All(a => a != null));
			actual.Should().Have.Count.EqualTo(1);
		}
	}
}
