using System.Collections.Generic;
using PatrickMcDougle_CTL_Star.Composite.Model;

namespace PatrickMcDougle_CTL_Star.Data
{
	public class ModelInformation
	{
		public IList<StateComposite> AllStates { get; set; }
		public StateComposite CurrentState { get; set; }
	}
}