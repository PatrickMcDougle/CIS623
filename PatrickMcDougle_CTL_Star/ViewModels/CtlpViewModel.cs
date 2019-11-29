using Newtonsoft.Json;
using PatrickMcDougle_CTL_Star.Models;
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

		public IDictionary<int, IList<int>> BinaryRelations { get; set; } = new Dictionary<int, IList<int>>();

		public int NumberOfStates
		{
			get => (_ctlpModel != null && _ctlpModel.States != null) ? _ctlpModel.States.Count : 0;
		}

		public IDictionary<int, IList<string>> LabelingFunctions { get; set; } = new Dictionary<int, IList<string>>();

		public IEnumerable<string> States
		{
			get => (_ctlpModel != null && _ctlpModel.States != null) ? _ctlpModel.States : null;
		}

		public string WriteBinaryRelations
		{
			get
			{
				if (_ctlpModel != null && _ctlpModel.BinaryRelations != null)
				{
					StringBuilder sb = new StringBuilder();
					foreach (var item in _ctlpModel.BinaryRelations)
					{
						if (sb.Length > 3)
						{
							sb.Append(",  ");
						}
						sb.Append(item.Start);
						sb.Append("->");
						sb.Append(item.Finish);
					}
					return sb.ToString();
				}

				return string.Empty;
			}
		}

		public string WriteLabelingFunctions
		{
			get
			{
				if (_ctlpModel != null && _ctlpModel.LabelingFunctions != null)
				{
					StringBuilder sb = new StringBuilder();
					foreach (var item in _ctlpModel.LabelingFunctions)
					{
						if (sb.Length > 6)
						{
							sb.Append(",  ");
						}
						sb.Append("L(");
						sb.Append(item.State);
						sb.Append(")={");
						sb.Append(string.Join(",", item.Propositions));
						sb.Append("}");
					}
					return sb.ToString();
				}

				return string.Empty;
			}
		}

		public string WriteStates
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

		public void AddState(string stateName)
		{
			if (!string.IsNullOrWhiteSpace(stateName) && !_ctlpModel.States.Contains(stateName))
			{
				_ctlpModel.States.Add(stateName);
				OnPropertyChange(nameof(WriteStates));
			}
		}

		internal void AddEdge(string start, string finish)
		{
			if (!string.IsNullOrWhiteSpace(start) && !string.IsNullOrWhiteSpace(finish))
			{
				if (!_ctlpModel.BinaryRelations.Any(e => e.Finish.Equals(finish) && e.Start.Equals(start)))
				{
					_ctlpModel.BinaryRelations.Add(new BinaryRelationModel
					{
						Start = start,
						Finish = finish
					});
					OnPropertyChange(nameof(WriteBinaryRelations));
				}
			}
		}

		internal void DeleteState(string stateName)
		{
			if (!string.IsNullOrWhiteSpace(stateName) && _ctlpModel.States.Contains(stateName))
			{
				_ctlpModel.States.Remove(stateName);
				OnPropertyChange(nameof(WriteStates));
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
			OnPropertyChange(nameof(WriteStates));
			OnPropertyChange(nameof(WriteBinaryRelations));
			OnPropertyChange(nameof(WriteLabelingFunctions));
		}

		protected void OnPropertyChange([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}