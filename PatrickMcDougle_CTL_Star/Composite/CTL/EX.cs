using System;
using System.Collections.Generic;
using System.Text;
using PatrickMcDougle_CTL_Star.Composite.Model;
using PatrickMcDougle_CTL_Star.Data;

namespace PatrickMcDougle_CTL_Star.Composite.CTL
{
	/// <summary>
	///     EX phi
	///     Precondition: phi is an arbotrary CTL Formula
	///     Postcondition: Returns the set of states satisfying EX phi
	/// </summary>
	public class EX : ACtlFormula
	{
		public EX() : base(nameof(EX))
		{
		}

		public override bool IsCtlFormulaLeftUsed { get => false; }
		public override bool IsCtlFormulaRightUsed { get => true; }

		public override string Display()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(Name);
			sb.Append("(");
			sb.Append(CtlFormulaRight == null ? "__" : CtlFormulaRight.Display());
			sb.Append(")");
			Console.WriteLine(sb.ToString());
			return sb.ToString();
		}

		public override IList<StateComposite> Satisfies(ModelInformation modelInformation)
		{
			/*
			 * X = SAT phi
			 * Y = Pre_E(X)
			 * return Y
			 * */
			IList<StateComposite> validPhiStates = CtlFormulaRight.Satisfies(modelInformation);

			IList<StateComposite> validStates = new List<StateComposite>();

			// Y = Pre_E(X)
			foreach (var phiState in validPhiStates)
			{
				foreach (var parentState in phiState.ParentStates)
				{
					if (!validStates.Contains(parentState))
					{
						validStates.Add(parentState);
					}
				}
			}

			return validStates;
		}
	}
}