using System.Windows;

namespace PatrickMcDougle_CTL_Star
{
	/// <summary>
	///     Interaction logic for AddState.xaml
	/// </summary>
	public partial class InitialState : Window
	{
		public InitialState()
		{
			InitializeComponent();
		}

		private void Button_Cancel_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = false;
			this.Close();
		}

		private void Button_Initial_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = true;
			this.Close();
		}
	}
}