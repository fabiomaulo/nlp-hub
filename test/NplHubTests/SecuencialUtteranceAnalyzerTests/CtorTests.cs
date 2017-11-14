using NlpHub;
using NUnit.Framework;
using SharpTestsEx;
using System;
using System.Collections.Generic;

namespace NplHubTests.SecuencialUtteranceAnalyzerTests
{
	public class CtorTests
	{
		[Test]
		public void WhenCreateWithNullStoreThenThrows()
		{
			IUtteranceAnalyzersStore store = null;
			Executing.This(()=> new SecuencialUtteranceAnalyzer(store)).Should().Throw<ArgumentNullException>()
				.And.ValueOf.ParamName.Should().Be("store");
		}

		[Test]
		public void WhenCreateWithNullPredicateThenNotThrow()
		{
			Func<IEnumerable<AnalyzedResult>, bool> isEnough = null;
			Executing.This(() => new SecuencialUtteranceAnalyzer(new SortedUtteranceAnalyzersStore()
				, isEnough)).Should().NotThrow();
		}
	}
}
