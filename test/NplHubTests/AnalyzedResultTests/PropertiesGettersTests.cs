using NlpHub;
using NUnit.Framework;
using SharpTestsEx;

namespace NplHubTests.AnalyzedResultTests
{
	public class PropertiesGettersTests
	{
		[Test]
		public void WhenCreateThenAnalyzerIdIsNotNull()
		{
			var actual = new AnalyzedResult();
			actual.AnalyzerId.Should().Not.Be.Null();
		}

		[Test]
		public void WhenAssignNullToAnalyzerIdThenAnalyzerIdIsNotNull()
		{
			var actual = new AnalyzedResult();
			actual.AnalyzerId = null;
			actual.AnalyzerId.Should().Not.Be.Null();
		}

		[Test]
		public void WhenCreateThenEntitiesIsNotNull()
		{
			var actual = new AnalyzedResult();
			actual.Entities.Should().Not.Be.Null();
		}

		[Test]
		public void WhenAssignNullToEntitiesThenEntitiesIsEmpty()
		{
			var actual = new AnalyzedResult();
			actual.Entities = null;
			actual.Entities.Should().Not.Be.Null();
		}
	}
}
