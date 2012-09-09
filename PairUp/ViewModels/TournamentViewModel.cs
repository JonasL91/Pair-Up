using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Win32;
using Models.Domain;
using Models.Repositories;
using log4net;


namespace PairUp.ViewModels
{
    public class TournamentViewModel : ViewModelBase
    {
        #region Static fields

        private static readonly ILog Log = LogManager.GetLogger(typeof (TournamentViewModel));

        #endregion

        #region Properties

        public Tournament CurrentTournament;
        private ObservableCollection<PlayerViewModel> _players;
        public ObservableCollection<PlayerViewModel> Players
        {
            get { return this._players; }
            set
            {
                if (this._players != value)
                {
                    this._players = value;
                    RaisePropertyChanged("Players");
                }
            }
        }

        public ObservableCollection<PlayerViewModel> PlayersRanked
        {
            get { return new ObservableCollection<PlayerViewModel>(this._players.OrderByDescending(playerViewModel => playerViewModel.Points)); }
        }


        private ObservableCollection<GameViewModel> _games;
        public ObservableCollection<GameViewModel> Games
        {
            get { return this._games; }
            set
            {
                if (this._games != value)
                {
                    this._games = value;
                    RaisePropertyChanged("Games");
                }
            }
        }
        public RecentFileRepository RecentFileRepository { get; set; }

        private ObservableCollection<RecentFileViewModel> _recentFiles;
        public ObservableCollection<RecentFileViewModel> RecentFiles
        {
            get { return _recentFiles; }
            set
            {
                if (_recentFiles != value)
                {
                    _recentFiles = value;
                    RaisePropertyChanged("RecentFiles");
                }
            }
        }

        public RelayCommand PairUpCommand { get; set; }
        public RelayCommand SaveAsCommand { get; set; }
        public RelayCommand SaveCommand { get; set; }
        //public bool IsSaved { get; set; }

        #endregion




        #region Constructors

        public TournamentViewModel()
        {
            CurrentTournament = new Tournament();

            Players = new ObservableCollection<PlayerViewModel>();
            Games = new ObservableCollection<GameViewModel>();
            Messenger.Default.Register<NotificationMessage>(this, NotificationMessageReceived);
            InitCommands();

        }
        /// <summary>
        /// Used when the user chooses to open an existing tournament.
        /// </summary>
        /// <param name="tournament"></param>
        public TournamentViewModel(Tournament tournament)
        {
            CurrentTournament = tournament;
            Players = new ObservableCollection<PlayerViewModel>();
            Games = new ObservableCollection<GameViewModel>();
            InitTournament();
            InitCommands();
        }

        #endregion

        private void InitTournament()
        {
            foreach (Game game in CurrentTournament.Games)
            {
                GameViewModel gameViewModel = new GameViewModel(game);
                Games.Add(gameViewModel);
            }
            foreach (Player player in CurrentTournament.Players)
            {
                PlayerViewModel playerViewModel = new PlayerViewModel(player);
                Players.Add(playerViewModel);
            }
        }

        #region Commands

        private void InitCommands()
        {
            PairUpCommand = new RelayCommand(PairUp, ()=>CanPairUp());
            SaveAsCommand = new RelayCommand(() => SaveAs());
        }

        private bool CanSaveAs()
        {
            throw new NotImplementedException();
        }

        private void SaveAs()
        {
            var saveFileDialog = new SaveFileDialog
                                     {
                                         FileName = "New Tournament",  // Default file name
                                         DefaultExt = ".xml", // Default file extension
                                         Filter = "XML documents (.xml)|*.xml" // Filter files by extension
                                     };
           
            
            

            // Show save file dialog box
            Nullable<bool> result = saveFileDialog.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string filename = saveFileDialog.FileName;
                Save(filename);
            }
        }

        #region Methods
        /// <summary>
        /// First thing we do is add the new players to the tournament. 
        /// TODO Now we loop over all the games each time the button is pressed and clear the list each time, finally we add for each game a new viewmodel.
        /// TODO Would there be a way or is it necessary to add only the new games?
        /// We have to clear the Observable Games list each time or duplicate viewmodels get added.
        /// </summary>
        private void PairUp()
        {
            AddNewPlayers();
            CurrentTournament.PairUp();
            Games.Clear();
            foreach (Game game in CurrentTournament.Games)
            {
                GameViewModel gameViewModel = new GameViewModel(game);
                Games.Add(gameViewModel);             
            }
            RaisePropertyChanged("PlayersRanked");
        }

        private void AddNewPlayers()
        {
            foreach (PlayerViewModel p in Players)
            {
                CurrentTournament.AddPlayer(p.Player);
            }
        }

        //TODO Repositories solution: We shouldn't create them each time we need them. Can we make them accesible everywhere somehow? i.e static class controller?
        /// <summary>
        /// This method is responsible for saving a tournament to a certain path.
        /// TODO Should we insert a recent file here or automaticly when we save a tournament, so in the tournament repository.
        /// </summary>
        /// <param name="uri"></param>
        private void Save(string uri)
        {
            Log.Debug(uri);
            
            RecentFileRepository recentFileRepository = new RecentFileRepository();
            TournamentRepository tournamentRepository = new TournamentRepository();
            tournamentRepository.SaveTournamentAsXML(CurrentTournament, uri);
            recentFileRepository.InsertFile(uri);
            
        }

#endregion

        /// <summary>
        /// Check if we can PairUp.
        /// - If there are no players we can't pairup.
        /// - If there are players, but none is ready to play, we can't pairup. We need at least 2 players, ready to play to pairup.
        /// </summary>
        /// <returns>True if can PairUp. False if cannot PairUp</returns>
        private bool CanPairUp()
        {
            if ((from p in Players where p.IsReadyToPlay select p).Count() > 1)
            {
                return true;
            }
            else return false;
        }

        #endregion
        /// <summary>
        /// Currently only used when a result gets changed, the rank table needs to get updated.
        /// </summary>
        /// <param name="obj"></param>
        private void NotificationMessageReceived(NotificationMessage obj)
        {
            RaisePropertyChanged("PlayersRanked");
        }


    }



}
