using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroFalcons_TicTacToeGame
{
    class Program
    {
        static void Main(string[] args)
        {
            //  Create game controller object
            GameController controller = new GameController();

            //  Instruct controller to run the game
            controller.RunGameLogic();
        }
    }
}
