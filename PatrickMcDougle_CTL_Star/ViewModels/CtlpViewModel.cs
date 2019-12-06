using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using PatrickMcDougle_CTL_Star.Composite.CTL;
using PatrickMcDougle_CTL_Star.Data;

namespace PatrickMcDougle_CTL_Star
{
	public class CtlpViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public IDictionary<int, IList<int>> BinaryRelations { get; set; } = new Dictionary<int, IList<int>>();
		public IDictionary<int, IList<string>> LabelingFunctions { get; set; } = new Dictionary<int, IList<string>>();

		public int NumberOfStates
		{
			get => (_ctlpData != null && _ctlpData.States != null) ? _ctlpData.States.Count : 0;
		}

		public IEnumerable<string> States
		{
			get => (_ctlpData != null && _ctlpData.States != null) ? _ctlpData.States : null;
		}

		public IEnumerable<string> StatesBinaryRelationFinish
		{
			get
			{
				if (_ctlpData != null && _ctlpData.States != null && _ctlpData.States.Any())
				{
					if (_ctlpData.BinaryRelations != null && _ctlpData.BinaryRelations.Any() && !string.IsNullOrWhiteSpace(_stateBinaryRelationStart))
					{
						return _ctlpData.BinaryRelations.Where(x => x.Start.Equals(_stateBinaryRelationStart)).Select(y => y.Finish).Distinct();
					}
					return _ctlpData.BinaryRelations.Select(y => y.Finish).Distinct();
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
				if (_ctlpData != null && _ctlpData.States != null && _ctlpData.States.Any())
				{
					if (_ctlpData.BinaryRelations != null && _ctlpData.BinaryRelations.Any() && !string.IsNullOrWhiteSpace(_stateBinaryRelationFinish))
					{
						return _ctlpData.BinaryRelations.Where(x => x.Finish.Equals(_stateBinaryRelationFinish)).Select(y => y.Start).Distinct();
					}
					return _ctlpData.BinaryRelations.Select(y => y.Start).Distinct();
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
				if (_ctlpData != null && _ctlpData.BinaryRelations != null)
				{
					StringBuilder sb = new StringBuilder();
					foreach (var item in _ctlpData.BinaryRelations)
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

		public string WriteCtlFormula
		{
			get
			{
				Proposition propP = new Proposition("p");
				Proposition propQ = new Proposition("q");
				Proposition propR = new Proposition("r");

				Conjunction conj = new Conjunction
				{
					CtlFormulaLeft = propP,
					CtlFormulaRight = propQ
				};

				Disjunction disj = new Disjunction
				{
					CtlFormulaLeft = propQ,
					CtlFormulaRight = propR
				};

				Implication impl = new Implication
				{
					CtlFormulaLeft = propP,
					CtlFormulaRight = propR
				};

				AF af = new AF
				{
					CtlFormulaRight = conj
				};

				EX ex = new EX
				{
					CtlFormulaRight = disj
				};

				Disjunction dis2 = new Disjunction
				{
					CtlFormulaLeft = af,
					CtlFormulaRight = ex
				};

				EU eu = new EU
				{
					CtlFormulaLeft = impl,
					CtlFormulaRight = dis2
				};

				return eu.Display();
			}
		}

		public string WriteLabelingFunctions
		{
			get
			{
				if (_ctlpData != null && _ctlpData.LabelingFunctions != null)
				{
					StringBuilder sb = new StringBuilder();
					foreach (var item in _ctlpData.LabelingFunctions)
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
				if (_ctlpData != null && _ctlpData.Path != null)
				{
					// \u03c0 is lowercase PI
					return "\u03C0 = " + string.Join("->", _ctlpData.Path.ToArray());
				}

				return string.Empty;
			}
		}

		public string WriteStates
		{
			get
			{
				if (_ctlpData != null && _ctlpData.States != null && !string.IsNullOrWhiteSpace(_ctlpData.InitialState))
				{
					return string
						.Join(" ", _ctlpData.States.ToArray())
						.Replace(_ctlpData.InitialState, _ctlpData.InitialState + "*");
				}

				return string.Empty;
			}
		}

		public void AddBinaryRelation(string start, string finish)
		{
			if (!string.IsNullOrWhiteSpace(start)
				&& !string.IsNullOrWhiteSpace(finish)
				&& !_ctlpData.BinaryRelations.Any(e => e.Finish.Equals(finish) && e.Start.Equals(start)))
			{
				_ctlpData.BinaryRelations.Add(new BinaryRelationData
				{
					Start = start,
					Finish = finish
				});
				OnPropertyChange(nameof(WriteBinaryRelations));
			}
		}

		public void AddState(string stateName)
		{
			if (!string.IsNullOrWhiteSpace(stateName) && !_ctlpData.States.Contains(stateName))
			{
				_ctlpData.States.Add(stateName);
				OnPropertyChange(nameof(WriteStates));
			}
		}

		public void DeleteBinaryRelation(string startState, string finishState)
		{
			if (!string.IsNullOrWhiteSpace(startState) && !string.IsNullOrWhiteSpace(finishState)
				&& _ctlpData.BinaryRelations.Any(x => x.Start.Equals(startState) && x.Finish.Equals(finishState)))
			{
				var item = _ctlpData.BinaryRelations.Where(x => x.Start.Equals(startState) && x.Finish.Equals(finishState)).ToArray()[0];
				_ctlpData.BinaryRelations.Remove(item);
				_stateBinaryRelationFinish = string.Empty;
				_stateBinaryRelationStart = string.Empty;
				OnPropertyChange(nameof(WriteBinaryRelations));
			}
		}

		public void DeleteState(string stateName)
		{
			if (!string.IsNullOrWhiteSpace(stateName) && _ctlpData.States.Contains(stateName))
			{
				_ctlpData.States.Remove(stateName);
				OnPropertyChange(nameof(WriteStates));
			}
		}

		public object GetModel()
		{
			return _ctlpData;
		}

		public void LoadModel(CtlpData ctlpData)
		{
			_ctlpData = ctlpData;

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
		private CtlpData _ctlpData;
		private string _stateBinaryRelationFinish = string.Empty;
		private string _stateBinaryRelationStart = string.Empty;
	}
}