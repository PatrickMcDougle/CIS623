using System.Collections.Generic;
using System.Linq;
using PatrickMcDougle_CTL_Star.Composite.Model;
using PatrickMcDougle_CTL_Star.Data;

namespace PatrickMcDougle_CTL_Star.Factories
{
	public class ModelFactory
	{
		public ModelInformation CreateModel(CtlpData ctlpData)
		{
			ModelInformation modelInformation = new ModelInformation();
			CreateStates(ctlpData.States);

			AddEdges(ctlpData.BinaryRelations);

			AddPropositions(ctlpData.LabelingFunctions);

			// return initial state

			if (_states.Count > 0)
			{
				modelInformation.AllStates = _states;

				if (!string.IsNullOrWhiteSpace(ctlpData.InitialState) && _states.Any(x => x.Name.Equals(ctlpData.InitialState)))
				{
					modelInformation.CurrentState = _states.First(x => x.Name.Equals(ctlpData.InitialState));
				}
			}

			return modelInformation;
		}

		private readonly IList<StateComposite> _states = new List<StateComposite>();

		private void AddEdges(IList<BinaryRelationData> binaryRelations)
		{
			// add the edges/binary relations
			foreach (var item in binaryRelations)
			{
				var stateStart = _states.FirstOrDefault(x => x.Name.Equals(item.Start));
				var stateFinish = _states.FirstOrDefault(x => x.Name.Equals(item.Finish));

				stateStart.AddEdgeToChildState(stateFinish);
				stateFinish.AddEdgeToParentState(stateStart);
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