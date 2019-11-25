using System.Collections.Generic;

namespace PatrickMcDougle_CTL_Star.Models
{
	public class CtlpModel
	{
		public IList<EdgeModel> Edges { get; set; } = new List<EdgeModel>();
		public IList<StateFormulasModel> StateFormulas { get; set; } = new List<StateFormulasModel>();
		public IList<string> States { get; set; } = new List<string>();
	}
}