using System;

namespace PatrickMcDougle_CTL_Star.Composite
{
	/// <summary>
	///     X (neXt) class is the next state in our model path.
	/// </summary>
	public class X : AComponent, ILineartimeTemporalLogic
	{
		private AComponent _componentRight;

		public X() : base("X")
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
			Console.WriteLine("X");
			return "X";
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