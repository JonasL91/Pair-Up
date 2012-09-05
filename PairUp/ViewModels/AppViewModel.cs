using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace PairUp.ViewModels
{
    public class AppViewModel : ViewModelBase
    {
        private TournamentViewModel tournamentViewModel;
        private ViewModelBase _currentView;
        public ViewModelBase CurrentView
        {
            get { return _currentView; }
            set
            {
                if(_currentView != value)
                {
                    _currentView = value;
                    RaisePropertyChanged("CurrentView");
                }
            }
        }

       

        //We need to see the start page only at start currently, so no need to make a command switching to that.
        //public RelayCommand ViewStartPageCommand { get; set; }
        public RelayCommand ViewTournamentCommand { get; set; }

        public AppViewModel()
        {
            StartUpViewModel startUpViewModel = new StartUpViewModel();
            CurrentView = startUpViewModel;
            ViewTournamentCommand = new RelayCommand(ShowTournamentView);
            Messenger.Default.Register<NotificationMessage<ViewModelBase>>(this, NotificationMessageReceived);
        }

        public void ShowTournamentView()
        {
            CurrentView = tournamentViewModel;
        }

        private void NotificationMessageReceived(NotificationMessage<ViewModelBase> currentView)
        {
            CurrentView = currentView.Content;
        }
    }
}
