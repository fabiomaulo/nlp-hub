using NlpHub;
using NplHub;
using NUnit.Framework;
using SharpTestsEx;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NplHubTests.UtteranceAnalyzersStoreTests
{
	public class RegistrationTests
	{
		public class UtteranceAnalyzerStub: IUtteranceAnalyzer
		{
			private IEnumerable<AnalyzedResult> result;

			public UtteranceAnalyzerStub(IEnumerable<AnalyzedResult> result)
			{
				this.result = result;
			}

			public Task<IEnumerable<AnalyzedResult>> Matchs(string utterance)
			{
				return Task.FromResult(result);
			}
		}

		[Test]
		public async Task WhenNoRegisteredThenNoMatch()
		{
			IUtteranceAnalyzers analyzers = new UtteranceAnalyzers();
			var utterance = "whatever";
			IEnumerable<AnalyzedResult> actual = await analyzers.Analyze(utterance);
			actual.Should().Be.Empty();
		}

		[Test]
		public async Task WhenRegisteredWithMatchThenHaveMatch()
		{
			IUtteranceAnalyzers analyzers = new UtteranceAnalyzers();
			IUtteranceAnalyzer analyzer = new UtteranceAnalyzerStub(new[] { new AnalyzedResult() });
			analyzers.Register(analyzer);
			IEnumerable<AnalyzedResult> actual = await analyzers.Analyze("whatever");
			actual.Should().Not.Be.Null().And.Not.Be.Empty();
		}

		[Test]
		public async Task WhenNullMatchThenEmptyResult()
		{
			IUtteranceAnalyzers analyzers = new UtteranceAnalyzers();
			IUtteranceAnalyzer analyzer = new UtteranceAnalyzerStub(null);
			analyzers.Register(analyzer);
			IEnumerable<AnalyzedResult> actual = await analyzers.Analyze("whatever");
			actual.Should().Be.Empty();
		}

		[Test]
		public async Task WhenNullMatchElementThenEmptyResult()
		{
			IUtteranceAnalyzers analyzers = new UtteranceAnalyzers();
			IUtteranceAnalyzer analyzer = new UtteranceAnalyzerStub(new AnalyzedResult[] { null });
			analyzers.Register(analyzer);
			IEnumerable<AnalyzedResult> actual = await analyzers.Analyze("whatever");
			actual.Should().Be.Empty();
		}

		[Test]
		public async Task WhenMatchNullElementThenIgnoreEmpty()
		{
			IUtteranceAnalyzers analyzers = new UtteranceAnalyzers();
			IUtteranceAnalyzer analyzer = new UtteranceAnalyzerStub(new [] { new AnalyzedResult(), null });
			analyzers.Register(analyzer);
			IEnumerable<AnalyzedResult> actual = await analyzers.Analyze("whatever");
			actual.Satisfies(x=> x.All(a=> a != null));
			actual.Should().Have.Count.EqualTo(1);
		}
	}
}
