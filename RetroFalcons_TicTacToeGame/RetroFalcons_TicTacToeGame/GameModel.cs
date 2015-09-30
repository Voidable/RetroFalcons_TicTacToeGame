using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroFalcons_TicTacToeGame
{
    class GameModel
    {
        #region [ ENUMERATIONS ]

        public enum GameState
        {
            PLAYER_X,
            PLAYER_O,
            CAT,
            THREE_IN_ROW
        }

        public enum GamePiece
        {
            NO_VALUE,
            X,
            O
        }

        #endregion


        #region [ FIELDS ]

        private GamePiece[,] _field = new GamePiece[3, 3];
        private GameState _state = GameState.PLAYER_X;

        #endregion


        #region [ PROPERTIES ]

        public GamePiece[,] Field
        {
            get { return _field; }
        }

        public GameState State
        {
            get { return _state; }
            set { _state = value; }
        }

        #endregion


        #region [ METHODS ]

        /// <summary>
        /// Evaluates if a piece can be played on inputed cell.
        /// </summary>
        /// <param name="x">Column number</param>
        /// <param name="y">Row number</param>
        /// <returns></returns>
        public bool EvaluateValidMove(int x, int y)
        {
            bool valid = false;

            if ((x >= 0 & x <= 3) & y >= 0 & y <= 3) // If the input coordinates are within valid limits
            {
                if (_field[x, y] == GamePiece.NO_VALUE) //  If the cell is empty, it is valid to put a piece in it.
                    valid = true;
            }

            return valid;   //  This will be false unless it passes both conditions
        }

        /// <summary>
        /// Inputs a piece into an empty cell, evaluating input for validity. Gets correct piece from Game's current state.
        /// </summary>
        /// <param name="x">Column number</param>
        /// <param name="y">Row number</param>
        public void InputMove(int x, int y)
        {
            //  Validate the inputs
            if (EvaluateValidMove(x,y)) //  Valid move
            {
                //  Set the current piece in the field
                _field[x, y] = GetCurrentGamePiece();

                //  Switch players
                SwitchCurrentPlayer();
            }
            else   //   Invalid move
            {
                throw new ArgumentException("Invalid move entered, Validate your inputs!");
            }
        }

        public GamePiece GetCurrentGamePiece()
        {
            //  Get the correct piece
            GamePiece piece = GamePiece.NO_VALUE;   //  Initialized to NO_VALUE to make compiler happy, it WILL be properly assigned before it gets returned.

            switch (_state) //  Switch on game state
            {
                case GameState.PLAYER_X:    //  Its player X's turn
                    piece = GamePiece.X;
                    break;
                case GameState.PLAYER_O:    //  It's player O's turn
                    piece = GamePiece.O;
                    break;
                case GameState.CAT:     //  This should not happen
                    throw new ArgumentException("Should not be inputing a move when the game is over!, CAT");
                case GameState.THREE_IN_ROW:    //  This should not happen
                    throw new ArgumentException("Should not be inputing a move when the game is over!, 3-IN-ROW");
            }

            return piece;
        }

        public void SwitchCurrentPlayer()
        {
            //  Change Players
            if (_state == GameState.PLAYER_O)
                _state = GameState.PLAYER_X;
            else if (_state == GameState.PLAYER_X)
                _state = GameState.PLAYER_O;
        }

        #endregion


        #region [ CONSTRUCTORS ]

        #endregion
    }
}
