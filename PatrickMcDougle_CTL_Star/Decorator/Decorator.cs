namespace PatrickMcDougle_CTL_Star.Decorator
{
	public class Decorator : IComponent
	{
		protected IComponent _component;

		public virtual void Operation()
		{
			if (_component != null)
			{
				_component.Operation();
			}
		}

		public void SetComponent(IComponent component)
		{
			_component = component;
		}
	}
}