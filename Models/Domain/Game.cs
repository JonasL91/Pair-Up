using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using log4net;

namespace Models.Domain
{
    public class Game 
    {

        #region Static fields
        private static readonly ILog Log = LogManager.GetLogger(typeof(Game));
        #endregion
        //Each game has a white and a black player.
        public Player WhitePlayer { get; set; }
        public Player BlackPlayer { get; set; }

        ///-1 = not played, 0 = draw, 1 = white wins, 2 = black wins 
        private double _result;
        public double Result
        {
            get { return _result; } 
            set 
            {
                Log.Debug("Setting the result for: "+WhitePlayer.FirstName+" and "+ BlackPlayer.FirstName);
                //Means it's not the first time the result has been set, so we must remove the current result before adding the new one.
                if(!IsInProgress)
                {
                    RemovePointsFromPlayers((int)_result);
                }
                
                AddPointsToPlayers((int) value);
                
                _result = value;
                IsInProgress = false;
                DateOfResult = DateTime.Now;
               
            }
        }

        public DateTime DateOfResult { get; set; }
        public int RoundNumber { get; set; }
       
        
        public bool IsInProgress { get; set; }

        public Game()
        {
            
        }   

        public Game(Player wPlayer, Player bPlayer)
        {
            this.WhitePlayer = wPlayer;
            this.BlackPlayer = bPlayer;
            //When a new game has been created, we set it immediatly to 'IsInProgress'
            IsInProgress = true;
            WhitePlayer.Games.Add(this);
            BlackPlayer.Games.Add(this);
            
        }

        /// <summary>
        /// This method removes points from the players. 
        /// Practically this method will be used when a result of a game has been changed.
        /// </summary>
        /// <param name="result">The result that we need to change. Basically the reverse of the AddPointsToPlayers method.</param>
        private void RemovePointsFromPlayers(int result)
        {
            Log.Debug("Removing "+result+" points");
            switch (result)
            {
                case 0:
                    WhitePlayer.Points = -0.5;
                    BlackPlayer.Points = -0.5;
                    Log.Debug("Removed 0.5 points for both players.");
                    break;
                case 1:
                    WhitePlayer.Points = -1;
                    Log.Debug("Removed 1 point for: "+WhitePlayer.FirstName);
                    break;
                case 2:
                    BlackPlayer.Points = -1;
                    Log.Debug("Removed 1 point for: " + BlackPlayer.FirstName);
                    break;
            }
        }
        /// <summary>
        /// This method adds points to the players of this game.
        /// The general rules are:
        ///     0: The game ended in a draw.
        ///     1: The white player won.
        ///     2: The black player won.
        /// According to this we add a points to a player.
        /// </summary>
        /// <param name="result">The result we need to add.</param>
        private void AddPointsToPlayers(int result)
        {
            Log.Debug("Adding " + result + " points");
            switch (result)
            {
                case 0:
                    WhitePlayer.Points = +0.5;
                    BlackPlayer.Points = +0.5;
                    Log.Debug("Added 0.5 points for both players.");
                    break;
                case 1:
                    WhitePlayer.Points = +1;
                    Log.Debug("Added 1 point for: " + WhitePlayer.FirstName);
                    break;
                case 2:
                    BlackPlayer.Points = +1;
                    Log.Debug("Added 1 point for: " + BlackPlayer.FirstName);
                    break;

            }
        }

        
    }
}
