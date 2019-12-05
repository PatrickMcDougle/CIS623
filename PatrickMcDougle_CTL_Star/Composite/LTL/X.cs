using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PatrickMcDougle_CTL_Star.Composite.Model;

namespace PatrickMcDougle_CTL_Star.Composite.LTL
{
	/// <summary>
	///     X (neXt) class is the next state in our model path.
	///
	///     Xp - p hold next time.
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
			sb.Append(name);
			sb.Append("(");
			sb.Append(_componentRight?.Display());
			sb.Append(")");
			Console.WriteLine(sb.ToString());
			return sb.ToString();
		}

		public override bool IsModelAndPathValid(StateComposite stateComposite, IList<string> path)
		{
			if (_componentRight == null)
			{
				return false;
			}

			if (path == null || path.Any())
			{
				return false;
			}

			var nextStateComposite =
			stateComposite.GetNextValidState(path[0]);

			path.RemoveAt(0);

			return _componentRight.IsModelAndPathValid(nextStateComposite, path);
		}

		private ALtlComponent _componentRight;
	}
}