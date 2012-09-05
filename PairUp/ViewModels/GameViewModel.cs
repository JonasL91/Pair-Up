using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using Models.Domain;
using log4net;

namespace PairUp.ViewModels
{
    public class GameViewModel : ViewModelBase
    {
        #region Static fields

        private static readonly ILog Log = LogManager.GetLogger(typeof(GameViewModel));

        #endregion

        #region Properties

        private Game _game;
        public Game Game
        {
            get { return _game; }
            set
            {
                if(_game != value)
                {
                    _game = value;
                    RaisePropertyChanged("Game");
                }
            }
        } 
        public Player WhitePlayer
        {
            get { return Game.WhitePlayer; }
            set 
            {
                if(Game.WhitePlayer != value)
                {
                    Game.WhitePlayer = value;
                    RaisePropertyChanged("WhitePlayer");
                }
            }
        }

        public Player BlackPlayer
        {
            get { return Game.BlackPlayer; }
            set
            {
                if(Game.BlackPlayer != value)
                {
                    Game.BlackPlayer = value;
                    RaisePropertyChanged("BlackPlayer");
                }
            }
        }

        public string Result
        {
            get { return ModelResultToGui(Game.Result); }
            set
            {
                if(!Game.Result.Equals(value))
                {
                    Log.Debug("User chose this out of the combobox: "+value);
                    Game.Result = GuiResultToModelResult(value);
                    RaisePropertyChanged("Result");
                }
            }
        }

        public DateTime DateOfResult
        {
            get { return Game.DateOfResult; }
        }
        #endregion
        #region Constructors
        public GameViewModel(Game game)
        {
            this.Game = game;
        }
        #endregion

        #region Methods
        
        #endregion
        #region ConvertMethods
        private double GuiResultToModelResult(string result)
        {
            if (result.Equals("½-½"))
            {
                return 0;
            }
            if(result.Equals("1-0"))
            {
                return 1;
            }
            if (result.Equals("0-1"))
            {
                return 2;
            }
            else return -1;
            
        }

        private string ModelResultToGui(double result)
        {
            if(result.Equals(0))
            {
                return "½-½";
            }
            if(result.Equals(1))
            {
                return "1-0";
            }
            if (result.Equals(2))
            {
                return "0-1";
            }
            else return "";
        }
        #endregion
    }
}
