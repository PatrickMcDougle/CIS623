using System.Collections.Generic;

namespace PatrickMcDougle_CTL_Star.Data
{
	public class LabelingFunctionData
	{
		public IList<string> Propositions { get; set; } = new List<string>();
		public string State { get; set; } = "";
	}
}