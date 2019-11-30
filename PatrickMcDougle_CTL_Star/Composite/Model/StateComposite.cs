using System;

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
	}
}