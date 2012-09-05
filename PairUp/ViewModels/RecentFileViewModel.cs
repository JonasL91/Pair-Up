using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Models.Domain;
using Models.Repositories;

namespace PairUp.ViewModels
{
    public class RecentFileViewModel : ViewModelBase
    {
        #region Properties
       
        private RecentFile _recentFile;

        public RecentFile RecentFile
        {
            get { return _recentFile; }
            set
            {
                if (_recentFile != value)
                {
                    _recentFile = value;
                    RaisePropertyChanged("RecentFile");
                }
            }
        }

        public string Name
        {
            get { return RecentFile.Filepath; }
        }


        #endregion

        #region Constructors
        public RecentFileViewModel(RecentFile recentFile)
        {
            this.RecentFile = recentFile;
        }

       
        #endregion


    }
}
