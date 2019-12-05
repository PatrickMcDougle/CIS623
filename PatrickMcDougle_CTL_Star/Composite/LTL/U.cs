using System;
using System.Collections.Generic;
using PatrickMcDougle_CTL_Star.Composite.Model;

namespace PatrickMcDougle_CTL_Star.Composite.LTL
{
	/// <summary>
	///     U (Until) - left component holds until right component holds
	///
	///     pUq - p holds until q holds
	/// </summary>
	public class U : ALtlComponent, ILineartimeTemporalLogic
	{
		public U() : base("U")
		{
		}

		public void AddLeft(ALtlComponent component)
		{
			_componentLeft = component;
		}

		public void AddRight(ALtlComponent component)
		{
			_componentRight = component;
		}

		public override string Display()
		{
			Console.WriteLine("G");
			return "G";
		}

		public override bool IsModelAndPathValid(StateComposite stateComposite, IList<string> path)
		{
			throw new NotImplementedException();
		}

		private ALtlComponent _componentLeft;
		private ALtlComponent _componentRight;
	}
}