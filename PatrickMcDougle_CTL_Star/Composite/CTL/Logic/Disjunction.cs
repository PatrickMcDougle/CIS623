using System;
using System.Collections.Generic;
using System.Text;
using PatrickMcDougle_CTL_Star.Composite.Model;
using PatrickMcDougle_CTL_Star.Data;

namespace PatrickMcDougle_CTL_Star.Composite.CTL
{
	/// <summary>
	///     (phi ∨ psi)
	///     Precondition: phi and psi is an arbitrary CTL formula
	///     Postcondition: Returns the set of states satisfying (phi ∨ psi)
	///
	///     \u222A = ∪ \u2228 = ∨
	/// </summary>
	public class Disjunction : ACtlFormula
	{
		public Disjunction() : base("\u2228")
		{
		}

		public override string Display()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("(");
			sb.Append(CtlFormulaLeft?.Display());
			sb.Append(" ");
			sb.Append(Name);
			sb.Append(" ");
			sb.Append(CtlFormulaRight?.Display());
			sb.Append(")");
			Console.WriteLine(sb.ToString());
			return sb.ToString();
		}

		public override IList<StateComposite> Satisfies(ModelInformation modelInformation)
		{
			IList<StateComposite> validPhiStates = CtlFormulaLeft.Satisfies(modelInformation);
			IList<StateComposite> validPsiStates = CtlFormulaRight.Satisfies(modelInformation);

			// (phi ∨ psi) == (phi ∪ psi) == (phi || psi)
			foreach (var phiState in validPhiStates)
			{
				if (!validPsiStates.Contains(phiState))
				{
					validPsiStates.Add(phiState);
				}
			}

			return validPsiStates;
		}
	}
}