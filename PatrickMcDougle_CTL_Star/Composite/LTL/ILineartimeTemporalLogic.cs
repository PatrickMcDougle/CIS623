using System.Collections.Generic;
using PatrickMcDougle_CTL_Star.Composite.Model;

namespace PatrickMcDougle_CTL_Star.Composite.LTL
{
	public interface ILineartimeTemporalLogic
	{
		void AddLeft(ALtlComponent component);

		void AddRight(ALtlComponent component);

		string Display();

		bool IsModelAndPathValid(StateComposite stateComposite, IList<string> path);
	}
}