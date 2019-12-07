using System;
using System.Collections.Generic;
using System.Text;
using PatrickMcDougle_CTL_Star.Composite.Model;
using PatrickMcDougle_CTL_Star.Data;

namespace PatrickMcDougle_CTL_Star.Composite.CTL
{
	/// <summary>
	///     ¬phi
	///     Precondition: phi is an arbotrary CTL Formula
	///     Postcondition: Returns the set of states satisfying ¬phi
	///
	///     \u00AC = ¬
	/// </summary>
	public class Negation : ACtlFormula
	{
		public Negation() : base("\u00AC")
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

		public override IList<StateComposite> Satisfies(ModelInformation modelInformation)
		{
			/*
			 * return S - SAT(phi)
			 * */
			IList<StateComposite> validPhiStates = CtlFormulaRight.Satisfies(modelInformation);

			IList<StateComposite> validStates = new List<StateComposite>();

			// S - SAT(phi)
			foreach (var state in modelInformation.AllStates)
			{
				if (!validPhiStates.Contains(state))
				{
					validStates.Add(state);
				}
			}

			return validStates;
		}
	}
}