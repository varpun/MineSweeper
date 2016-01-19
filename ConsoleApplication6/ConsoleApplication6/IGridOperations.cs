using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication6
{
    public interface IGridOperations
    {
        bool Win();
        void RevealNeighbors();
        bool CheckCellPosition(int turn);
        void ShowGrid();
        void RevealMines();
    }
}
