using System.Collections.Generic;
using PatrickMcDougle_CTL_Star.Composite.Model;
using PatrickMcDougle_CTL_Star.Data;

namespace PatrickMcDougle_CTL_Star.Composite.CTL
{
	/// <summary>
	///     Tautology
	///     Precondition: value is True
	///     Postcondition: Returns the set of states satisfying Tautology. All States
	///
	///     \u22A4 = ⊤
	/// </summary>
	public class Tautology : ACtlFormula
	{
		public Tautology() : base("\u22A4")
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
			 * return S
			 * */

			IList<StateComposite> validStates = new List<StateComposite>();

			if (modelInformation != null)
			{
				foreach (var state in modelInformation.AllStates)
				{
					validStates.Add(state);
				}
			}

			return validStates;
		}
	}
}