using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NlpHub
{
	/// <summary>
	/// An <see cref="IUtteranceAnalyzer"/> that analyze an utterance using multiple analyzers in sequence
	/// </summary>
	public class SecuencialUtteranceAnalyzer : IUtteranceAnalyzer
	{
		private static Func<IEnumerable<AnalyzedResult>, bool> neverEnough = x => false;
		private readonly IUtteranceAnalyzersStore store;
		private readonly Func<IEnumerable<AnalyzedResult>, bool> isEnough;

		/// <summary>
		/// Create a new instance of the analyzer.
		/// </summary>
		/// <param name="store">The store from where get enabled analyzers.</param>
		/// <param name="isEnough">A predicate to define when the analisys should be stopped.</param>
		public SecuencialUtteranceAnalyzer(IUtteranceAnalyzersStore store
			, Func<IEnumerable<AnalyzedResult>, bool> isEnough = null)
		{
			this.store = store ?? throw new ArgumentNullException(nameof(store));
			this.isEnough = isEnough ?? neverEnough;
		}

		public async Task<IEnumerable<AnalyzedResult>> Analyze(string utterance)
		{
			var analyzers = store.Registered();

			var results = new List<AnalyzedResult>();
			foreach (var a in analyzers)
			{
				var r = await a.Analyze(utterance);
				if (r == null)
				{
					continue;
				}
				results.AddRange(r.Where(x => x != null));
				if (isEnough(r))
				{
					return results;
				}
			}
			return results;
		}
	}
}
