using System;

namespace PatrickMcDougle_CTL_Star.Composite.LTL
{
	/// <summary>
	///     F (Future) class is that there is some future state in our model path.
	///
	///     F(phi) === ~G(~phi)
	///
	///     G(phi) === ~F(~phi)
	/// </summary>
	public class F : ALtlComponent, ILineartimeTemporalLogic
	{
		public F() : base("F")
		{
		}

		public void AddLeft(ALtlComponent component)
		{
			throw new NotImplementedException();
		}

		public void AddRight(ALtlComponent component)
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

		public void Remove(ALtlComponent component)
		{
			throw new NotImplementedException();
		}

		private ALtlComponent _componentRight;
	}
}