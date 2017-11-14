using System.Collections.Generic;
using System.Linq;

namespace NlpHub
{
	/// <summary>
	/// The result of an analysis.
	/// </summary>
	public class AnalyzedResult
	{
		private string analyzerId = "undefined";
		private IEnumerable<Entity> entities = Enumerable.Empty<Entity>();
		/// <summary>
		/// A code identifying the source of the decision
		/// </summary>
		public string AnalyzerId
		{
			get => analyzerId;
			set => analyzerId = value ?? "undefined";
		}

		/// <summary>
		/// The intent of an utterance
		/// </summary>
		public Intent Intent { get; set; }

		/// <summary>
		/// Optional entities found.
		/// </summary>
		public IEnumerable<Entity> Entities
		{
			get => entities;
			set => entities = value ?? Enumerable.Empty<Entity>();
		}

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
}
