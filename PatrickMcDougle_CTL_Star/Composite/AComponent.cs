using System.Collections.Generic;

namespace PatrickMcDougle_CTL_Star.Composite
{
	public abstract class AComponent
	{
		protected string name;
		protected bool valid;

		protected AComponent(string name, bool valid = true)
		{
			this.name = name;
			this.valid = valid;
		}

		public abstract bool IsPathValid();
		public abstract string Display();
	}
}