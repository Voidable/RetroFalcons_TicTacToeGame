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

            //TESTING
            view.DrawGameMaster("This is a test of the emergency broadcast system");

            Console.ReadLine();
            //ENDTESTING

            //TODO: Create Game Logic
        }
    }
}
