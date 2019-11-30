using System;

namespace PatrickMcDougle_CTL_Star.Composite.LTL
{
	/// <summary>
	///     X (neXt) class is the next state in our model path.
	/// </summary>
	public class X : ALtlComponent, ILineartimeTemporalLogic
	{
		public X() : base("X")
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
			Console.WriteLine("X");
			return "X";
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