using System.Collections.Generic;
using PatrickMcDougle_CTL_Star.Composite.Model;
using PatrickMcDougle_CTL_Star.Data;

namespace PatrickMcDougle_CTL_Star.Composite.CTL
{
	public abstract class ACtlFormula
	{
		public ACtlFormula CtlFormulaLeft { get; set; }
		public ACtlFormula CtlFormulaRight { get; set; }

		public abstract bool IsCtlFormulaLeftUsed { get; }
		public abstract bool IsCtlFormulaRightUsed { get; }

		public abstract string Display();

		public abstract IList<StateComposite> Satisfies(ModelInformation modelInformation);

		protected ACtlFormula(string name)
		{
			Name = name;
		}

		protected string Name { get; private set; }
	}
}