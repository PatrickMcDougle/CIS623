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
			IList<StateComposite> States = new List<StateComposite>();

			// create the states.
			foreach (var item in ctlpData.States)
			{
				States.Add(new StateComposite(item));
			}

			// add the edges/binary relations
			foreach (var item in ctlpData.BinaryRelations)
			{
				var stateStart = States.FirstOrDefault(x => x.Name.Equals(item.Start));
				var stateFinish = States.FirstOrDefault(x => x.Name.Equals(item.Finish));

				stateStart.AddEdgeToState(stateFinish);
			}

			// add the propositions
			foreach (var item in ctlpData.LabelingFunctions)
			{
				var state = States.FirstOrDefault(x => x.Name.Equals(item.State));
				foreach (var prop in item.Propositions)
				{
					state.AddProposition(prop);
				}
			}

			// return initial state

			if (!string.IsNullOrWhiteSpace(ctlpData.InitialState) && States.Any(x => x.Name.Equals(ctlpData.InitialState)))
			{
				return States.First(x => x.Name.Equals(ctlpData.InitialState));
			}

			if (States.Count > 0)
			{
				return States[0];
			}

			return null;
		}
	}
}