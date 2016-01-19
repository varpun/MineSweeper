using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication6
{
    public class Cell
    {
        public Cell()
        {
            CellState = '*';
            MineValue = 0;
        }

        public char CellState
        {
            get;
            set;
        }

        public int MineValue
        {
            get;
            set;
        }
    }
}
