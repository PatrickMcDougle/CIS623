using System.Windows;

namespace PatrickMcDougle_CTL_Star
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{

			var viewModel = new ModelViewModel
			{
				NumberOfStates = 4
			};

			DataContext = viewModel;

			InitializeComponent();
		}
	}
}
