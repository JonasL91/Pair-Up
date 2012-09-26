using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using Models.Domain;
using Models.Properties;
using log4net;

namespace Models.Helpers
{

    //TODO Rewrite class insert and remove functions, to much useless code.
    public class RegistryPersister : IPersist
    {
        #region Static fields

        private static readonly ILog Log = LogManager.GetLogger(typeof(RegistryPersister));

        #endregion
        private string RegistryKey { get; set; }
        private int MaxEntries { get; set; }

        public RegistryPersister(int maxEntries)
        {
            RegistryKey =
                "Software\\" +
                Settings.Default.CompanyName + "\\" +
                Settings.Default.ProductName + "\\" +
                Settings.Default.TournamentMRUList;
            MaxEntries = maxEntries;

        }

        public RegistryPersister(string key, int maxEntries)
        {
            RegistryKey = key;
            MaxEntries = maxEntries;
        }

      

        public List<FileInfo> GetRecentFiles()
        {
            Log.Debug("--Get RecentFiles Start--");                
            RegistryKey k = Registry.CurrentUser.OpenSubKey(RegistryKey);
            if (k == null)
            {
                k = Registry.CurrentUser.CreateSubKey(RegistryKey);
            }

            var list = new List<FileInfo>(MaxEntries);

            for (int i = 0; i < k.ValueCount; i++)
            {
                string path = (string) k.GetValue((string) k.GetValueNames().GetValue(i));
                Log.Debug("Get RecentFile " + i + " Path: " + path);

                if(File.Exists(path))
                {
                    list.Add(new FileInfo(path));
                    Log.Debug("File exists");
                }
                //if the file does not exists anymore we must delete the registery key.
                else
                {
                    Log.Debug("File does NOT exists, removing registry entry...");
                    RemoveFile(path);
                }    
            }

            return list;
        }

        public void InsertFile(string path)
        {
            string name = "File1";   
            Log.Debug("Inserting file in registry...  ("+path+")");
            RegistryKey k = Registry.CurrentUser.OpenSubKey(RegistryKey);
            if (k == null)
            {
                Registry.CurrentUser.CreateSubKey(RegistryKey);
            }

            k = Registry.CurrentUser.OpenSubKey(RegistryKey, true);
            

            RemoveFile(path);
            for (int i = 1; i < MaxEntries; i++)
            {
                if(k.GetValue("File"+i) == null)
                {
                    name = "File" + i;
                    break;
                }           
            }
            k.SetValue(name, path);
        }

        public void RemoveFile(string filepath)
        {
            RegistryKey k = Registry.CurrentUser.OpenSubKey(RegistryKey, true);
            if (k != null)
            {
                for (int i = 0; i < k.ValueCount; i++)
                {
                    string s = (string) k.GetValue((string) k.GetValueNames().GetValue(i));
                   
                    if (s != null && s.Equals(filepath, StringComparison.CurrentCultureIgnoreCase))
                    {
                        Log.Debug("Deleting registry entry number: "+i+" ("+s+")");
                        k.DeleteValue((string)k.GetValueNames().GetValue(i), false);                       
                    }
                        
                }
            }       
        }
    }
}
