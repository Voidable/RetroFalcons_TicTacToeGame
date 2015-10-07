using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroFalcons_TicTacToeGame
{
    class GameView
    {
        #region [ FIELDS ]
        private const int CONSOLE_WIDTH = 31;
        private const int CONSOLE_HEIGHT = 35;

        private const int MESSAGE_BOX_WIDTH = 17;

        private GameModel _model;

        #endregion


        #region [ PROPERTIES ]

        #endregion


        #region [ METHODS ]

        public void DrawGameMaster(string message)
        {
            //  Draw the grid
            DrawGameGrid();

            //  Draw the message box
            DrawMessageBox(message);
        }

        public void DrawGameGrid()
        {
            //  Get the GameModel field and convert to strings
            string[,] grid = new string[3, 3];
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    if (_model.Field[x, y] == GameModel.GamePiece.NO_VALUE)
                        grid[x, y] = " ";
                    else
                        grid[x, y] = _model.Field[x, y].ToString();
                }
            }

            //  Create the buffer to center the grid
            int bufferValue = ((Console.WindowWidth / 2) - (13 / 2));//13 is the width of the grid
            string buffer = new string(' ', bufferValue);

            //  The Grid itself
            Console.WriteLine("{0}#############", buffer);
            Console.WriteLine("{0}#   |   |   #", buffer);
            Console.WriteLine("{3}# {0} | {1} | {2} #", grid[0, 2], grid[1, 2], grid[2, 2], buffer);
            Console.WriteLine("{0}#   |   |   #", buffer);
            Console.WriteLine("{0}#-----------#", buffer);
            Console.WriteLine("{0}#   |   |   #", buffer);
            Console.WriteLine("{3}# {0} | {1} | {2} #", grid[0, 1], grid[1, 1], grid[2, 1], buffer);
            Console.WriteLine("{0}#   |   |   #", buffer);
            Console.WriteLine("{0}#-----------#", buffer);
            Console.WriteLine("{0}#   |   |   #", buffer);
            Console.WriteLine("{3}# {0} | {1} | {2} #", grid[0, 0], grid[1, 0], grid[2, 0], buffer);
            Console.WriteLine("{0}#   |   |   #", buffer);
            Console.WriteLine("{0}#############", buffer);

            //  Insert blank line
            Console.WriteLine("");
        }

        public void DrawMessageBox(string inputMessage)
        {
            //  Create the buffer to center the Message Box
            int bufferValue = ((CONSOLE_WIDTH / 2) - (MESSAGE_BOX_WIDTH / 2));//17 is the width of the message box
            string buffer = new string(' ', bufferValue);

            //  Top of message box
            Console.WriteLine("{0}#~~~~~~~~~~~~~~~#", buffer);
            Console.WriteLine("{0}#               #", buffer);

            //  Generate the middle of the message box
            //  Text gets divided into character segments, with a length of 4 less than box, width then gets '# ' and ' #'
            int lineLength = MESSAGE_BOX_WIDTH - 4;

            #region Credit for this code block goes to http://stackoverflow.com/users/306651/hans-olsson
            List<string> chunks = new List<string>();
            for (int i = 0; i < inputMessage.Length; i += lineLength)
            {
                if ((i + lineLength) < inputMessage.Length)
                    chunks.Add(inputMessage.Substring(i, lineLength));
                else
                    chunks.Add(inputMessage.Substring(i));
            }
            #endregion

            //  Write each line
            foreach (string chunk in chunks)
            {
                string endPadding = new string(' ', ((MESSAGE_BOX_WIDTH - 4) - chunk.Length));
                string line = buffer + "# " + chunk + endPadding + " #";
                Console.WriteLine(line);
            }

            //  Bottom of message box
            Console.WriteLine("{0}#               #", buffer);
            Console.WriteLine("{0}#_______________#", buffer);
        }


        #endregion


        #region [ CONSTRUCTORS ]

        /// <summary>
        /// Constructor, allows passing the GameModel object
        /// </summary>
        /// <param name="model"></param>
        public GameView(GameModel modelObject)
        {
            //  Assign the passed Model to the internal pointer
            _model = modelObject;

            //  Adjust the console window
            Console.WindowWidth = CONSOLE_WIDTH;
            Console.WindowHeight = CONSOLE_HEIGHT;
        }

        #endregion


    }
}
