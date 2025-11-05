using System.Diagnostics.Metrics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Timelines.Core.Exceptions;

namespace Timelines.GUI.Shapes
{
	public class Node
	{
		internal static Canvas? DefaultCanvas;

		private Node? Parent;
		private string _title = string.Empty;
		private double _x = 0;
		private double _y = 0;

		public Ellipse Shape;
		public Label Label;
		public Guid Id { get; }
		public string Title 
		{ 
			get { return _title; }
			set
			{
				_title = value;
				Label.Content = value;
				Label.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
				Canvas.SetLeft(Label, X - (Label.ActualWidth / 2) + (Shape.Width / 2));
			}
		}
		public double X
		{
			get { return _x; }
			set
			{
				_x = value;
				Canvas.SetLeft(Shape, value);
				Canvas.SetLeft(Label, value - (Label.ActualWidth / 2) + (Shape.Width / 2));
				if (Parent is not null)
					Parent.Lines[Id].X2 = value + Shape.Width / 2;

				foreach (var (_, line) in Lines)
				{
					line.X1 = value + Shape.Width / 2;
				}
			}
		}
		public double Y
		{
			get { return _y; }
			set
			{
				_y = value;
				Canvas.SetTop(Shape, value);
				Canvas.SetTop(Label, value + 10);
				if (Parent is not null)
					Parent.Lines[Id].Y2 = value + Shape.Height / 2;

				foreach (var (_, line) in Lines)
				{
					line.Y1 = value + Shape.Height / 2;
				}
			}
		}

		public Dictionary<Guid, Node> Children { get; set; }
		public Dictionary<Guid, Line> Lines { get; set; }

		public Node()
		{
			Parent = null;
			Id = Guid.NewGuid();
			Lines = [];
			Children = [];

			Shape = new Ellipse
			{
				Opacity = 100,
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Center,
				Width = 10,
				Height = 10,
				Stroke = Brushes.Black,
				Fill = Brushes.Black
			};
			
			DefaultCanvas?.Children.Add(Shape);

			Label = new Label
			{
				Opacity = 100,
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Center,
				FontSize = 10,
				Content = _title,
				Target = Shape
			};

			DefaultCanvas?.Children.Add(Label);
			Title = string.Empty;
		}

		public void AddChild(Node child)
		{
			if (Children.ContainsKey(child.Id))
			{
				throw new ChildAlreadyExistsException("Cannot add existent child to node.");
			}

			Children.Add(child.Id, child);
			child.Parent = this;
			
			if (!Lines.ContainsKey(child.Id))
			{
				Lines.Add(child.Id, new Line
				{
					Opacity = 100,
					Stroke = Brushes.Black,
					X1 = X + Shape.Width / 2,
					Y1 = Y + Shape.Height / 2,
					X2 = child.X + child.Shape.Width / 2,
					Y2 = child.Y + child.Shape.Height / 2
				});
				
				DefaultCanvas?.Children.Add(Lines[child.Id]);
			}
		}

		public void RemoveChild(Guid nodeId)
		{
			if (!Children.ContainsKey(nodeId))
			{
				throw new MissingChildException($"Node '{nodeId}' is not a child of '{Title}'.");
			}

			Children.Remove(nodeId);
		}

		public void Render(double x, double y, double dist = 50)
		{
			X = x; Y = y;
			foreach (var (_, child) in Children)
			{
				child.Render(x + dist, y);
			}
		}

	}
}
