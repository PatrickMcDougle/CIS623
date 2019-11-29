using System.Collections.Generic;

namespace PatrickMcDougle_CTL_Star.Models
{
	public class LabelingFunctionModel
	{
		public IList<string> Propositions { get; set; } = new List<string>();
		public string State { get; set; } = "";
	}
}