using System;

namespace PatrickMcDougle_CTL_Star.Composite.LTL
{
	/// <summary>
	///     G (Globally) class is that all future state in our model path.
	///
	///     F(phi) === ~G(~phi)
	///
	///     G(phi) === ~F(~phi)
	/// </summary>
	public class G : ALtlComponent, ILineartimeTemporalLogic
	{
		public G() : base("G")
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
			Console.WriteLine("G");
			return "G";
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