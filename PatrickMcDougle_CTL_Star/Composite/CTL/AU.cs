using System;
using System.Collections.Generic;
using System.Text;
using PatrickMcDougle_CTL_Star.Composite.Model;
using PatrickMcDougle_CTL_Star.Data;

namespace PatrickMcDougle_CTL_Star.Composite.CTL
{
	/// <summary>
	///     AU phi
	///     Precondition: phi is an arbotrary CTL Formula
	///     Postcondition: Returns the set of states satisfying AU phi
	/// </summary>
	public class AU : ACtlFormula
	{
		public AU() : base(nameof(AU))
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
			 * phi AU psi
			 * return SAT ¬([¬psi EU (¬phi ∧ ¬psi)] ∨ EG ¬psi)
			 * */

			// ¬EX ¬phi
			Negation n1 = new Negation
			{
				CtlFormulaRight = new Disjunction
				{
					CtlFormulaLeft = new EU
					{
						CtlFormulaLeft = new Negation
						{
							CtlFormulaRight = this.CtlFormulaRight
						},
						CtlFormulaRight = new Conjunction
						{
							CtlFormulaLeft = new Negation
							{
								CtlFormulaRight = this.CtlFormulaLeft
							},
							CtlFormulaRight = new Negation
							{
								CtlFormulaRight = this.CtlFormulaRight
							}
						}
					},
					CtlFormulaRight = new EG
					{
						CtlFormulaRight = new Negation
						{
							CtlFormulaRight = this.CtlFormulaRight
						}
					}
				}
			};

			return n1.Satisfies(modelInformation);
		}
	}
}