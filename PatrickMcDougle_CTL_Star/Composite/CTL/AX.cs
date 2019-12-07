using System;
using System.Collections.Generic;
using System.Text;
using PatrickMcDougle_CTL_Star.Composite.Model;
using PatrickMcDougle_CTL_Star.Data;

namespace PatrickMcDougle_CTL_Star.Composite.CTL
{
	/// <summary>
	///     AX phi
	///     Precondition: phi is an arbotrary CTL Formula
	///     Postcondition: Returns the set of states satisfying AX phi
	/// </summary>
	public class AX : ACtlFormula
	{
		public AX() : base(nameof(AX))
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
			 * return SAT ¬EX ¬phi
			 * */

			// ¬EX ¬phi
			Negation n1 = new Negation
			{
				CtlFormulaRight = new EX
				{
					CtlFormulaRight = new Negation
					{
						CtlFormulaRight = this.CtlFormulaRight
					}
				}
			};

			return n1.Satisfies(modelInformation);
		}
	}
}