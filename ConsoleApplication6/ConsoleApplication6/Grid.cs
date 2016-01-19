using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication6
{
    public class Grid
    {
        private int[,] mines;
        private char[,] actualGrid;
        private int row, column;
        int rowLength = 10;
        int columnLength = 10;
        Random random = new Random();

        public Grid()
        {
            mines = new int[10, 10];
            actualGrid = new char[10, 10];
            InitializeMines();
            
            StartBoard();
        }

        public bool Win()
        {
            int count = 0;
            for (int line = 1; line < 9; line++)
            {
                for (int column = 1; column < 9; column++)
                {
                    if (actualGrid[line, column] == '*')
                        count++;
                }
            }
            if (count == 10)
                return true;
            else
                return false;
        }

        public void RevealNeighbors()
        {
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (mines[row + i, column + j] != -1)
                    {
                        actualGrid[row + i, column + j] = mines[row + i, column + j].ToString()[0];
                    }
                }
            }
        }

        public int GetPosition(int line, int column)
        {
            return mines[line, column];
        }

        public bool ReadAndSetPosition(int turn)
        {
            do
            {
                Console.Write("\nrow: ");
                row = Convert.ToInt32(Console.ReadLine());
                Console.Write("Column: ");
                column = Convert.ToInt32(Console.ReadLine());
                
                if ((actualGrid[row, column] != '*') && ((row < 9 && row > 0) && (column < 9 && column > 0)))
                { 
                    Console.WriteLine("Field already shown"); 
                }

                if (row < 1 || row > 8 || column < 1 || column > 8)
                { 
                    Console.WriteLine("Choose a number between 1 and 8"); 
                }

                if (turn == 1)
                {
                    PlaceMines(row, column);
                    FillNoOfSurroundingNeighbours();
                }

            } 
            while ((row < 1 || row > 8 || column < 1 || column > 8) || (actualGrid[row, column] != '*'));

            if (GetPosition(row, column) == -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ShowGrid()
        {
            Console.WriteLine("\n     Lines");
            for (int line = 8; line > 0; line--)
            {
                Console.Write("       " + line + " ");

                for (int column = 1; column < 9; column++)
                {
                    Console.Write("   " + actualGrid[line, column]);
                }

                Console.WriteLine();
            }

            Console.WriteLine("\n            1   2   3   4   5   6   7   8");
            Console.WriteLine("                      Columns");
        }

        public void FillNoOfSurroundingNeighbours()
        {
            for (int line = 1; line < 9; line++)
            {
                for (int column = 1; column < 9; column++)
                {
                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            if (mines[line, column] != -1)
                            {
                                if (mines[line + i, column + j] == -1)
                                {
                                    mines[line, column]++;
                                }
                            }
                        }
                    }
                }
            }
        }

        public void RevealMines()
        {

            for (int i = 1; i < 9; i++)
            {
                for (int j = 1; j < 9; j++)
                {
                    if (mines[i, j] == -1)
                    {
                        actualGrid[i, j] = '#';
                    }
                }
            }
            ShowGrid();
        }

        public void StartBoard()
        {
            for (int i = 1; i < rowLength; i++)
            {
                for (int j = 1; j < columnLength; j++)
                {
                    actualGrid[i, j] = '*';
                }
            }
        }

        public void InitializeMines()
        {
            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < columnLength; j++)
                {
                    mines[i, j] = 0;
                }
            }
        }

        public void PlaceMines(int initialLine,int initialColumn)
        {
            bool shuffle;
            int line, column;
            for (int i = 0; i < 10; i++)
            {
                do
                {
                    line = random.Next(8) + 1;
                    column = random.Next(8) + 1;
                    if(line == initialLine  && column == initialColumn)
                    {
                        shuffle = true;
                    }
                    else if (mines[line, column] == -1)
                    {
                        shuffle = true;
                    }
                    else
                    {
                        shuffle = false;
                    }
                }
                while (shuffle);
                mines[line, column] = -1;
            }
        }
    }
}
