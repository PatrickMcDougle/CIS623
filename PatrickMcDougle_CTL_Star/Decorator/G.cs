using System;

namespace PatrickMcDougle_CTL_Star.Decorator
{
	/// <summary>
	/// G (Globally) class is that all future state in our model path.
	/// 
	/// F(phi) === ~G(~phi)
	/// 
	/// G(phi) === ~F(~phi)
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