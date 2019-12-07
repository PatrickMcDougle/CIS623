using System;
using System.Collections.Generic;
using System.Text;
using PatrickMcDougle_CTL_Star.Composite.Model;
using PatrickMcDougle_CTL_Star.Data;

namespace PatrickMcDougle_CTL_Star.Composite.CTL
{
	/// <summary>
	///     E phi U psi
	///     Precondition: phi and psi is an arbitrary CTL formula
	///     Postcondition: Returns the set of states satisfying E phi U psi
	/// </summary>
	public class EU : ACtlFormula
	{
		public EU() : base(nameof(EU))
		{
		}

		public override string Display()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("[");
			sb.Append(CtlFormulaLeft?.Display());
			sb.Append(" ");
			sb.Append(Name);
			sb.Append(" ");
			sb.Append(CtlFormulaRight?.Display());
			sb.Append("]");
			Console.WriteLine(sb.ToString());
			return sb.ToString();
		}

		public override IList<StateComposite> Satisfies(ModelInformation modelInformation)
		{
			/*
			 * W = SAT phi
			 * Y = SAT psi
			 * repeat until X == Y
			 *     begin
			 *         X = Y
			 *         Y = Y || (W && Pre_E(Y))
			 *     end
			 * return Y
			 * */

			IList<StateComposite> validPhiStates = CtlFormulaLeft.Satisfies(modelInformation);
			IList<StateComposite> validPsiStates = CtlFormulaRight.Satisfies(modelInformation);

			IList<StateComposite> validStates = new List<StateComposite>();

			// repeat
			while (validStates.Count != validPsiStates.Count)
			{
				// X = Y
				foreach (var psiState in validPsiStates)
				{
					if (!validStates.Contains(psiState))
					{
						validStates.Add(psiState);
					}
				}

				IList<StateComposite> validNewStates = new List<StateComposite>();

				// W && Pre_E(Y)
				foreach (var psiState in validPsiStates)
				{
					foreach (var parentState in psiState.ParentStates)
					{
						if (validPhiStates.Contains(parentState))
						{
							validNewStates.Add(parentState);
						}
					}
				}

				// Y = Y || (W && Pre_E(Y))
				foreach (var newState in validNewStates)
				{
					if (!validPhiStates.Contains(newState))
					{
						validPhiStates.Add(newState);
					}
				}
			}

			return validStates;
		}
	}
}