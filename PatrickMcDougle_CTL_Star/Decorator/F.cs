using System;

namespace PatrickMcDougle_CTL_Star.Decorator
{
	/// <summary>
	/// F (Future) class is that there is some future state in our model path.
	/// 
	/// F(phi) === ~G(~phi)
	/// 
	/// G(phi) === ~F(~phi)
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