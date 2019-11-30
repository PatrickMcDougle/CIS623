using System.Collections.Generic;

namespace PatrickMcDougle_CTL_Star.Data
{
	public class CtlpData
	{
		public IList<BinaryRelationData> BinaryRelations { get; set; } = new List<BinaryRelationData>();
		public string InitialState { get; set; } = "";
		public IList<LabelingFunctionData> LabelingFunctions { get; set; } = new List<LabelingFunctionData>();
		public IList<string> Path { get; set; } = new List<string>();
		public IList<string> Propositions { get; set; } = new List<string>();
		public IList<string> States { get; set; } = new List<string>();
	}
}