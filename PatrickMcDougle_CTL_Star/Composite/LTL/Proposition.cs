using System;

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

		public override bool IsPathValid()
		{
			return valid;
		}
	}
}