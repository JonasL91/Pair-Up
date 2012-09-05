using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Models.Domain;

namespace Models.Repositories
{
    public class TournamentRepository
    {
        private readonly XmlSerializer serializer;


        public TournamentRepository()
        {
            serializer = new XmlSerializer(typeof (Tournament));
        }

        public Tournament LoadTournamentFromXML(string path)
        {
            var reader = new StreamReader(path);
            Tournament tournament = (Tournament) serializer.Deserialize(reader);
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
