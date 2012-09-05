using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models.Domain
{
    public interface IPersist
    {
        List<RecentFile> GetRecentFiles(int max);
        void InsertFile(string filepath, int max);
        void RemoveFile(string filepath, int max);
    }
}
