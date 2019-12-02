using System;
using PatrickMcDougle_CTL_Star.Composite.Model;

namespace PatrickMcDougle_CTL_Star.Composite.LTL
{
	internal class Proposition : ALtlComponent
	{
		public Proposition(string name, bool valid = true) : base(name, valid)
		{
		}

		public override string Display()
		{
			Console.Write(name);
			return name;
		}

		public override bool IsLogicValid(StateComposite stateComposite)
		{
			return valid;
		}
	}
}