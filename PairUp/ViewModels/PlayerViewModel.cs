using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using GalaSoft.MvvmLight;
using System.ComponentModel;
using GalaSoft.MvvmLight.Messaging;
using Models.Domain;
using log4net;

namespace PairUp.ViewModels
{
    public class PlayerViewModel : ViewModelBase
    {

        #region Static Fields
        private static readonly ILog Log = LogManager.GetLogger(typeof(PlayerViewModel));
        #endregion
        #region Properties 
        private Player _player;
        public Player Player
        {
            get
            {
                return _player;
            }

            set
            {
                if (_player != value)
                {
                    _player = value;
                    RaisePropertyChanged("Player");
                }
            }
        }

        public string FirstName
        {
            get
            {
                return Player.FirstName;
            }

            set
            {
                if (Player.FirstName != value ||Player.FirstName == "")
                {
                    Player.FirstName = value;
                    RaisePropertyChanged("FirstName");
                }
            }
        }

        public string LastName
        {
            get
            {
                return Player.LastName;
            }

            set
            {
                if (Player.LastName != value)
                {
                    Player.LastName = value;
                    RaisePropertyChanged("LastName");
                }
            }
        }


        
        public double Points
        {
            get { return Player.Points; }
            set
            {
                if(Player.Points != value)
                {
                    Player.Points = value;
                    RaisePropertyChanged("Points");
                }
            }
        }

        public int GamesPlayed
        {
            get
            { 
                return Player.GamesPlayed;
            }
        }

        public bool IsReadyToPlay
        {
            get
            {
                return Player.IsReadyToPlay;
            }

            set
            {
                if (Player.IsReadyToPlay != value)
                {
                    Player.IsReadyToPlay = value;
                    RaisePropertyChanged("IsReadyToPlay");
                }
            }
        }
        
        
        #endregion


        #region Constructor
        public PlayerViewModel()
        {
            
            this.Player = new Player();
            Messenger.Default.Register<NotificationMessage>(this, NotificationMessageReceived);
        }


        public PlayerViewModel(Player player)
        {
            this.Player = player;
            Messenger.Default.Register<NotificationMessage>(this, NotificationMessageReceived);
        }

        private void NotificationMessageReceived(NotificationMessage obj)
        {
            RaisePropertyChanged("Points");
            RaisePropertyChanged("IsReadyToPlay");
        }

        #endregion

        
    }

}
