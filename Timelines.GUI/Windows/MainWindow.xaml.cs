using System.Windows;
using System.Windows.Input;

namespace Timelines.GUI.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.N:
					var newProjectWindow = new ProjectWindow();
					newProjectWindow.Show();
                    this.Close();
                    break;
                case Key.O:
                    MessageBox.Show("Open Project!\nNot implemented yet!");
                    break;
                case Key.Q:
                    this.Close();
                    break;
            }

            e.Handled = true;
        }
	}
}