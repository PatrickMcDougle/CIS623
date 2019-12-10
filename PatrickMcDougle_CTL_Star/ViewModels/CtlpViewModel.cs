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
		public CtlpViewModel()
		{
			_aCtlFormula = new EX
			{
				CtlFormulaRight = new Proposition("p")
			};
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public IDictionary<int, IList<int>> BinaryRelations { get; set; } = new Dictionary<int, IList<int>>();

		public string LabelCtlTruth
		{
			get
			{
				if (!string.IsNullOrWhiteSpace(Model.InitialState))
				{
					return $"M,{Model.InitialState} ⊨";
				}
				return "M,s0 ⊨";
			}
		}

		public IDictionary<int, IList<string>> LabelingFunctions { get; set; } = new Dictionary<int, IList<string>>();
		public CtlpData Model { get; private set; }

		public int NumberOfStates
		{
			get => (Model != null && Model.States != null) ? Model.States.Count : 0;
		}

		public IEnumerable<string> States
		{
			get => (Model != null && Model.States != null) ? Model.States : null;
		}

		public IEnumerable<string> StatesBinaryRelationFinish
		{
			get
			{
				if (Model != null && Model.States != null && Model.States.Any())
				{
					if (Model.BinaryRelations != null && Model.BinaryRelations.Any() && !string.IsNullOrWhiteSpace(_stateBinaryRelationStart))
					{
						return Model.BinaryRelations.Where(x => x.Start.Equals(_stateBinaryRelationStart)).Select(y => y.Finish).Distinct();
					}
					return Model.BinaryRelations.Select(y => y.Finish).Distinct();
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
				if (Model != null && Model.States != null && Model.States.Any())
				{
					if (Model.BinaryRelations != null && Model.BinaryRelations.Any() && !string.IsNullOrWhiteSpace(_stateBinaryRelationFinish))
					{
						return Model.BinaryRelations.Where(x => x.Finish.Equals(_stateBinaryRelationFinish)).Select(y => y.Start).Distinct();
					}
					return Model.BinaryRelations.Select(y => y.Start).Distinct();
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
				if (Model != null && Model.BinaryRelations != null)
				{
					StringBuilder sb = new StringBuilder();
					foreach (var item in Model.BinaryRelations)
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
				return _aCtlFormula.Display();
			}
		}

		public string WriteCtlStates
		{
			get
			{
				if (_modelInformation != null)
				{
					var a = _aCtlFormula
					.Satisfies(_modelInformation)
					.Select(s => s.Name)
					.ToArray();

					return string.Join(" ", a);
				}
				return string.Empty;
			}
		}

		public string WriteCtlTruth
		{
			get
			{
				if (_modelInformation == null)
				{
					return string.Empty;
				}

				if (!string.IsNullOrWhiteSpace(Model.InitialState))
				{
					return _aCtlFormula
						.Satisfies(_modelInformation)
						.Any(s => s.Name.Equals(Model.InitialState))
						.ToString();
				}
				return false.ToString();
			}
		}

		public string WriteLabelingFunctions
		{
			get
			{
				if (Model != null && Model.LabelingFunctions != null)
				{
					StringBuilder sb = new StringBuilder();
					foreach (var item in Model.LabelingFunctions)
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
				if (Model != null && Model.Path != null)
				{
					// \u03c0 is lowercase PI
					return "\u03C0 = " + string.Join("->", Model.Path.ToArray());
				}

				return string.Empty;
			}
		}

		public string WriteStates
		{
			get
			{
				if (Model != null && Model.States != null)
				{
					if (string.IsNullOrWhiteSpace(Model.InitialState))
					{
						return string.Join(" ", Model.States.ToArray());
					}

					return string.Join(" ", Model.States.ToArray())
						.Replace(Model.InitialState, Model.InitialState + "*");
				}

				return string.Empty;
			}
		}

		public void AddBinaryRelation(string start, string finish)
		{
			if (!string.IsNullOrWhiteSpace(start)
				&& !string.IsNullOrWhiteSpace(finish)
				&& !Model.BinaryRelations.Any(e => e.Finish.Equals(finish) && e.Start.Equals(start)))
			{
				Model.BinaryRelations.Add(new BinaryRelationData
				{
					Start = start,
					Finish = finish
				});
				OnPropertyChange(nameof(WriteBinaryRelations));
			}
		}

		public void AddCtlFormula(ACtlFormula constructedCtlFormula)
		{
			_aCtlFormula = constructedCtlFormula;
			OnPropertyChange(nameof(WriteCtlFormula));
			OnPropertyChange(nameof(WriteCtlStates));
			OnPropertyChange(nameof(WriteCtlTruth));
		}

		public void AddPropositions(string stateName, string propositionString)
		{
			if (Model.LabelingFunctions.Any(l => l.State.Equals(stateName)))
			{
				var propList = propositionString.Split(',');
				var labelFunction = Model.LabelingFunctions.First(x => x.State.Equals(stateName));

				foreach (var prop in propList)
				{
					var t = prop.Trim();
					if (!labelFunction.Propositions.Any(x => x.Equals(t)))
					{
						labelFunction.Propositions.Add(t);
					}
				}
				OnPropertyChange(nameof(WriteLabelingFunctions));
			}
		}

		public void AddState(string stateName)
		{
			if (!string.IsNullOrWhiteSpace(stateName) && !Model.States.Contains(stateName))
			{
				Model.States.Add(stateName);
				OnPropertyChange(nameof(WriteStates));
			}
		}

		public void DeleteBinaryRelation(string startState, string finishState)
		{
			if (!string.IsNullOrWhiteSpace(startState) && !string.IsNullOrWhiteSpace(finishState)
				&& Model.BinaryRelations.Any(x => x.Start.Equals(startState) && x.Finish.Equals(finishState)))
			{
				var item = Model.BinaryRelations.Where(x => x.Start.Equals(startState) && x.Finish.Equals(finishState)).ToArray()[0];
				Model.BinaryRelations.Remove(item);
				_stateBinaryRelationFinish = string.Empty;
				_stateBinaryRelationStart = string.Empty;
				OnPropertyChange(nameof(WriteBinaryRelations));
			}
		}

		public void DeleteState(string stateName)
		{
			if (!string.IsNullOrWhiteSpace(stateName) && Model.States.Contains(stateName))
			{
				Model.States.Remove(stateName);
				OnPropertyChange(nameof(WriteStates));
			}
		}

		public void DelPropositions(string stateName, string propositionString)
		{
			if (Model.LabelingFunctions.Any(l => l.State.Equals(stateName)))
			{
				var propList = propositionString.Split(',');
				var labelFunction = Model.LabelingFunctions.First(x => x.State.Equals(stateName));

				foreach (var prop in propList)
				{
					var t = prop.Trim();
					if (labelFunction.Propositions.Any(x => x.Equals(t)))
					{
						labelFunction.Propositions.Remove(t);
					}
				}
				OnPropertyChange(nameof(WriteLabelingFunctions));
			}
		}

		public void InitialState(string initialState)
		{
			if (string.IsNullOrWhiteSpace(initialState))
			{
				Model.InitialState = string.Empty;
			}
			else if (Model.States.Any(s => s.Equals(initialState)))
			{
				Model.InitialState = initialState;
			}
			else
			{
				Model.InitialState = string.Empty;
			}
			OnPropertyChange(nameof(WriteStates));
			OnPropertyChange(nameof(WriteCtlTruth));
			OnPropertyChange(nameof(LabelCtlTruth));
		}

		public void LoadModel(CtlpData ctlpData)
		{
			Model = ctlpData;

			OnPropertyChange(nameof(States));
			OnPropertyChange(nameof(WriteStates));
			OnPropertyChange(nameof(WriteBinaryRelations));
			OnPropertyChange(nameof(WriteLabelingFunctions));
			OnPropertyChange(nameof(WritePath));
			OnPropertyChange(nameof(WriteCtlFormula));
			OnPropertyChange(nameof(WriteCtlStates));
			OnPropertyChange(nameof(WriteCtlTruth));
			OnPropertyChange(nameof(LabelCtlTruth));
		}

		public void LoadModelInfo(ModelInformation modelInformation)
		{
			_modelInformation = modelInformation;
			OnPropertyChange(nameof(WriteCtlFormula));
			OnPropertyChange(nameof(WriteCtlStates));
			OnPropertyChange(nameof(WriteCtlTruth));
			OnPropertyChange(nameof(LabelCtlTruth));
		}

		protected void OnPropertyChange([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		private readonly IEnumerable<string> _empltyStringList = new List<string>();
		private ACtlFormula _aCtlFormula;
		private ModelInformation _modelInformation;
		private string _stateBinaryRelationFinish = string.Empty;
		private string _stateBinaryRelationStart = string.Empty;
	}
}