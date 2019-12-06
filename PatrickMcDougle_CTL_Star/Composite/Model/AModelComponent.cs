using System.Collections.Generic;

namespace PatrickMcDougle_CTL_Star.Composite.Model
{
	public abstract class AModelComponent
	{
		public string Name { get => name; }

		public bool AddLabel(string label)
		{
			if (DoesLabelExist(label))
			{
				return false;
			}

			Labels.Add(label);
			return true;
		}

		public bool ClearAllLabels()
		{
			Labels.Clear();
			return true;
		}

		public abstract string Display();

		public bool DoesLabelExist(string label)
		{
			return Labels.Contains(label);
		}

		public bool RemoveLabel(string label)
		{
			if (DoesLabelExist(label))
			{
				Labels.Remove(label);
				return Labels.Remove(label);
			}

			return false;
		}

		protected readonly IList<StateComposite> _componentsChildren = new List<StateComposite>();
		protected readonly IList<StateComposite> _componentsParents = new List<StateComposite>();
		protected readonly IList<string> _propositions = new List<string>();
		protected readonly IList<string> Labels = new List<string>();
		protected string name;

		protected AModelComponent(string name)
		{
			this.name = name;
		}
	}
}