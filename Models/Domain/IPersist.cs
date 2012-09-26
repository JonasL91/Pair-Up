using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Models.Domain
{
    public interface IPersist
    {
        List<FileInfo> GetRecentFiles();
        void InsertFile(string path);
        void RemoveFile(string path);
    }
}
