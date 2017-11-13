using System.Collections.Generic;

namespace NlpHub
{
	/// <summary>
	/// The result of an analysis.
	/// </summary>
	public class AnalyzedResult
	{
		/// <summary>
		/// A code identifying the source of the decision
		/// </summary>
		public string DecisionIdentity { get; set; } = "undefined";

		/// <summary>
		/// The intent of an utterance
		/// </summary>
		public Intent Intent { get; set; }

		/// <summary>
		/// Optional entities found.
		/// </summary>
		public IEnumerable<Entity> Entities { get; set; }

		public dynamic AnalyzedResponse { get; set; }
	}

	/// <summary>
	/// Entity identified in an utterance
	/// </summary>
	public class Entity
	{
		/// <summary>
		/// The type/code of the entity
		/// </summary>
		public string Type { get; set; }

		/// <summary>
		/// The value of the entity
		/// </summary>
		public string Value { get; set; }

		/// <summary>
		/// The score/probability (0..1) of the entity presence
		/// </summary>
		public float Score { get; set; }
	}

	/// <summary>
	/// An intent identified in the utterance 
	/// </summary>
	public class Intent
	{
		/// <summary>
		/// The code of the intent
		/// </summary>
		public string Code { get; set; }

		/// <summary>
		/// The score/probability (0..1) of the intent
		/// </summary>
		public float Score { get; set; }
	}

	/// <summary>
	/// The utterance analyzer
	/// </summary>
	public interface IUtteranceAnalyzer
	{
		/// <summary>
		/// Matchs of an analysis
		/// </summary>
		/// <param name="utterance">The utterance to analyze</param>
		/// <returns>The collection of matching result</returns>
		IEnumerable<AnalyzedResult> Matchs(string utterance);
	}
}
