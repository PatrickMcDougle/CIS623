using System;
using System.Collections.Generic;
using System.Linq;
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
	///
	///     Gp - p holds globally in the future
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

			//bool isValid = true; //
			//StateComposite state = stateComposite; //
			//if (_componentRight is Proposition prop)
			//{ //
			//	foreach (var stateName in path)
			//	{ //
			//		isValid &= state.IsPropositionValid(prop.Display()); //

			// if (!isValid) { // if we find an invalid value (false) then no
			// need to // continue, but just return false.

			// return false; } //

			// state = stateComposite.GetNextValidState(stateName); //

			// if (state = null) { // if state is null than something went //
			// wrong. return false.

			// return false; } } //

			//	return isValid; //
			//} //

			var nextStateComposite =
			stateComposite.GetNextValidState(path[0]);

			path.RemoveAt(0);

			return _componentRight.IsModelAndPathValid(nextStateComposite, path);
		}

		private ALtlComponent _componentRight;
	}
}