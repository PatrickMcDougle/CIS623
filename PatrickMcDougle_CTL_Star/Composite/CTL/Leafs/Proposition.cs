using System;
using System.Collections.Generic;
using PatrickMcDougle_CTL_Star.Composite.Model;
using PatrickMcDougle_CTL_Star.Data;

namespace PatrickMcDougle_CTL_Star.Composite.CTL
{
	public class Proposition : ACtlFormula
	{
		public Proposition(string name) : base(name)
		{
		}

		public override string Display()
		{
			Console.Write(Name);
			return Name;
		}

		public override IList<StateComposite> Satisfies(ModelInformation modelInformation)
		{
			IList<StateComposite> validStates = new List<StateComposite>();

			foreach (StateComposite state in modelInformation.AllStates)
			{
				if (state.IsPropositionValid(Name))
				{
					validStates.Add(state);
				}
			}

			return validStates;
		}
	}
}