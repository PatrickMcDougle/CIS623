using PatrickMcDougle_CTL_Star.Composite.Model;

namespace PatrickMcDougle_CTL_Star.Composite.LTL
{
	public abstract class ALtlComponent
	{
		public abstract string Display();

		public abstract bool IsLogicValid(StateComposite stateComposite);

		protected string name;
		protected bool valid;

		protected ALtlComponent(string name, bool valid = true)
		{
			this.name = name;
			this.valid = valid;
		}
	}
}