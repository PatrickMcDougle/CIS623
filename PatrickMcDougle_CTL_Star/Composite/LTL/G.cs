using System;
using System.Text;
using PatrickMcDougle_CTL_Star.Composite.Model;

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
			StringBuilder sb = new StringBuilder();
			sb.Append("G(");
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