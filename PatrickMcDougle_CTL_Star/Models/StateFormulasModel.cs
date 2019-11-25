using System.Collections.Generic;

namespace PatrickMcDougle_CTL_Star.Models
{
	public class StateFormulasModel
	{
		public IList<string> Formulas { get; set; } = new List<string>();
		public string State { get; set; } = "";
	}
}