using System;
using System.Text;
using PatrickMcDougle_CTL_Star.Composite.Model;

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
			StringBuilder sb = new StringBuilder();
			sb.Append("F(");
			if (_componentRight != null)
			{
				sb.Append(_componentRight.Display());
			}

			sb.Append(")");
			return sb.ToString();
		}

		public override bool IsLogicValid(StateComposite stateComposite)
		{
			throw new NotImplementedException();
		}

		private ALtlComponent _componentRight;
	}
}