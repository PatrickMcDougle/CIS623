﻿using System;
using System.Collections.Generic;
using System.Text;
using PatrickMcDougle_CTL_Star.Composite.Model;
using PatrickMcDougle_CTL_Star.Data;

namespace PatrickMcDougle_CTL_Star.Composite.CTL
{
	/// <summary>
	///     (phi ∧ psi)
	///     Precondition: phi and psi is an arbitrary CTL formula
	///     Postcondition: Returns the set of states satisfying (phi ∧ psi)
	///
	///     \u2229 = ∩ \u2227 = ∧
	/// </summary>
	public class Conjunction : ACtlFormula
	{
		public Conjunction() : base(nameof(Disjunction))
		{
		}

		public override string Display()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("(");
			sb.Append(CtlFormulaLeft?.Display());
			sb.Append(" \u2227 ");  // intersection syble, hopefully
			sb.Append(CtlFormulaRight?.Display());
			sb.Append(")");
			Console.WriteLine(sb.ToString());
			return sb.ToString();
		}

		public override IList<StateComposite> SAT(ModelInformation modelInformation)
		{
			IList<StateComposite> validPhiStates = CtlFormulaLeft.SAT(modelInformation);
			IList<StateComposite> validPsiStates = CtlFormulaRight.SAT(modelInformation);

			// (phi ∧ psi) == (phi ∩ psi) == (phi && psi)
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