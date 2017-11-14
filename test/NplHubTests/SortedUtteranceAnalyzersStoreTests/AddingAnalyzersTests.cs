using NlpHub;
using NUnit.Framework;
using SharpTestsEx;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NplHubTests.SortedUtteranceAnalyzersStoreTests
{
	public class AddingAnalyzersTests
	{
		public class Fake1 : IUtteranceAnalyzer
		{
			public Task<IEnumerable<AnalyzedResult>> Analyze(string utterance)
			{
				throw new NotImplementedException();
			}
		}

		public class Fake2 : IUtteranceAnalyzer
		{
			public Task<IEnumerable<AnalyzedResult>> Analyze(string utterance)
			{
				throw new NotImplementedException();
			}
		}

		[Test]
		public void WhenNoregistrationThenEmpty()
		{
			IUtteranceAnalyzersStore analyzers = new SortedUtteranceAnalyzersStore();
			IEnumerable<IUtteranceAnalyzer> actual = analyzers.Registered();
			actual.Should().Be.Empty();
		}

		[Test]
		public void WhenAddNullThenEmpty()
		{
			IUtteranceAnalyzersStore analyzers = new SortedUtteranceAnalyzersStore();
			analyzers.Register(null);
			analyzers.Registered().Should().Be.Empty();
		}

		[Test]
		public void WhenAddOneThenHaveOne()
		{
			IUtteranceAnalyzersStore analyzers = new SortedUtteranceAnalyzersStore();
			analyzers.Register(new Fake1());
			analyzers.Registered().Should().Have.Count.EqualTo(1);
		}

		[Test]
		public void WhenAddTwoThenReturnInSequence()
		{
			IUtteranceAnalyzersStore analyzers = new SortedUtteranceAnalyzersStore();
			var a1 = new Fake1();
			var a2 = new Fake2();
			analyzers.Register(a1);
			analyzers.Register(a2);
			analyzers.Registered().Should().Have.SameSequenceAs(a1, a2);
		}
	}
}
