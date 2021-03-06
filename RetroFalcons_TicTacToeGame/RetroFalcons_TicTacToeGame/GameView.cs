﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroFalcons_TicTacToeGame
{
    class GameView
    {
        #region [ FIELDS ]
        //  Dimensions of the Console
        private const int CONSOLE_WIDTH = 41;
        private const int CONSOLE_HEIGHT = 35;

        //  Width of the message box
        private const int MESSAGE_BOX_WIDTH = 27;
        private const int MESSAGE_LINE_WIDTH = MESSAGE_BOX_WIDTH - 4;

        //  Size of the grid
        private const int GRID_SIZE = 13;

        //  Refrence to the model
        private GameModel _model;

        // Character definitions
        private char _block = '#'; //  Outlines the message box and grid
        private const char vertLine = '|';  //  Vertical lines
        private const char horLine = '-';   //  Horizontal Lines
        private const char cross = '+';     //  Cross of horizontal and vertical lines

        //  Color definitions for characters
        ConsoleColor blockColor = ConsoleColor.DarkYellow;
        ConsoleColor lineColor = ConsoleColor.DarkGray;
        ConsoleColor normal = ConsoleColor.White;
        ConsoleColor xColor = ConsoleColor.Red;
        ConsoleColor oColor = ConsoleColor.Blue;

        #endregion

        #region [ FIELDS ]

        public char Block
        {
            get { return _block; }
            set { value = _block; }
        }

        #endregion

        #region [ METHODS ]

        /// <summary>
        /// Clears then re-draws the grid
        /// </summary>
        /// <param name="message"></param>
        public void DrawGameMaster(string message)
        {
            //  Clears the grid
            Console.Clear();

            //  Draw the grid
            DrawGameGrid();

            //  Draw the message box
            DrawMessageBox(message);
        }

        /// <summary>
        /// Retrieves the grid from the Model, then draws it in the middle of the screen
        /// </summary>
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
                        grid[x, y] = _model.Field[x, y].ToString().ToUpper();
                }
            }

            //  Create the buffer to center the grid
            int bufferValue = ((Console.WindowWidth / 2) - (GRID_SIZE / 2));

            //  Create list to hold the lines of the grid
            List<string> boardLines = new List<string>();

            boardLines.Add("#############");
            boardLines.Add("#   |   |   #");
            boardLines.Add(string.Format("# {0} | {1} | {2} #", grid[0, 2], grid[1, 2], grid[2, 2]));
            boardLines.Add("#   |   |   #");
            boardLines.Add("#---+---+---#");
            boardLines.Add("#   |   |   #");
            boardLines.Add(string.Format("# {0} | {1} | {2} #", grid[0, 1], grid[1, 1], grid[2, 1]));
            boardLines.Add("#   |   |   #");
            boardLines.Add("#---+---+---#");
            boardLines.Add("#   |   |   #");
            boardLines.Add(string.Format("# {0} | {1} | {2} #", grid[0, 0], grid[1, 0], grid[2, 0]));
            boardLines.Add("#   |   |   #");
            boardLines.Add("#############");

            //  Iterate through each line
            foreach (string l in boardLines)
            {
                //  Create list of characters
                List<string> lineChars = l.Select(c => c.ToString()).ToList();

                //  Set the cursor location
                Console.CursorLeft = bufferValue;

                //  Iterate through each character
                foreach (string c in lineChars)
                {
                    if (c == _block.ToString())  //  Character is block
                    {
                        Console.ForegroundColor = blockColor;
                        Console.Write(c);
                        Console.ForegroundColor = normal;
                    }
                    //  Character is line
                    else if (c == horLine.ToString() || c == vertLine.ToString() || c == cross.ToString())
                    {
                        Console.ForegroundColor = lineColor;
                        Console.Write(c);
                        Console.ForegroundColor = normal;
                    }
                    else if (c == "X")   //   Character is X
                    {
                        Console.ForegroundColor = xColor;
                        Console.Write(c);
                        Console.ForegroundColor = normal;
                    }
                    else if (c == "O")   //   Character is O
                    {
                        Console.ForegroundColor = oColor;
                        Console.Write(c);
                        Console.ForegroundColor = normal;
                    }
                    else
                    {
                        Console.Write(c);
                    }
                }
                //  Go to next line
                Console.Write("\n");

            }

            //  Insert blank line
            Console.WriteLine("");
        }

        /// <summary>
        /// Draws the message box, which size adapts to the message passed in. Colors the border
        /// </summary>
        /// <param name="inputMessage"></param>
        public void DrawMessageBox(string inputMessage)
        {
            //  Create the buffer to center the Message Box
            int bufferValue = ((CONSOLE_WIDTH / 2) - (MESSAGE_BOX_WIDTH / 2));

            string bar = new string(_block, MESSAGE_BOX_WIDTH); //   filled bar for message box
            string leftBar = _block + " ";   //  Encloses the left of a message line
            string rightBar = " " + _block;  //  Encloses the right of a message line

            //  Create the empty bar
            string spacer = new string(' ', (MESSAGE_BOX_WIDTH - 2));
            string emptyBar = _block + spacer + _block;

            //  Create list of strings
            List<string> lines = new List<string>();

            //  Add the top of box
            lines.Add(bar);
            lines.Add(emptyBar);

            #region Add middle of box -- old version

            ////  Define the length of the chunks of the message
            //int messageLineLength = MESSAGE_BOX_WIDTH - 4;

            ////  Split the input string into lines
            //List<string> messageLines = new List<string>();
            //for (int i = 0; i < inputMessage.Length; i += messageLineLength)    //  Iterate through the input
            //{
            //    if ((i + messageLineLength) < inputMessage.Length)  //  If we are before the last chunk
            //        messageLines.Add(string.Format(leftBar + inputMessage.Substring(i, messageLineLength)) + rightBar);
            //    else   //   On the last chunk
            //        messageLines.Add(leftBar + inputMessage.Substring(i).PadRight(messageLineLength % inputMessage.Length) + rightBar);
            //}

            ////  Add the split message lines into the lines to write
            //foreach (string line in messageLines)
            //{
            //    lines.Add(line);
            //}


            #endregion

            #region Add middle of box -- Wordwrap version

            //  Split the message into lines, wrapping words to next line
            string[] words = inputMessage.Split(' ');
            string line = "";
            foreach (string word in words)
            {
                //  If the word does not make the line go over, and has room for an extra space, add it to the line
                if ((word.Length + line.Length) < MESSAGE_LINE_WIDTH)
                {
                    line += word + " ";

                    //  If the word is the last word in the message, finish creating the line
                    if (word == words.Last())
                    {
                        //  Pad the line to make it the right width
                        line = line.PadRight((MESSAGE_LINE_WIDTH), ' ');

                        //  Add the begining and ending bars to the lines
                        line = leftBar + line + rightBar;

                        lines.Add(line); // Add the current line to the list
                    }
                }
                //  If the word makes the line equal the max length, add it to the line and add line to list
                else if ((word.Length + line.Length) == MESSAGE_LINE_WIDTH)
                {
                    line += word;

                    //  Pad the line to make it the right width
                    line = line.PadRight((MESSAGE_LINE_WIDTH), ' ');

                    //  Add the begining and ending bars to the lines
                    line = leftBar + line + rightBar;

                    lines.Add(line); // Add the current line to the list

                    //  Clear the line
                    line = "";

                }
                else // The word would go into the next line
                {
                    //  Pad the line to make it the right width
                    line = line.PadRight((MESSAGE_LINE_WIDTH), ' ');

                    //  Add the begining and ending bars to the lines
                    line = leftBar + line + rightBar;

                    lines.Add(line); // Add the current line to the list
                    line = word; //   Make line just the word

                    //  Add a space if the word is less than the line
                    if (line.Length < MESSAGE_LINE_WIDTH)
                        line += " ";

                    //  If the word is the last word in the message, add it to a line
                    if (word == words.Last())
                    {
                        //  Pad the line to make it the right width
                        line = line.PadRight((MESSAGE_LINE_WIDTH), ' ');

                        //  Add the begining and ending bars to the lines
                        line = leftBar + line + rightBar;

                        lines.Add(line); // Add the current line to the list
                    }
                }
            }


            #endregion


            //  Add the bottom of box
            lines.Add(emptyBar);
            lines.Add(bar);

            //  Write each line one character at a time
            #region Write lines
            foreach (string l in lines)
            {
                //  Create list of characters
                List<string> lineChars = l.Select(c => c.ToString()).ToList();

                //  Set the cursor location
                Console.CursorLeft = bufferValue;

                //  Iterate through each character
                foreach (string c in lineChars)
                {
                    if (c == _block.ToString())  //  Character is block
                    {
                        Console.ForegroundColor = blockColor;
                        Console.Write(c);
                        Console.ForegroundColor = normal;
                    }
                    else   //   Character is not block
                    {
                        Console.Write(c);
                    }
                }
                //  Go to next line
                Console.Write("\n");

            }
            #endregion

            //  Write blank line
            Console.WriteLine();

        }

        /// <summary>
        /// Returns a string from the player's input
        /// </summary>
        /// <returns></returns>
        public string GetPlayerInput()
        {
            string output = "";

            output = Console.ReadLine();

            return output;
        }

        /// <summary>
        /// Simply makes the program wait until the player presses a button
        /// </summary>
        public void WaitForPlayer()
        {
            Console.ReadLine();
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
