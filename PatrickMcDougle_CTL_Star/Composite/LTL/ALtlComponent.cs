﻿using System.Collections.Generic;
using PatrickMcDougle_CTL_Star.Composite.Model;

namespace PatrickMcDougle_CTL_Star.Composite.LTL
{
	public abstract class ALtlComponent
	{
		public abstract string Display();

		public abstract bool IsModelAndPathValid(StateComposite stateComposite, IList<string> path);

		protected string name;
		protected bool valid;

		protected ALtlComponent(string name, bool valid = true)
		{
			this.name = name;
			this.valid = valid;
		}
	}
}