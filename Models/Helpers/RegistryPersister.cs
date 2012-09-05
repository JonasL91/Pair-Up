using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using Models.Domain;

namespace Models.Helpers
{
    //TODO Rewrite class insert and remove functions, to much useless code.
    public class RegistryPersister : IPersist
    {
            private string RegistryKey { get; set; }

            public RegistryPersister()
            {
                RegistryKey =
                    "Software\\" +
                    "PairUp Inc."+ "\\" +
                    "PairUp" + "\\" +
                    "MRU";
            }

            public RegistryPersister(string key)
            {
                RegistryKey = key;
            }

            string Key(int i) { return i.ToString("00"); }

            public List<RecentFile> GetRecentFiles(int max)
            {
                              
                RegistryKey k = Registry.CurrentUser.OpenSubKey(RegistryKey);
                if (k == null)
                {
                    k = Registry.CurrentUser.CreateSubKey(RegistryKey);
                }

                var list = new List<RecentFile>(max);

                for (int i = 0; i < max; i++)
                {
                    string path = (string) k.GetValue(Key(i));
                    if(File.Exists(path))
                    {
                        list.Add(new RecentFile(path));
                    }
                    //if the file does not exists anymore we must delete the registery key.
                    else
                    {
                        RemoveFile(path);
                    }    
                }

                return list;
            }

            public void InsertFile(string path, int max)
            {
             
                
                RegistryKey k = Registry.CurrentUser.OpenSubKey(RegistryKey);
                if (k == null) Registry.CurrentUser.CreateSubKey(RegistryKey);
                k = Registry.CurrentUser.OpenSubKey(RegistryKey, true);
                for (int i = 0; i < k.ValueCount;i++ )
                {
                    if (Key(i).Equals(path))
                    {
                        throw new ArgumentException("File already exists in registry");
                    }
                }

                RemoveFile(path, max);

                for (int i = max - 2; i >= 0; i--)
                {
                    string sThis = Key(i);
                    string sNext = Key(i + 1);

                    object oThis = k.GetValue(sThis);
                    if (oThis != null)
                    {
                        k.SetValue(sNext, oThis);
                    }

                    
                }

                k.SetValue(Key(0), path);
            }

            public void RemoveFile(string path)
            {
                RegistryKey k = Registry.CurrentUser.OpenSubKey(RegistryKey, true);
               
                if (k != null)
                {
                    for (int i = 0; i < k.ValueCount; i++)
                    {
                        string s = (string)k.GetValue(Key(i));
                        if (s != null && s.Equals(path, StringComparison.CurrentCultureIgnoreCase))
                        {
                            
                            k.DeleteValue(Key(i));
                        }

                    }
                }
            }

            public void RemoveFile(string filepath, int max)
            {
                RegistryKey k = Registry.CurrentUser.OpenSubKey(RegistryKey);
                if (k != null)
                {
                    for (int i = 0; i < max; i++)
                    {                      
                        string s = (string)k.GetValue(Key(i));
                        if (s != null && s.Equals(filepath, StringComparison.CurrentCultureIgnoreCase))
                        {
                            RemoveFile(i, max);                             
                        }
                        
                    }
                }

               
            }

            public void RemoveFile(int index, int max)
            {
                RegistryKey k = Registry.CurrentUser.OpenSubKey(RegistryKey, true);
                if (k == null)
                {
                    k.DeleteValue(Key(index), false);

                    for (int i = index; i < max - 1; i++)
                    {
                        string sThis = Key(i);
                        string sNext = Key(i + 1);

                        object oNext = k.GetValue(sNext);
                        if (oNext == null) break;

                        k.SetValue(sThis, oNext);
                        k.DeleteValue(sNext);
                    }
                }
            }            
        }
}
