using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Models.Domain;
using log4net;

namespace Models.Repositories
{
    public class TournamentRepository
    {
        #region Static Fields
        private static readonly ILog Log = LogManager.GetLogger(typeof(TournamentRepository));
        #endregion
        private readonly XmlSerializer serializer;


        public TournamentRepository()
        {
            serializer = new XmlSerializer(typeof (Tournament));
        }

        public Tournament LoadTournamentFromXML(string path)
        {
            var reader = new StreamReader(path);
           
            var tournament = (Tournament)serializer.Deserialize(reader);
            reader.Close();
            return tournament;
           
        }

        public void SaveTournamentAsXML(Tournament tournament, string path)
        {           
            TextWriter textWriter = new StreamWriter(path);
            serializer.Serialize(textWriter, tournament);
            textWriter.Close();
        }
    }
}
