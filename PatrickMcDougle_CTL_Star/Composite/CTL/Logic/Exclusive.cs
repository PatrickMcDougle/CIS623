using System;
using System.Collections.Generic;
using System.Text;
using PatrickMcDougle_CTL_Star.Composite.Model;
using PatrickMcDougle_CTL_Star.Data;

namespace PatrickMcDougle_CTL_Star.Composite.CTL
{
	/// <summary>
	///     (phi ⊕ psi)
	///     Precondition: phi and psi is an arbitrary CTL formula
	///     Postcondition: Returns the set of states satisfying (phi ⊕ psi)
	///
	///     \u2295 = ⊕
	/// </summary>
	public class Exclusive : ACtlFormula
	{
		public Exclusive() : base("\u2295")
		{
		}

		public override bool IsCtlFormulaLeftUsed { get => true; }
		public override bool IsCtlFormulaRightUsed { get => true; }

		public override string Display()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("(");
			sb.Append(CtlFormulaLeft == null ? "__" : CtlFormulaLeft.Display());
			sb.Append(" ");
			sb.Append(Name);
			sb.Append(" ");
			sb.Append(CtlFormulaRight == null ? "__" : CtlFormulaRight.Display());
			sb.Append(")");
			Console.WriteLine(sb.ToString());
			return sb.ToString();
		}

		public override IList<StateComposite> Satisfies(ModelInformation modelInformation)
		{
			IList<StateComposite> validPhiStates = CtlFormulaLeft.Satisfies(modelInformation);
			IList<StateComposite> validPsiStates = CtlFormulaRight.Satisfies(modelInformation);

			IList<StateComposite> validStates = new List<StateComposite>();

			// (phi ⊕ psi) == (phi != psi)
			foreach (var phiState in validPhiStates)
			{
				if (!validPsiStates.Contains(phiState) && !validStates.Contains(phiState))
				{
					validStates.Add(phiState);
				}
			}
			foreach (var psiState in validPsiStates)
			{
				if (!validPhiStates.Contains(psiState) && !validStates.Contains(psiState))
				{
					validStates.Add(psiState);
				}
			}

			return validPsiStates;
		}
	}
}