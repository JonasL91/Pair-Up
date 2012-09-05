using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models.Domain;
using Models.Helpers;

namespace Models.Repositories
{
    public class RecentFileRepository
    {
        private IPersist Persister { get; set; }
        private int MaxNumberOfFiles { get; set; }
        private int MaxPathLength { get; set; }
        
        public RecentFileRepository()
        {
            Persister = new RegistryPersister();

            MaxNumberOfFiles = 9;
            MaxPathLength = 50;       
        }

        public void RemoveFile(string path)
        {
            Persister.RemoveFile(path, MaxNumberOfFiles);
        }
        public void InsertFile(string path)
        {
            Persister.InsertFile(path, MaxNumberOfFiles);
        }

        public List<RecentFile> GetRecentFiles()
        {
            return Persister.GetRecentFiles(MaxNumberOfFiles);
        } 

       
    }
}
