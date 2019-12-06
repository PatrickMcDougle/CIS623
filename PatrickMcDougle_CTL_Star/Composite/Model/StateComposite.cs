using System;
using System.Collections.Generic;
using System.Linq;

namespace PatrickMcDougle_CTL_Star.Composite.Model
{
	public class StateComposite : AModelComponent
	{
		public StateComposite(string name) : base(name)
		{
		}

		public IList<StateComposite> ChildrenStates { get => _componentsChildren; }
		public IList<StateComposite> ParentStates { get => _componentsParents; }

		public void AddEdgeToChildState(StateComposite childState)
		{
			_componentsChildren.Add(childState);
		}

		public void AddEdgeToParentState(StateComposite ParentState)
		{
			_componentsParents.Add(ParentState);
		}

		public void AddProposition(string prop)
		{
			_propositions.Add(prop);
		}

		public override string Display()
		{
			throw new NotImplementedException();
		}

		public StateComposite GetNextValidState(string stateName)
		{
			return _componentsChildren.First(x => x.Name.Equals(stateName));
		}

		public bool IsPropositionValid(string name) => _propositions != null && _propositions.Contains(name);

		public bool RemoveEdgeToChildState(StateComposite childState)
		{
			return _componentsChildren.Remove(childState);
		}

		public bool RemoveEdgeToParentState(StateComposite parentState)
		{
			return _componentsParents.Remove(parentState);
		}

		public bool RemoveProposition(string prop)
		{
			return _propositions.Remove(prop);
		}
	}
}