using System.Collections.Generic;

namespace PatrickMcDougle_CTL_Star.Models
{
	public class CtlpModel
	{
		public IList<BinaryRelationModel> BinaryRelations { get; set; } = new List<BinaryRelationModel>();
		public string InitialState { get; set; } = "";
		public IList<LabelingFunctionModel> LabelingFunctions { get; set; } = new List<LabelingFunctionModel>();
		public IList<string> Path { get; set; } = new List<string>();
		public IList<string> Propositions { get; set; } = new List<string>();
		public IList<string> States { get; set; } = new List<string>();
	}
}