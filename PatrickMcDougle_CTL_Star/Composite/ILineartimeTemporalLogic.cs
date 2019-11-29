namespace PatrickMcDougle_CTL_Star.Composite
{
	public interface ILineartimeTemporalLogic
	{
		void AddLeft(AComponent component);

		void AddRight(AComponent component);

		void Remove(AComponent component);
	}
}