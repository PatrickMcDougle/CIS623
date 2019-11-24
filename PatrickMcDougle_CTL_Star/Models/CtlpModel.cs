using System.Collections.Generic;

namespace PatrickMcDougle_CTL_Star.Models
{
	public class CtlpModel
	{
		public IList<EdgeModel> Edges { get; set; }
		public IList<string> States { get; set; }
		public IList<StateFormulasModel> StateFormulas { get; set; }
	}
}