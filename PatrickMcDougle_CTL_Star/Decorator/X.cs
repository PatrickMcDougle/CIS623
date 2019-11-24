using System;

namespace PatrickMcDougle_CTL_Star.Decorator
{
	/// <summary>
	/// X class is the next state in our model path.
	/// </summary>
	public class X : Decorator
	{
		public override void Operation()
		{
			base.Operation();
			Console.WriteLine("X");
		}
	}
}