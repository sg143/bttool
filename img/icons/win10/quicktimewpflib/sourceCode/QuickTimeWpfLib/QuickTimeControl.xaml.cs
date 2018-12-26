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

namespace QuickTimeWpfLib
{
	/// <summary>
	/// Interaction logic for QuickTimeControl.xaml
	/// </summary>
	public partial class QuickTimeControl : UserControl
	{
		public QuickTimeControl()
		{
			InitializeComponent();
			qtControl.Error += QtControlError;
			qtControl.StatusUpdate += QtControlStatusUpdate;
		}



		// TODO Optimize Events, Reduce unused COM-.NET translations
		#region Event Handlers
		void QtControlError(object sender, AxQTOControlLib._IQTControlEvents_ErrorEvent e)
		{
			if (Error!=null)
			{
				Error(this,new ErrorEventArgs(e));
			}
		}

		void QtControlStatusUpdate(object sender, AxQTOControlLib._IQTControlEvents_StatusUpdateEvent e)
		{
			if (StatusUpdate!=null)
			{
				StatusUpdate(this, new StatusUpdateEventArgs(e));
			}
		}
		#endregion

		public QuickTimeMovie Movie
		{
			get { return new QuickTimeMovie(qtControl.Movie); }
		}

		public OpenMovieFlags NewMovieFlags
		{
			get { return (OpenMovieFlags)qtControl.NewMovieFlags; }
			set { qtControl.NewMovieFlags = (int)value; }
		}

		public string AutoPlay
		{
			get { return qtControl.AutoPlay; }
			set { qtControl.AutoPlay = value; }
		}

		public string Url
		{
			get { return qtControl.URL; }
			set { qtControl.URL = value; }
		}

		public event EventHandler<ErrorEventArgs> Error;
		public event EventHandler<StatusUpdateEventArgs> StatusUpdate;
	}

	[Flags]
	public enum OpenMovieFlags
	{
		Active=1,
		DontResolveDataRefs = 2,
		DontAskUnresolvedDataRefs = 4,
		DontAutoAlternates = 8,
		AsyncOK = 256,
		IdleImportOK = 1024,
		DontInteractWithUser = 2048,
		DefaultFlags=Active|IdleImportOK
	}
}
