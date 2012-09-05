using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Models.Domain;
using Models.Repositories;
using log4net;

namespace PairUp.ViewModels
{
    public class StartUpViewModel : ViewModelBase
    {
        #region Static fields

        private static readonly ILog Log = LogManager.GetLogger(typeof(StartUpViewModel));

        #endregion
        #region Properties

        public RelayCommand StartNewTournamentCommand { get; set; }
        public RelayCommand LoadTournamentCommand { get; set; }

        private RecentFileRepository _recentFileRepository;
        private RecentFileViewModel _selectedFile;
        public RecentFileViewModel SelectedFile
        {
            get { return _selectedFile; }
            set
            {
                if (value != _selectedFile)
                {
                    _selectedFile = value;
                    RaisePropertyChanged("SelectedFile");
                }
            }
        }
        private ObservableCollection<RecentFileViewModel> _recentFiles;
        public ObservableCollection<RecentFileViewModel> RecentFiles
        {
            get { return _recentFiles; }
            set
            {
                if (value != _recentFiles)
                {
                    _recentFiles = value;
                    RaisePropertyChanged("RecentFiles");
                }
            }
        } 
        #endregion
        public StartUpViewModel()
        {
            
            InitCommands();
            _recentFileRepository = new RecentFileRepository();
            var temp = _recentFileRepository.GetRecentFiles();
            RecentFiles = new ObservableCollection<RecentFileViewModel>();
            foreach (RecentFile f in temp)
            {
                var v = new RecentFileViewModel(f);
                RecentFiles.Add(v);
            }
        }

        private void InitCommands()
        {
            StartNewTournamentCommand = new RelayCommand(StartNewTournament);
            LoadTournamentCommand = new RelayCommand(LoadTournament);
        }

        private void LoadTournament()
        {
            Log.Debug("Loading tournament...");
            TournamentRepository tournamentRepository = new TournamentRepository();
            //TODO better approach possible? Binding directly to the filepath. Or does the selected item needs to be a whole "RecentFileViewModel".
            //For example if we want to show information or w/e when selected... (just an example)
            Tournament tournament = tournamentRepository.LoadTournamentFromXML(SelectedFile.RecentFile.Filepath);
            var tournamentViewModel = new TournamentViewModel(tournament);
            Messenger.Default.Send(new NotificationMessage<ViewModelBase>(this, tournamentViewModel, "CurrentContent"));
        }

        private void StartNewTournament()
        {
            Log.Debug("Starting new tournament...");
            var tournamentViewModel = new TournamentViewModel();
            Messenger.Default.Send(new NotificationMessage<ViewModelBase>(this,tournamentViewModel,"CurrentContent"));
        }


    }
}
