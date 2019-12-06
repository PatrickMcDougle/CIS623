using System.Collections.Generic;
using PatrickMcDougle_CTL_Star.Composite.Model;

namespace PatrickMcDougle_CTL_Star.Data
{
	public class ModelInformation
	{
		public IList<StateComposite> AllStates { get => _allStates; }
		public StateComposite CurrentState { get; internal set; }

		private readonly IList<StateComposite> _allStates = new List<StateComposite>();
	}
}