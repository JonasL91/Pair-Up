using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
       
        private FileInfo _recentFile;

        public FileInfo RecentFile
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
            get { return RecentFile.Name; }
        }

        public string LastTimeEdited
        {
            get { return RecentFile.LastWriteTime.ToShortDateString(); }
        }

        public string Folder
        {
            get { return RecentFile.DirectoryName; }
        }


        #endregion

        #region Constructors
        public RecentFileViewModel(FileInfo recentFile)
        {
            this.RecentFile = recentFile;
        }

       
        #endregion


    }
}
