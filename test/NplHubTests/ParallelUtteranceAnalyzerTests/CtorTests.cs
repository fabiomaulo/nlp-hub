using NlpHub;
using NUnit.Framework;
using SharpTestsEx;
using System;

namespace NplHubTests.ParallelUtteranceAnalyzerTests
{
	public class CtorTests
	{
		[Test]
		public void WhenCreateWithNullStoreThenThrows()
		{
			IUtteranceAnalyzersStore store = null;
			Executing.This(()=> new ParallelUtteranceAnalyzer(store)).Should().Throw<ArgumentNullException>()
				.And.ValueOf.ParamName.Should().Be("store");
		}
	}
}
