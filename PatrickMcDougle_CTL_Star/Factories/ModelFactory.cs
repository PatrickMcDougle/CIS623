using System.Collections.Generic;
using System.Linq;
using PatrickMcDougle_CTL_Star.Composite.Model;
using PatrickMcDougle_CTL_Star.Data;

namespace PatrickMcDougle_CTL_Star.Factories
{
	public class ModelFactory
	{
		public StateComposite CreateModel(CtlpData ctlpData)
		{
			CreateStates(ctlpData.States);

			AddEdges(ctlpData.BinaryRelations);

			AddPropositions(ctlpData.LabelingFunctions);

			// return initial state

			if (!string.IsNullOrWhiteSpace(ctlpData.InitialState) && _states.Any(x => x.Name.Equals(ctlpData.InitialState)))
			{
				return _states.First(x => x.Name.Equals(ctlpData.InitialState));
			}

			if (_states.Count > 0)
			{
				return _states[0];
			}

			return null;
		}

		private readonly IList<StateComposite> _states = new List<StateComposite>();

		private void AddEdges(IList<BinaryRelationData> binaryRelations)
		{
			// add the edges/binary relations
			foreach (var item in binaryRelations)
			{
				var stateStart = _states.FirstOrDefault(x => x.Name.Equals(item.Start));
				var stateFinish = _states.FirstOrDefault(x => x.Name.Equals(item.Finish));

				stateStart.AddEdgeToState(stateFinish);
			}
		}

		private void AddPropositions(IList<LabelingFunctionData> labelingFunctions)
		{
			// add the propositions
			foreach (var item in labelingFunctions)
			{
				var state = _states.FirstOrDefault(x => x.Name.Equals(item.State));
				foreach (var prop in item.Propositions)
				{
					state.AddProposition(prop);
				}
			}
		}

		private void CreateStates(IList<string> states)
		{
			// create the states.
			foreach (var item in states)
			{
				_states.Add(new StateComposite(item));
			}
		}
	}
}