using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using PatrickMcDougle_CTL_Star.Models;

namespace PatrickMcDougle_CTL_Star
{
	public class CtlpViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public IDictionary<int, IList<int>> BinaryRelations { get; set; } = new Dictionary<int, IList<int>>();
		public IDictionary<int, IList<string>> LabelingFunctions { get; set; } = new Dictionary<int, IList<string>>();

		public int NumberOfStates
		{
			get => (_ctlpModel != null && _ctlpModel.States != null) ? _ctlpModel.States.Count : 0;
		}

		public IEnumerable<string> States
		{
			get => (_ctlpModel != null && _ctlpModel.States != null) ? _ctlpModel.States : null;
		}

		public IEnumerable<string> StatesBinaryRelationFinish
		{
			get
			{
				if (_ctlpModel != null && _ctlpModel.States != null && _ctlpModel.States.Any())
				{
					if (_ctlpModel.BinaryRelations != null && _ctlpModel.BinaryRelations.Any() && !string.IsNullOrWhiteSpace(_stateBinaryRelationStart))
					{
						return _ctlpModel.BinaryRelations.Where(x => x.Start.Equals(_stateBinaryRelationStart)).Select(y => y.Finish).Distinct();
					}
					return _ctlpModel.BinaryRelations.Select(y => y.Finish).Distinct();
				}
				return _empltyStringList;
			}
			set
			{
				if (value.Any())
				{
					_stateBinaryRelationFinish = value.FirstOrDefault();
					OnPropertyChange(nameof(StatesBinaryRelationStart));
				}
			}
		}

		public IEnumerable<string> StatesBinaryRelationStart
		{
			get
			{
				if (_ctlpModel != null && _ctlpModel.States != null && _ctlpModel.States.Any())
				{
					if (_ctlpModel.BinaryRelations != null && _ctlpModel.BinaryRelations.Any() && !string.IsNullOrWhiteSpace(_stateBinaryRelationFinish))
					{
						return _ctlpModel.BinaryRelations.Where(x => x.Finish.Equals(_stateBinaryRelationFinish)).Select(y => y.Start).Distinct();
					}
					return _ctlpModel.BinaryRelations.Select(y => y.Start).Distinct();
				}
				return _empltyStringList;
			}
			set
			{
				if (value.Any())
				{
					_stateBinaryRelationStart = value.FirstOrDefault();
					OnPropertyChange(nameof(StatesBinaryRelationFinish));
				}
			}
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

		public string WritePath
		{
			get
			{
				if (_ctlpModel != null && _ctlpModel.Path != null)
				{
					return string.Join("->", _ctlpModel.Path.ToArray());
				}

				return string.Empty;
			}
		}

		public string WriteStates
		{
			get
			{
				if (_ctlpModel != null && _ctlpModel.States != null && !string.IsNullOrWhiteSpace(_ctlpModel.InitialState))
				{
					return string
						.Join(" ", _ctlpModel.States.ToArray())
						.Replace(_ctlpModel.InitialState, _ctlpModel.InitialState + "*");
				}

				return string.Empty;
			}
		}

		public void AddBinaryRelation(string start, string finish)
		{
			if (!string.IsNullOrWhiteSpace(start)
				&& !string.IsNullOrWhiteSpace(finish)
				&& !_ctlpModel.BinaryRelations.Any(e => e.Finish.Equals(finish) && e.Start.Equals(start)))
			{
				_ctlpModel.BinaryRelations.Add(new BinaryRelationModel
				{
					Start = start,
					Finish = finish
				});
				OnPropertyChange(nameof(WriteBinaryRelations));
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

		public void DeleteBinaryRelation(string startState, string finishState)
		{
			if (!string.IsNullOrWhiteSpace(startState) && !string.IsNullOrWhiteSpace(finishState)
				&& _ctlpModel.BinaryRelations.Any(x => x.Start.Equals(startState) && x.Finish.Equals(finishState)))
			{
				var item = _ctlpModel.BinaryRelations.Where(x => x.Start.Equals(startState) && x.Finish.Equals(finishState)).ToArray()[0];
				_ctlpModel.BinaryRelations.Remove(item);
				_stateBinaryRelationFinish = string.Empty;
				_stateBinaryRelationStart = string.Empty;
				OnPropertyChange(nameof(WriteBinaryRelations));
			}
		}

		public void DeleteState(string stateName)
		{
			if (!string.IsNullOrWhiteSpace(stateName) && _ctlpModel.States.Contains(stateName))
			{
				_ctlpModel.States.Remove(stateName);
				OnPropertyChange(nameof(WriteStates));
			}
		}

		public object GetModel()
		{
			return _ctlpModel;
		}

		public void LoadModel(CtlpModel ctlpModel)
		{
			_ctlpModel = ctlpModel;

			OnPropertyChange(nameof(States));
			OnPropertyChange(nameof(WriteStates));
			OnPropertyChange(nameof(WriteBinaryRelations));
			OnPropertyChange(nameof(WriteLabelingFunctions));
			OnPropertyChange(nameof(WritePath));
		}

		protected void OnPropertyChange([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		private readonly IEnumerable<string> _empltyStringList = new List<string>();
		private CtlpModel _ctlpModel;
		private string _stateBinaryRelationFinish = string.Empty;
		private string _stateBinaryRelationStart = string.Empty;
	}
}