using System.Windows;

namespace PatrickMcDougle_CTL_Star
{
	/// <summary>
	///     Interaction logic for AddState.xaml
	/// </summary>
	public partial class DelPropositions : Window
	{
		public DelPropositions()
		{
			InitializeComponent();
		}

		private void Button_Cancel_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = false;
			this.Close();
		}

		private void Button_Delete_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = true;
			this.Close();
		}
	}
}