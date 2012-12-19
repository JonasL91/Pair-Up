using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Win32;
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
            foreach (FileInfo f in temp)
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
            if(SelectedFile != null)
            {
                Tournament tournament = tournamentRepository.LoadTournamentFromXML(SelectedFile.RecentFile.FullName);
                var tournamentViewModel = new TournamentViewModel(tournament, SelectedFile.RecentFile.FullName);
                Messenger.Default.Send(new NotificationMessage<ViewModelBase>(this, tournamentViewModel, "TournamentViewModel"));
            }
            else
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Title = "Open XML bestand";
                openFileDialog.DefaultExt = ".xml";
                openFileDialog.Filter = "XML documents (.xml)|*.xml";
                openFileDialog.CheckFileExists = true;

                openFileDialog.CheckPathExists = true;
                if (openFileDialog.ShowDialog() == true)
                {
                    Tournament tournament = tournamentRepository.LoadTournamentFromXML(openFileDialog.FileName);
                    var tournamentViewModel = new TournamentViewModel(tournament);
                    Messenger.Default.Send(new NotificationMessage<ViewModelBase>(this, tournamentViewModel, "TournamentViewModel"));
                }             
            }
            //Making it null again in case we want to access the method out of the first window (for example starting a new tournament while in the tournamentViewModel)
            //If we don't do this, we always get in the first if and it will always open the tournament that was originally selected.
            SelectedFile = null;
        }

        private void StartNewTournament()
        {
            Log.Debug("Starting new tournament...");
            var tournamentViewModel = new TournamentViewModel();
            Messenger.Default.Send(new NotificationMessage<ViewModelBase>(this,tournamentViewModel,"TournamentViewModel"));
        }


    }
}
