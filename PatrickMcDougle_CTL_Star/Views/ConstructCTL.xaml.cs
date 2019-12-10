using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using PatrickMcDougle_CTL_Star.Composite.CTL;

namespace PatrickMcDougle_CTL_Star
{
	/// <summary>
	///     Interaction logic for ConstructCTL.xaml
	/// </summary>
	public partial class ConstructCTL : Window
	{
		public ConstructCTL()
		{
			InitializeComponent();
		}

		public ACtlFormula ConstructedCtlFormula { get => _ctlFormula; }
		private ACtlFormula _ctlFormula;

		private void Button_Add_Click(object sender, RoutedEventArgs e)
		{
			if (sender is Button button)
			{
				ACtlFormula aCtlFormula = GetACtlFormula(TheCtlFormula.Text);

				if (_ctlFormula == null)
				{
					_ctlFormula = aCtlFormula;
				}
				else
				{
					RecursiveSearchForNull(_ctlFormula, aCtlFormula);
				}

				var theSolution = _ctlFormula.Display();

				string[] solutionParts = Regex.Split(theSolution, "__");

				if (solutionParts.Length > 1)
				{
					TheCurrentSolutionP1.Text = solutionParts[0];
					TheCurrentSolutionP2.Text = "__";
					StringBuilder sb = new StringBuilder(solutionParts[1]);
					for (int i = 2; i < solutionParts.Length; ++i)
					{
						sb.Append("__");
						sb.Append(solutionParts[i]);
					}
					TheCurrentSolutionP3.Text = sb.ToString();

					TheCurrentSolutionDone.Text = string.Empty;
				}
				else
				{
					TheCurrentSolutionP1.Text = string.Empty;
					TheCurrentSolutionP2.Text = string.Empty;
					TheCurrentSolutionP3.Text = string.Empty;
					TheCurrentSolutionDone.Text = theSolution;
				}
			}
		}

		private void Button_Cancel_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = false;
			this.Close();
		}

		private void Button_Complete_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = true;
			this.Close();
		}

		private ACtlFormula GetACtlFormula(string ctlFormulaName)
		{
			ACtlFormula aCtlFormula = null;
			if (string.IsNullOrWhiteSpace(ctlFormulaName))
			{
				return aCtlFormula;
			}

			switch (ctlFormulaName[0])
			{
				case 'A':
					switch (ctlFormulaName[1])
					{
						case 'F':
							aCtlFormula = new AF();
							break;

						case 'G':
							aCtlFormula = new AG();
							break;

						case 'U':
							aCtlFormula = new AU();
							break;

						case 'X':
							aCtlFormula = new AX();
							break;
					}
					break;

				case 'E':
					switch (ctlFormulaName[1])
					{
						case 'F':
							aCtlFormula = new EF();
							break;

						case 'G':
							aCtlFormula = new EG();
							break;

						case 'U':
							aCtlFormula = new EU();
							break;

						case 'X':
							aCtlFormula = new EX();
							break;
					}
					break;

				case '∧':
					aCtlFormula = new Conjunction();
					break;

				case '∨':
					aCtlFormula = new Disjunction();
					break;

				case '⊕':
					aCtlFormula = new Exclusive();
					break;

				case '→':
					aCtlFormula = new Implication();
					break;

				case '¬':
					aCtlFormula = new Negation();
					break;

				case '⊤':
					aCtlFormula = new Tautology();
					break;

				case '⊥':
					aCtlFormula = new Contradiction();
					break;

				case 'P':
					aCtlFormula = new Proposition(TheProposition.Text);
					break;
			}

			return aCtlFormula;
		}

		private bool RecursiveSearchForNull(ACtlFormula formula, ACtlFormula aCtlFormula)
		{
			if (formula.IsCtlFormulaLeftUsed)
			{
				if (formula.CtlFormulaLeft == null)
				{
					formula.CtlFormulaLeft = aCtlFormula;
					return true;
				}

				if (RecursiveSearchForNull(formula.CtlFormulaLeft, aCtlFormula))
				{
					return true;
				}
			}

			if (formula.IsCtlFormulaRightUsed)
			{
				if (formula.CtlFormulaRight == null)
				{
					formula.CtlFormulaRight = aCtlFormula;
					return true;
				}

				if (RecursiveSearchForNull(formula.CtlFormulaRight, aCtlFormula))
				{
					return true;
				}
			}

			return false;
		}

		private void TheCtlFormula_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (sender is ComboBox comboBox && comboBox.SelectedItem is ComboBoxItem item)
			{
				if (item.Content.Equals("Proposition"))
				{
					TheExtraText.Visibility = Visibility.Visible;
					TheProposition.Visibility = Visibility.Visible;
				}
				else
				{
					TheExtraText.Visibility = Visibility.Hidden;
					TheProposition.Visibility = Visibility.Hidden;
				}
			}
		}
	}
}