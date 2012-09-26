using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Models.Domain;
using Models.Helpers;
using Models.Properties;

namespace Models.Repositories
{
    public class RecentFileRepository
    {
        private IPersist Persister { get; set; }
       
        
        public RecentFileRepository()
        {
            Persister = new RegistryPersister(Settings.Default.MaxEntriesRencentTournaments);   
        }

        public void RemoveFile(string path)
        {
            Persister.RemoveFile(path);
        }
        public void InsertFile(string path)
        {
            Persister.InsertFile(path);
        }

        public List<FileInfo> GetRecentFiles()
        {
            return Persister.GetRecentFiles();
        } 

       
    }
}
