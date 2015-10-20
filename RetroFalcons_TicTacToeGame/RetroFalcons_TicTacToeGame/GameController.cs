using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroFalcons_TicTacToeGame
{
    class GameController
    {
        public void RunGameLogic()
        {
            //  First, create the Game Model
            GameModel model = new GameModel();

            //  Then, create the Game View, passing the Model to it
            GameView view = new GameView(model);

            bool playingGame = true;

            view.DrawGameMaster("Welcome to Tic-Tac-Toe! Press any key to play!");
            view.WaitForPlayer();

            while (playingGame)
            {

            }

        }
    }
}
