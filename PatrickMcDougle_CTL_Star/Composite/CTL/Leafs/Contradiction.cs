using System.Collections.Generic;
using PatrickMcDougle_CTL_Star.Composite.Model;
using PatrickMcDougle_CTL_Star.Data;

namespace PatrickMcDougle_CTL_Star.Composite.CTL
{
	/// <summary>
	///     Contradiction
	///     Precondition: value is False
	///     Postcondition: Returns the set of states satisfying Tautology. All States
	///
	///     \u22A5 = ⊥
	/// </summary>
	public class Contradiction : ACtlFormula
	{
		public Contradiction() : base("\u22A5")
		{
		}

		public override bool IsCtlFormulaLeftUsed { get => false; }
		public override bool IsCtlFormulaRightUsed { get => false; }

		public override string Display()
		{
			return Name;
		}

		public override IList<StateComposite> Satisfies(ModelInformation modelInformation)
		{
			/*
			 * return 0
			 * */

			IList<StateComposite> validStates = new List<StateComposite>();

			return validStates;
		}
	}
}