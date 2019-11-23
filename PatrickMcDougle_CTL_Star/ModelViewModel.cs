using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace PatrickMcDougle_CTL_Star
{
	public class ModelViewModel : INotifyPropertyChanged
	{
		private int _numberOfStates = 3;
		private List<string> _states = new List<string>();

		public event PropertyChangedEventHandler PropertyChanged;

		public IDictionary<int, IList<int>> ListOfEdges { get; set; } = new Dictionary<int, IList<int>>();

		public int NumberOfStates
		{
			get => _numberOfStates;
			set
			{
				_numberOfStates = (value >= 3) ? value : 3;
				OnPropertyChange();
				OnPropertyChange("States");
				OnPropertyChange("StatesListJson");
			}
		}

		public IDictionary<int, IList<string>> StateProperties { get; set; } = new Dictionary<int, IList<string>>();

		public IEnumerable<string> States
		{
			get
			{
				_states = new List<string>();
				for (int i = 0; i < _numberOfStates; ++i)
				{
					_states.Add($"s{i}");
				}
				return _states;
			}
		}

		public string StatesListJson
		{
			get
			{
				StringBuilder sb = new StringBuilder();
				sb.Append("{ \"States\" : [");

				sb.Append(string.Join(",", _states.ToArray()));

				sb.Append("] }");

				return sb.ToString();
			}
		}

		protected void OnPropertyChange([CallerMemberName] string propertyName = "")
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}