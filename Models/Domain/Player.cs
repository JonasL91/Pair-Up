using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using log4net;

namespace Models.Domain
{
    public class Player
    {
        #region Static fields
        private static readonly ILog Log = LogManager.GetLogger(typeof(Player));
        #endregion
        #region Properties
        public bool IsReadyToPlay { get; set; }
        private double _points;
        public double Points
        {
            get { return _points; }
            set
            {
                //A player can't have less then 0 points.
                if (_points + value >= 0)
                {
                    _points += value;
                }
            }
        }

        public int WhiteBlackBalance { get; set; }
        //Currently the games are sorted always by date of result. This could be changed later on.
        [NonSerialized]
        private List<Game> _games;
        [XmlIgnore]
        public List<Game> Games 
        { 
            get 
            {
                _games = _games.OrderBy(g => g.DateOfResult).ToList();
                return _games;
            }
            set { _games = value; }
        }  

        //Trim the first name, we don't want spaces.
        private string _firstName;
        public string FirstName
        {
            get {return _firstName;}
            set { _firstName = value.Trim(); }
        }
        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value.Trim(); }
        }

        public int GamesPlayed { get; set; }

        //Should be moved to viewModel
        public string FullName
        {
            get
            {
                return _firstName + " " + _lastName;
            }
        }
        #endregion
       

        public Player()
        {
            Log.Debug("Empty Player object created");
            //When a new player has been created, he's immediatly ready to play.
            IsReadyToPlay = true;
            Games = new List<Game>();
        } 
        
        public Player(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            IsReadyToPlay = true;
            
        }

        /// <summary>
        /// Calculates the number of games difference between now and when the players have played last time. Initial value is 1000, which is practically impossible.
        /// </summary>
        /// <returns>The number of games ago this player has played against the parameter player</returns>
        public int CalculateDifferencePlayedAgainstPlayer(Player player)
        {
            int value = 1000;

            for (int i = 0; i < Games.Count; i++)
            {
                Game currentGame = Games.ElementAt(i);
                if(currentGame.WhitePlayer.Equals(player) || currentGame.BlackPlayer.Equals(player))
                {
                    value = i;
                    break;
                }
            }
            return value;
        }

        
        
    }
}
