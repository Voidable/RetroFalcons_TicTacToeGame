using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroFalcons_TicTacToeGame
{
    class PerfectPlayer
    {
        //  This class is an attempt at making an unbeatable tic tac toe game,
        //  it will use a the minmax algorithim to determine the best possible move
        //  The algorithim is a recursive method, if a move wins the game for the current player
        //  it results in +10, if it wins for the opponent its a -10, all other moves are +0.
        //  The depth is also added to the score, to prevent the program from becoming 
        //  fatalistic and throwing away the game.

        #region [ FIELDS ]

        private GameModel _model;
        private GameModel.GameState _currentPlayer;
        private GameModel.GamePiece[,] _currentField;

        private int depthCounter = 0;

        //  choice holds the coords of the best possible move
        private int[] choice = new int[2];

        #endregion

        /// <summary>
        /// Default constructor,gets passed the gamemodel
        /// </summary>
        public PerfectPlayer(GameModel model)
        {
            //  Get refrence to game model
            _model = model;

            //  Get current player, (who's turn the computer will take)
            _currentPlayer = model.State;

            //  Get the current game field
            _currentField = model.Field;
        }

        public void GetBestMove()
        {
            //  Call the minimax method
            MiniMax(_currentField, ref depthCounter);

        }

        private int MiniMax(GameModel.GamePiece[,] field, ref int depth)
        {
            //  Check if game is over, if so, return the score of the game
            if (!(_model.CheckForWinCondition(field) | _model.CheckForCatCondition(field)))
            {
                return ScoreGame(field);
            }

            return 0;
        }

        /// <summary>
        /// Gets passed a gameboard, and returns the score of that board
        /// </summary>
        /// <returns></returns>
        public int ScoreGame(GameModel.GamePiece[,] field)
        {
            int score = 0;

            return score;
        }
    }
}
