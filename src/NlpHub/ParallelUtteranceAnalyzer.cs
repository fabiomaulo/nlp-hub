﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NlpHub
{
	/// <summary>
	/// An <see cref="IUtteranceAnalyzer"/> that analyze an utterance using ultiple analyzers in parallel
	/// </summary>
	public class ParallelUtteranceAnalyzer: IUtteranceAnalyzer
	{
		private IUtteranceAnalyzersStore store;

		/// <summary>
		/// Create a new instance of the analyzer.
		/// </summary>
		/// <param name="store">The store from where get enabled analyzers.</param>
		public ParallelUtteranceAnalyzer(IUtteranceAnalyzersStore store)
		{
			this.store = store ?? throw new ArgumentNullException(nameof(store));
		}

		public Task<IEnumerable<AnalyzedResult>> Analyze(string utterance)
		{
			throw new NotImplementedException();
		}
	}
}
