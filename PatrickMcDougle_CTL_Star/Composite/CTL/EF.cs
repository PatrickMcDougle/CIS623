using System;
using System.Collections.Generic;
using System.Text;
using PatrickMcDougle_CTL_Star.Composite.Model;
using PatrickMcDougle_CTL_Star.Data;

namespace PatrickMcDougle_CTL_Star.Composite.CTL
{
	/// <summary>
	///     EF phi
	///     Precondition: phi is an arbotrary CTL Formula
	///     Postcondition: Returns the set of states satisfying EF phi
	/// </summary>
	public class EF : ACtlFormula
	{
		public EF() : base(nameof(EF))
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
			 * return SAT ⊤ EU phi
			 * */

			// ⊤ EU phi
			EU eu = new EU
			{
				CtlFormulaLeft = new Tautology(),
				CtlFormulaRight = this.CtlFormulaRight
			};

			return eu.Satisfies(modelInformation);
		}
	}
}