using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Models.Domain;
using log4net;

namespace Models.Domain
{

    public class Tournament
    {
        #region Static fields
        private static readonly ILog log = LogManager.GetLogger(typeof (Tournament));
        // Variables used by the PairUp method this can be changed later on.
        private const int LastTimePlayedAgainstOtherPlayer = 5;
        private const int MaxPositionsRemovedFromOtherPlayer = 5;
        #endregion
        #region Properties
        public string Name { get; set; }
        //When we aks the players list, it's currently always ordered by points.
        private List<Player> _players; 
        public List<Player> Players
        {
            get 
            { 
                _players = _players.OrderBy(p => p.Points).ToList();
                return _players;
            } 
            set { _players = value; }
        }
        public Collection<Game> Games { get; set; } 
        private readonly DateTime _date;
        #endregion
        #region Constructor
        //Create a new Tournament with the current date and an empty list of Players.
        public Tournament()
        {
            this._date = DateTime.Now;
            Players = new List<Player>();
            log.Debug("New Tournament Created with date: " + _date);
            Games = new Collection<Game>();
        }
        #endregion
        #region Public Methods

        public void AddPlayer(Player player)
        {
            //Possible other validation to add a new Player, eg. first name and last name already exist.
            //A player must have a first name. Does a player must have a last name in order to play?
            if ((!DoesPlayerExistInTournament(player)) && (DoesPlayerHasNames(player)))
            {
                Players.Add(player);
                log.Debug("Player " + player.FirstName + " has been added to the tournament");
            }
        }

       /// <summary>
       /// The method to make the new games.
       /// We query the players list to filter all the players that are ready to play.
       /// Then we sort them by points, but descending, because we start to pair with the last player.
       /// The method is recursive (Do while) Until less or then 2 players are in the list. (Because you need two players to pair obviously).
       /// 
       /// </summary>
       /// //TODO method not optimized so far.
        public void PairUp()
        {
            List<Player> playersToBePaired = (from p in Players
                                                                 where p.IsReadyToPlay == true
                                                                 orderby p.Points descending 
                                                                 select p).ToList();
           Collection<Game> pairings = new Collection<Game>();
           do
           {
               Player currentPlayer = playersToBePaired.First();
               for (int i = 1; i < playersToBePaired.Count(); i++)
               {
                   //Potential player is the player at position i. Which is first the player above. 
                   //If they can't play against each other we will try with a player more above. And so on
                   Player potentialOpponent = playersToBePaired.ElementAt(i);
                   if(CanPlayAgainstPlayer(currentPlayer, potentialOpponent))
                   {
                       Player[] p = DeterminePlayerColor(currentPlayer, potentialOpponent);
                       Game game = new Game(p[0], p[1]);
                       pairings.Add(game);
                       playersToBePaired.Remove(currentPlayer);
                       playersToBePaired.Remove(potentialOpponent);
                       //exit the loop and goes to the while check.
                       break;
                   }
               }
               

           } while (playersToBePaired.Count() > 2 );
            foreach(Game game  in pairings)
            {
                Games.Add(game);
            }
           
        }
        #endregion

       
 
        #region CanAddPlayerToTournament

       
        /// <summary>
        /// Current rules for adding a new player:
        /// - Player must have a first name and a last name. (Can be changed later to first name only eventually).
        /// - Player first name and last name combination must be unique in the tournament.
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public bool DoesPlayerExistInTournament(Player player)
        {
            return Players.Any(p => p.FirstName.Equals(player.FirstName, StringComparison.InvariantCultureIgnoreCase) && p.LastName.Equals(player.LastName, StringComparison.InvariantCultureIgnoreCase));
        }

        private bool DoesPlayerHasNames(Player player)
        {
            if (string.IsNullOrEmpty(player.FirstName) || string.IsNullOrEmpty(player.LastName))
            {
                return false;
            }
            else return true;
        }
        #endregion

        #region PairUp
        //Check of the two domain rules (see constants above)
        //TODO Move method to Models/Player
        private bool CanPlayAgainstPlayer(Player worstInRank, Player betterInRank)
        {
            
            //The last check is too see if the players played already against each other
            if (((worstInRank.CurrentRank - betterInRank.CurrentRank) < MaxPositionsRemovedFromOtherPlayer) &&
                worstInRank.CalculateDifferencePlayedAgainstPlayer(betterInRank) > LastTimePlayedAgainstOtherPlayer)
            {
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Determines who will play with white and black. 
        /// </summary>
        /// <param name="playerA">The first player</param>
        /// <param name="playerB">The second player</param>
        /// <returns>An array of Players, where Player[0] is white and Player[1] is black.</returns>
        private Player[] DeterminePlayerColor(Player playerA, Player playerB)
        {
            Player[] players = new Player[2];
            if(playerA.WhiteBlackBalance < playerB.WhiteBlackBalance)
            {
                players[0] = playerA;
                players[1] = playerB;
            }
            if(playerA.WhiteBlackBalance > playerB.WhiteBlackBalance)
            {
                players[0] = playerB;
                players[1] = playerA;
            }
            if(playerA.WhiteBlackBalance == playerB.WhiteBlackBalance)
            {
                if (playerA.CurrentRank < playerB.CurrentRank)
                {
                    players[0] = playerA;
                    players[1] = playerB;
                }
                else
                {
                    players[0] = playerB;
                    players[1] = playerA;
                }
            }
            return players;
        }
        #endregion
    }
}
