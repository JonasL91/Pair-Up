using System.Windows;
using log4net;
using log4net.Config;

namespace PairUp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static ILog log;

        static App() 
        {
            XmlConfigurator.Configure();
            log = LogManager.GetLogger(typeof(App));
            log.Info("PairUp started");

        }


    }
}
