using System;

namespace PatrickMcDougle_CTL_Star.Composite
{
	/// <summary>
	/// G (Globally) class is that all future state in our model path.
	///
	/// F(phi) === ~G(~phi)
	///
	/// G(phi) === ~F(~phi)
	/// </summary>
	public class G : AComponent, ILineartimeTemporalLogic
	{
		private AComponent _componentRight;

		public G() : base("G")
		{
		}

		public void AddLeft(AComponent component)
		{
			throw new NotImplementedException();
		}

		public void AddRight(AComponent component)
		{
			_componentRight = component;
		}

		public override string Display()
		{
			Console.WriteLine("G");
			return "G";
		}

		public override bool IsPathValid()
		{
			throw new NotImplementedException();
		}

		public void Remove(AComponent component)
		{
			throw new NotImplementedException();
		}
	}
}