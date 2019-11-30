using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace PatrickMcDougle_CTL_Star
{
	/// <summary>
	///     Interaction logic for AddState.xaml
	/// </summary>
	public partial class DelBinaryRelation : Window
	{
		public DelBinaryRelation()
		{
			InitializeComponent();
		}

		private void BinaryRelationFinish_Selected(object sender, RoutedEventArgs e)
		{
			if (sender is ComboBox comboBox && DataContext is CtlpViewModel viewModel)
			{
				var value = comboBox.SelectedValue as string;

				viewModel.StatesBinaryRelationFinish = new List<string>() { value };
			}
		}

		private void BinaryRelationStart_Selected(object sender, RoutedEventArgs e)
		{
			if (sender is ComboBox comboBox && DataContext is CtlpViewModel viewModel)
			{
				var value = comboBox.SelectedValue as string;

				viewModel.StatesBinaryRelationStart = new List<string>() { value };
			}
		}

		private void Button_Add_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = true;
			this.Close();
		}

		private void Button_Cancel_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = false;
			this.Close();
		}
	}
}