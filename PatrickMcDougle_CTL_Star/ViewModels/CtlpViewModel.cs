using Newtonsoft.Json;
using PatrickMcDougle_CTL_Star.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace PatrickMcDougle_CTL_Star
{
	public class CtlpViewModel : INotifyPropertyChanged
	{
		private CtlpModel _ctlpModel;

		public event PropertyChangedEventHandler PropertyChanged;

		public string EdgesListJson
		{
			get
			{
				if (_ctlpModel != null && _ctlpModel.Edges != null)
				{
					StringBuilder sb = new StringBuilder();
					sb.Append("{");
					foreach (var item in _ctlpModel.Edges)
					{
						if (sb.Length > 6)
						{
							sb.Append(", ");
						}
						sb.Append("[");
						sb.Append(item.Start);
						sb.Append("->");
						sb.Append(item.Finish);
						sb.Append("]");
					}
					sb.Append("}");
					return sb.ToString();
				}

				return string.Empty;
			}
		}

		public string FormulasPerStateListJson
		{
			get
			{
				if (_ctlpModel != null && _ctlpModel.StateFormulas != null)
				{
					StringBuilder sb = new StringBuilder();
					sb.Append("{");
					foreach (var item in _ctlpModel.StateFormulas)
					{
						if (sb.Length > 6)
						{
							sb.Append(", ");
						}
						sb.Append("[");
						sb.Append(item.State);
						sb.Append("=>|");
						sb.Append(string.Join(",", item.Formulas));
						sb.Append("|]");
					}
					sb.Append("}");
					return sb.ToString();
				}

				return string.Empty;
			}
		}

		public IDictionary<int, IList<int>> ListOfEdges { get; set; } = new Dictionary<int, IList<int>>();

		public int NumberOfStates
		{
			get => (_ctlpModel != null && _ctlpModel.States != null) ? _ctlpModel.States.Count : 0;
		}

		public IDictionary<int, IList<string>> StateProperties { get; set; } = new Dictionary<int, IList<string>>();

		public IEnumerable<string> States
		{
			get => (_ctlpModel != null && _ctlpModel.States != null) ? _ctlpModel.States : null;
		}

		public void AddState(string stateName)
		{
			if (!string.IsNullOrWhiteSpace(stateName) && !_ctlpModel.States.Contains(stateName))
			{
				_ctlpModel.States.Add(stateName);
				OnPropertyChange(nameof(StatesListJson));
			}
		}

		public string StatesListJson
		{
			get
			{
				if (_ctlpModel != null && _ctlpModel.States != null)
				{
					return JsonConvert.SerializeObject(_ctlpModel.States, Formatting.None);
				}

				return null;
			}
		}

		internal void DeleteState(string stateName)
		{
			if (!string.IsNullOrWhiteSpace(stateName) && _ctlpModel.States.Contains(stateName))
			{
				_ctlpModel.States.Remove(stateName);
				OnPropertyChange(nameof(StatesListJson));
			}
		}

		internal void AddEdge(string start, string finish)
		{
			if (!string.IsNullOrWhiteSpace(start) && !string.IsNullOrWhiteSpace(finish))
			{
				if (!_ctlpModel.Edges.Any(e => e.Finish.Equals(finish) && e.Start.Equals(start)))
				{
					_ctlpModel.Edges.Add(new EdgeModel
					{
						Start = start,
						Finish = finish
					});
					OnPropertyChange(nameof(EdgesListJson));
				}
			}
		}

		internal object GetModel()
		{
			return _ctlpModel;
		}

		internal void LoadModel(CtlpModel ctlpModel)
		{
			_ctlpModel = ctlpModel;

			OnPropertyChange(nameof(States));
			OnPropertyChange(nameof(StatesListJson));
			OnPropertyChange(nameof(EdgesListJson));
			OnPropertyChange(nameof(FormulasPerStateListJson));
		}

		protected void OnPropertyChange([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}