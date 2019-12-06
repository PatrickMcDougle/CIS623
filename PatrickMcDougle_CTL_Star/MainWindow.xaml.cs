using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.Win32;
using PatrickMcDougle_CTL_Star.Data;
using PatrickMcDougle_CTL_Star.Factories;
using PatrickMcDougle_CTL_Star.Json;

namespace PatrickMcDougle_CTL_Star
{
	/// <summary>
	///     Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			_viewModel.LoadModel(new CtlpData());
			DataContext = _viewModel;

			InitializeComponent();
		}

		private const string _INIT_DIR_ = @"D:\GitHub\CIS623\PatrickMcDougle_CTL_Star\Examples";
		private const int _stateCircleDiamater = 80;
		private readonly JsonFile _jsonFile = new JsonFile();
		private readonly CtlpViewModel _viewModel = new CtlpViewModel();

		private void Button_Add_Binary_Relation_Click(object sender, RoutedEventArgs e)
		{
			AddBinaryRelation addBinaryRelation = new AddBinaryRelation()
			{
				DataContext = this.DataContext
			};

			if (addBinaryRelation.ShowDialog() == true)
			{
				_viewModel.AddBinaryRelation(addBinaryRelation.BinaryRelationStart.Text, addBinaryRelation.BinaryRelationFinish.Text);
			}
		}

		private void Button_Add_Labeling_Function_Click(object sender, RoutedEventArgs e)
		{
			// nothing at this time.
		}

		private void Button_Add_State_Click(object sender, RoutedEventArgs e)
		{
			AddState addState = new AddState();
			if (addState.ShowDialog() == true)
			{
				_viewModel.AddState(addState.StateName.Text);
			}
		}

		private void Button_Del_Binary_Relation_Click(object sender, RoutedEventArgs e)
		{
			DelBinaryRelation delBinaryRelation = new DelBinaryRelation
			{
				DataContext = this.DataContext
			};

			if (delBinaryRelation.ShowDialog() == true)
			{
				_viewModel.DeleteBinaryRelation(
					delBinaryRelation.BinaryRelationStart.SelectedItem as string,
					delBinaryRelation.BinaryRelationFinish.SelectedItem as string
					);
			}
		}

		private void Button_Del_Labeling_Function_Click(object sender, RoutedEventArgs e)
		{
			//nothing at this time
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

		private void DrawCircleOnCanvas(double x, double y, double r, Brush fill, Brush stroke, double strokeThickness)
		{
			// Background Circle
			TransformGroup myTransformGroup = new TransformGroup();
			myTransformGroup.Children.Add(new TranslateTransform()
			{
				X = x,
				Y = y
			});

			Ellipse myEllipse = new Ellipse
			{
				Width = r,
				Height = r,

				Fill = fill,
				Stroke = stroke,
				StrokeThickness = strokeThickness,
				RenderTransform = myTransformGroup
			};

			TheCanvas.Children.Add(myEllipse);
		}

		private void DrawStatesOnCanvas(CtlpData ctlpData)
		{
			double smallestSide = (TheCanvas.Height > TheCanvas.Width) ? TheCanvas.Width : TheCanvas.Height;
			double circleDiamater = smallestSide - _stateCircleDiamater;
			double circleRadius = circleDiamater * 0.5;

			double canvasVerticleCenter = TheCanvas.Height * 0.5;
			double canvasHorizontalCenter = TheCanvas.Width * 0.5;

			TransformGroup transformGroup = new TransformGroup();
			transformGroup.Children.Add(new TranslateTransform()
			{
				X = canvasHorizontalCenter - smallestSide * 0.5,
				Y = canvasVerticleCenter - smallestSide * 0.5
			});

			Rectangle myBackground = new Rectangle
			{
				Width = smallestSide,
				Height = smallestSide,
				Fill = Brushes.DarkSlateGray,
				RenderTransform = transformGroup
			};

			TheCanvas.Children.Add(myBackground);

			// Background Circle
			DrawCircleOnCanvas(canvasHorizontalCenter - circleRadius,
				canvasVerticleCenter - circleRadius,
				circleDiamater,
				Brushes.White, Brushes.Blue, 5);

			var angleDelta = (Math.PI + Math.PI) / ctlpData.States.Count;
			var angle = 0.0;

			// draw a circle for each state.
			foreach (var item in ctlpData.States)
			{
				var x = canvasHorizontalCenter + circleRadius * Math.Cos(angle);
				var y = canvasVerticleCenter + circleRadius * Math.Sin(angle);

				DrawCircleOnCanvas(x - _stateCircleDiamater * 0.5,
					y - _stateCircleDiamater * 0.5,
					_stateCircleDiamater,
					Brushes.Yellow, Brushes.Green, 5);

				// state name
				TextBlock textBlock = new TextBlock()
				{
					Text = item,
					Foreground = Brushes.Black,
					Background = Brushes.Wheat,
					FontSize = 16.0
				};

				Canvas.SetLeft(textBlock, canvasHorizontalCenter + circleDiamater * 0.61 * Math.Cos(angle + 0.15) - 8);
				Canvas.SetTop(textBlock, canvasVerticleCenter + circleDiamater * 0.61 * Math.Sin(angle + 0.15) - 8);

				TheCanvas.Children.Add(textBlock);

				// state propositions
				if (ctlpData.LabelingFunctions.Any(l => l.State.Equals(item)))
				{
					textBlock = new TextBlock()
					{
						Text = string.Join(",", ctlpData.LabelingFunctions.First(f => f.State.Equals(item)).Propositions.ToArray()),
						Foreground = Brushes.Black,
						Background = Brushes.Wheat,
						FontSize = 16.0,
						FontFamily = new FontFamily("Anonymous Pro")
					};

					Canvas.SetLeft(textBlock, x - textBlock.Text.Count() * 4);
					Canvas.SetTop(textBlock, y - 8);

					TheCanvas.Children.Add(textBlock);
				}

				angle += angleDelta;
			}

			foreach (var item in ctlpData.BinaryRelations)
			{
				var startIndex = ctlpData.States.IndexOf(item.Start);
				var finishIndex = ctlpData.States.IndexOf(item.Finish);

				if (startIndex != finishIndex)
				{
					// if the two indexes are different, then draw a line
					// between them.

					// first calculate the line from center to center.
					Line line = new Line
					{
						X1 = canvasHorizontalCenter + circleRadius * Math.Cos(angleDelta * startIndex),
						Y1 = canvasVerticleCenter + circleRadius * Math.Sin(angleDelta * startIndex),
						X2 = canvasHorizontalCenter + circleRadius * Math.Cos(angleDelta * finishIndex),
						Y2 = canvasVerticleCenter + circleRadius * Math.Sin(angleDelta * finishIndex),
						Stroke = Brushes.Black,
						StrokeThickness = 3
					};

					// Remove part of the line so that the line is coming from
					// the edge of the circle.
					if (FindLineCircleIntersections(line.X1, line.Y1, _stateCircleDiamater * 0.5, line.X1, line.Y1, line.X2, line.Y2, out double x1, out double y1, out double x2, out double y2))
					{
						double d1 = FindDistanceBetweenPoints(x1, y1, line.X2, line.Y2);
						double d2 = FindDistanceBetweenPoints(x2, y2, line.X2, line.Y2);
						if (d1 < d2)
						{
							line.X1 = x1;
							line.Y1 = y1;
						}
						else
						{
							line.X1 = x2;
							line.Y1 = y2;
						}
					}

					// remove the other end of the line so that the line is at
					// the edge of the circle.
					if (FindLineCircleIntersections(line.X2, line.Y2, _stateCircleDiamater * 0.5, line.X1, line.Y1, line.X2, line.Y2, out x1, out y1, out x2, out y2))
					{
						double d1 = FindDistanceBetweenPoints(x1, y1, line.X1, line.Y1);
						double d2 = FindDistanceBetweenPoints(x2, y2, line.X1, line.Y1);
						if (d1 < d2)
						{
							line.X2 = x1;
							line.Y2 = y1;
						}
						else
						{
							line.X2 = x2;
							line.Y2 = y2;
						}
					}

					TheCanvas.Children.Add(line);

					// draw a circle at the end of the finish to denote that it
					// is traveling in that direction.
					DrawCircleOnCanvas(line.X2 - 5,
						line.Y2 - 5,
						10,
						Brushes.Black, Brushes.Black, 1);
				}
			}
		}

		/// <summary>
		///     Finding the distance between two points. This method uses the
		///     following formula: ((x2-x1)^2 + (y2-y1)^2)^(1/2)
		/// </summary>
		/// <param name="x1">point one's X location</param>
		/// <param name="y1">point one's Y location</param>
		/// <param name="x2">point two's X location</param>
		/// <param name="y2">point two's Y location</param>
		/// <returns></returns>
		private double FindDistanceBetweenPoints(double x1, double y1, double x2, double y2)
		{
			double dx = x2 - x1;
			double dy = y2 - y1;
			return Math.Sqrt(dx * dx + dy * dy);
		}

		// Find the points of intersection.
		private bool FindLineCircleIntersections(double cx, double cy, double radius,
												double x1, double y1, double x2, double y2,
												out double xi1, out double yi1,
												out double xi2, out double yi2)
		{
			double dx = x2 - x1;
			double dy = y2 - y1;

			double A = dx * dx + dy * dy;
			double B = 2 * (dx * (x1 - cx) + dy * (y1 - cy));
			double C = (x1 - cx) * (x1 - cx) + (y1 - cy) * (y1 - cy) - radius * radius;

			double det = B * B - 4 * A * C;
			if ((A <= 0.0000001) || (det < 0))
			{
				xi1 = 0.0;
				yi1 = 0.0;
				xi2 = 0.0;
				yi2 = 0.0;
				return false;
			}
			else if (det == 0)
			{
				xi1 = 0.0;
				xi2 = 0.0;
				yi1 = 0.0;
				yi2 = 0.0;
				return false;
			}
			else
			{
				// Two solutions.
				double t = ((-B + Math.Sqrt(det)) / (2 * A));
				xi1 = x1 + t * dx;
				yi1 = y1 + t * dy;

				t = (float)((-B - Math.Sqrt(det)) / (2 * A));
				xi2 = x1 + t * dx;
				yi2 = y1 + t * dy;
				return true;
			}
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

				var ctlpData = _jsonFile.DeserializeFromFile<CtlpData>(path);

				_viewModel.LoadModel(ctlpData);

				ModelFactory modelFactory = new ModelFactory();

				var item = modelFactory.CreateModel(ctlpData);
				Console.WriteLine(item.Name);

				DrawStatesOnCanvas(ctlpData);
			}
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
	}
}