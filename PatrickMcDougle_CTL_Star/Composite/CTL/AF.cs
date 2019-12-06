using System;
using System.Collections.Generic;
using System.Text;
using PatrickMcDougle_CTL_Star.Composite.Model;
using PatrickMcDougle_CTL_Star.Data;

namespace PatrickMcDougle_CTL_Star.Composite.CTL
{
	/// <summary>
	///     AF phi
	///     Precondition: phi is an arbitrary CTL Formula
	///     Postcondition: Returns the set of states satisfying AF phi
	/// </summary>
	public class AF : ACtlFormula
	{
		public AF() : base(nameof(AF))
		{
		}

		public override string Display()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(Name);
			sb.Append("(");
			sb.Append(CtlFormulaRight?.Display());
			sb.Append(")");
			Console.WriteLine(sb.ToString());
			return sb.ToString();
		}

		public override IList<StateComposite> SAT(ModelInformation modelInformation)
		{
			/*
			 * Y = SAT phi
			 * repeat until X == Y
			 *     begin
			 *         X = Y
			 *         Y = Y || Pre_A(Y)
			 *     end
			 * return Y
			 * */

			IList<StateComposite> validPhiStates = CtlFormulaRight.SAT(modelInformation);

			IList<StateComposite> validStates = new List<StateComposite>();

			// repeat
			while (validStates.Count != validPhiStates.Count)
			{
				// X = Y
				foreach (var phiState in validPhiStates)
				{
					if (!validStates.Contains(phiState))
					{
						validStates.Add(phiState);
					}
				}

				IList<StateComposite> validNewStates = new List<StateComposite>();

				// Pre_A(Y)
				foreach (var phiState in validPhiStates)
				{
					foreach (var parentState in phiState.ParentStates)
					{
						bool allChilderenStatesValid = true;
						foreach (var childState in parentState.ChildrenStates)
						{
							allChilderenStatesValid &= validPhiStates.Contains(childState);
							if (!allChilderenStatesValid)
							{
								break;
							}
						}

						if (allChilderenStatesValid && !validNewStates.Contains(parentState))
						{
							validNewStates.Add(parentState);
						}
					}
				}

				// Y = Y || Pre_A(Y)
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