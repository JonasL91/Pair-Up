using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using Models.Domain;
using PairUp.ViewModels;
using log4net;

namespace PairUp
{
	/// <summary>
	/// Interaction logic for StartUp.xaml
	/// </summary>
	public partial class MainScreen : Window
	{
        #region Static fields
        private static readonly ILog Log = LogManager.GetLogger(typeof(MainScreen));
        #endregion
	    public TournamentViewModel TournamentViewModel { get; set; }
        public MainScreen()
		{
			this.InitializeComponent();
            this.DataContext = new AppViewModel();


            // Insert code required on object creation below this point.
		}   
	}
}