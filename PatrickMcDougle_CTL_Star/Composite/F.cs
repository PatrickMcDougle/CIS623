using System;

namespace PatrickMcDougle_CTL_Star.Composite
{
	/// <summary>
	///     F (Future) class is that there is some future state in our model path.
	///
	///     F(phi) === ~G(~phi)
	///
	///     G(phi) === ~F(~phi)
	/// </summary>
	public class F : AComponent, ILineartimeTemporalLogic
	{
		private AComponent _componentRight;

		public F() : base("F")
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
			Console.WriteLine("F");
			return "F";
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