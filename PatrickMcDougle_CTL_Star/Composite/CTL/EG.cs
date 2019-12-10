using System;
using System.Collections.Generic;
using System.Text;
using PatrickMcDougle_CTL_Star.Composite.Model;
using PatrickMcDougle_CTL_Star.Data;

namespace PatrickMcDougle_CTL_Star.Composite.CTL
{
	/// <summary>
	///     EG phi
	///     Precondition: phi is an arbotrary CTL Formula
	///     Postcondition: Returns the set of states satisfying EG phi
	/// </summary>
	public class EG : ACtlFormula
	{
		public EG() : base(nameof(EG))
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
			 * return SAT ¬AF ¬phi
			 * */

			// ¬EX ¬phi
			Negation n1 = new Negation
			{
				CtlFormulaRight = new AF
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