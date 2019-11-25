using Microsoft.Win32;
using PatrickMcDougle_CTL_Star.Json;
using PatrickMcDougle_CTL_Star.Models;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PatrickMcDougle_CTL_Star
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private const string _INIT_DIR_ = @"D:\GitHub\CIS623\PatrickMcDougle_CTL_Star\Examples";
		private readonly JsonFile _jsonFile = new JsonFile();
		private readonly CtlpViewModel _viewModel = new CtlpViewModel();

		public MainWindow()
		{
			_viewModel.LoadModel(new CtlpModel());
			DataContext = _viewModel;

			InitializeComponent();

		}

		private void MenuItem_Open_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog
			{
				InitialDirectory = _INIT_DIR_
			};

			if (openFileDialog.ShowDialog() == true)
			{
				string path = openFileDialog.FileName;

				var json = _jsonFile.DeserializeFromFile<CtlpModel>(path);

				_viewModel.LoadModel(json);
			}
			PointCollection myPointCollection = new PointCollection
			{
				new Point(0, 0),
				new Point(0, 1),
				new Point(1, 1)
			};

			Polygon myPolygon = new Polygon
			{
				Points = myPointCollection,
				Fill = Brushes.Blue,
				Width = 100,
				Height = 100,
				Stretch = Stretch.Fill,
				Stroke = Brushes.Black,
				StrokeThickness = 2
			};

			TheCanvas.Children.Add(myPolygon);

		}

		private void MenuItem_Save_Click(object sender, RoutedEventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog
			{
				InitialDirectory = _INIT_DIR_
			};

			if (saveFileDialog.ShowDialog() == true)
			{
				string path = saveFileDialog.FileName;

				_jsonFile.SerializeToFile(_viewModel.GetModel(), path);
			}
		}

		private void Button_Add_State_Click(object sender, RoutedEventArgs e)
		{
			AddState addState = new AddState();
			if (addState.ShowDialog() == true)
			{
				_viewModel.AddState(addState.StateName.Text);
			}
		}

		private void Button_Del_State_Click(object sender, RoutedEventArgs e)
		{
			DelState delState = new DelState
			{
				DataContext = this.DataContext
			};

			if (delState.ShowDialog() == true)
			{
				_viewModel.DeleteState(delState.StateName.SelectedItem as string);
			}

		}

		private void Button_Add_Edge_Click(object sender, RoutedEventArgs e)
		{
			AddEdge addEdge = new AddEdge()
			{
				DataContext = this.DataContext
			};

			if (addEdge.ShowDialog() == true)
			{
				_viewModel.AddEdge(addEdge.EdgeStart.Text, addEdge.EdgeFinish.Text);
			}
		}

		private void Button_Del_Edge_Click(object sender, RoutedEventArgs e)
		{
			DelState delState = new DelState
			{
				DataContext = this.DataContext
			};

			if (delState.ShowDialog() == true)
			{
				_viewModel.DeleteState(delState.StateName.SelectedItem as string);
			}

		}
	}
}