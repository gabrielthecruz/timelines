using System.Windows;
using System.Windows.Input;
using Timelines.GUI.Shapes;

namespace Timelines.GUI.Windows
{
	public partial class ProjectWindow : Window
	{
		private Node RootNode;
		private Node SelectedNode;

		public ProjectWindow()
		{
			InitializeComponent();
			Node.DefaultCanvas = ProjectCanvas;

			RootNode = new Node { Title = "Untitled Node" };
			SelectedNode = RootNode;

			ProjectCanvas.Width = this.Width;
			ProjectCanvas.Height = this.Height;

			RootNode.Render(ProjectCanvas.Width / 2, ProjectCanvas.Height / 2);

			Title = $"{RootNode.Title} - Timelines";
		}

		private void ProjectWindow_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			ProjectCanvas.Width = e.NewSize.Width;
			ProjectCanvas.Height = e.NewSize.Height;

			RootNode.Render(ProjectCanvas.Width / 2, ProjectCanvas.Height / 2);
		}

		private void ProjectWindow_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.Key)
			{
				case Key.C: // Create node
					var node = new Node
					{
						Title = "Untitled Node",
						X = SelectedNode.X + 50,
						Y = SelectedNode.Y
					};

					SelectedNode.AddChild(node);
					SelectedNode = node;
					break;
				case Key.Q:
					Close();
					break;
			}

			e.Handled = true;
		}
	}
}
