using System;

namespace PatrickMcDougle_CTL_Star.Decorator
{
	/// <summary>
	/// G class is the all future state in our model path.
	/// </summary>
	public class G : Decorator
	{
		public override void Operation()
		{
			base.Operation();
			Console.WriteLine("G");
		}
	}
}