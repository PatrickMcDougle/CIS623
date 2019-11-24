using System;

namespace PatrickMcDougle_CTL_Star.Decorator
{
	/// <summary>
	/// F class is the some future state in our model path.
	/// </summary>
	public class F : Decorator
	{
		public override void Operation()
		{
			base.Operation();
			Console.WriteLine("F");
		}
	}
}