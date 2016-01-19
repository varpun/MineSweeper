using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication6
{
    public class Play
    {
        private Grid grid;
        bool finish = false;
        int turn = 0;

        static void Main(string[] args)
        {
            new Play();
        }

        public Play()
        {
            grid = new Grid();
            StartGame(grid);
        }

        public void StartGame(Grid grid)
        {
            do
            {
                turn++;
                Console.WriteLine("Turn " + turn);
                grid.ShowGrid();                
                finish = grid.CheckCellPosition(turn);

                if (!finish)
                {
                    grid.RevealNeighbors();
                    finish = grid.Win();
                }

            }
            while (!finish);

            if (grid.Win())
            {
                Console.WriteLine("You Won the game in  " + turn + " turns :)");
                grid.RevealMines();
            }
            else
            {
                Console.WriteLine("ooh you step over the Mine, :(");
                grid.RevealMines();
            }
        }
    }
}
