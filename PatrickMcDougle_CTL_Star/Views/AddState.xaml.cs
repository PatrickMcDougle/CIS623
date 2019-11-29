using System.Windows;

namespace PatrickMcDougle_CTL_Star
{
	/// <summary>
	///     Interaction logic for AddState.xaml
	/// </summary>
	public partial class AddState : Window
	{
		public AddState()
		{
			InitializeComponent();
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