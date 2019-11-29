namespace PatrickMcDougle_CTL_Star.Composite
{
	public abstract class AComponent
	{
		protected string name;
		protected bool valid;

		protected AComponent(string name, bool valid = true)
		{
			this.name = name;
			this.valid = valid;
		}

		public abstract string Display();

		public abstract bool IsPathValid();
	}
}