using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Models.Domain;

namespace ModelsTest
{
    public class DummyContext
    {
        public Tournament Tournament { get; set; }

        public DummyContext()
        {
            Tournament = new Tournament();
        }

        private void AddPlayers()
        {
            List<Player> players = new List<Player>();
            players.Add(new Player("Jonas","Larsen"));
            players.Add(new Player("Pol", "Jansens"));
            players.Add(new Player("Glenn", "Vanderschaeghe"));
            players.Add(new Player("Eden", "Hazard"));

            Tournament.Players = players;
        }
    }
}
