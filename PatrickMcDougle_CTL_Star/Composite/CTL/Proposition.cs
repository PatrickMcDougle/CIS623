﻿using System;
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

		public override IList<StateComposite> SAT(ModelInformation modelInformation)
		{
			IList<StateComposite> validStates = new List<StateComposite>();
			if (modelInformation.CurrentState.IsPropositionValid(Name))
			{
				validStates.Add(modelInformation.CurrentState);
			}
			return validStates;
		}
	}
}