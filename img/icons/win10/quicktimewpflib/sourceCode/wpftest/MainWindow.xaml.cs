using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using QuickTimeWpfLib;

namespace wpftest
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

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			//Reference you video
		//	qt.Url = 
		}

		private void qt_StatusUpdate(object sender, StatusUpdateEventArgs e)
		{
			if (e.StatusCode == StatusCode.MovieLoadStateComplete)
			{
				qt.Movie.Play();
			}
		}

		private void qt_Error(object sender, ErrorEventArgs e)
		{

		}

	}
}
