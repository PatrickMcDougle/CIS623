using System.Collections.Generic;

namespace PatrickMcDougle_CTL_Star.Composite.Model
{
	public abstract class AModelComponent
	{
		public string Name { get => name; }

		public abstract string Display();

		protected readonly IList<AModelComponent> _components = new List<AModelComponent>();
		protected readonly IList<string> _propositions = new List<string>();
		protected string name;

		protected AModelComponent(string name)
		{
			this.name = name;
		}
	}
}