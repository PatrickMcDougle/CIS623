using System;
using System.Linq;

namespace PatrickMcDougle_CTL_Star.Composite.Model
{
	public class StateComposite : AModelComponent
	{
		public StateComposite(string name) : base(name)
		{
		}

		public void AddEdgeToState(AModelComponent aModelComponent)
		{
			_components.Add(aModelComponent);
		}

		public void AddProposition(string prop)
		{
			_propositions.Add(prop);
		}

		public override string Display()
		{
			throw new NotImplementedException();
		}

		public bool DoesPropositionValid(string name) => _propositions != null && _propositions.Contains(name);

		public StateComposite GetNextValidState(string stateName)
		{
			return _components.First(x => x.Name.Equals(stateName)) as StateComposite;
		}
	}
}