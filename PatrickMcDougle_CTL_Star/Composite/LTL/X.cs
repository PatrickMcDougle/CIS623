using System;
using System.Text;
using PatrickMcDougle_CTL_Star.Composite.Model;

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
			StringBuilder sb = new StringBuilder();
			sb.Append("X(");
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