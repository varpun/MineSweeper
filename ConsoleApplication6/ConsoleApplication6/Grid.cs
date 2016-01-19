using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication6
{
    public class Grid : IGridOperations
    {
        private Cell[,] _Cell = new Cell[10, 10];
        private int _Row, _Column;
        private int _RowLength = 10;
        private int _ColumnLength = 10;
        private IGridIO _GridIO;

        public Grid(IGridIO gridIO)
        {
            _GridIO = gridIO;
            InitializeBoard();
        }

        public bool Win()
        {
            int count = 0;
            for (int line = 1; line < 9; line++)
            {
                for (int column = 1; column < 9; column++)
                {
                    if (_Cell[line, column].CellState == '*')
                        count++;
                }
            }
            if (count == 10)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void RevealNeighbors()
        {
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (_Cell[_Row + i, _Column + j].MineValue != -1)
                    {
                        _Cell[_Row + i, _Column + j].CellState = _Cell[_Row + i, _Column + j].MineValue.ToString()[0];
                    }
                }
            }
        }

        private int GetPosition(int line, int column)
        {
            return _Cell[line, column].MineValue;
        }

        public bool CheckCellPosition(int turn)
        {
            do
            {
                _GridIO.WriteOutput("\nrow: ");
                _Row = _GridIO.ReadInputNumber();
                _GridIO.WriteOutput("Column: ");
                _Column = _GridIO.ReadInputNumber(); 

                if (_Row < 1 || _Row > 8 || _Column < 1 || _Column > 8)
                {
                    _GridIO.WriteLineOutput("Choose a number between 1 and 8");
                    continue;
                }

                if ((_Cell[_Row, _Column].CellState != '*') && ((_Row < 9 && _Row > 0) && (_Column < 9 && _Column > 0)))
                {
                    _GridIO.WriteLineOutput("Field already shown");
                }

                if (turn == 1)
                {
                    PlaceMines(_Row, _Column);
                    FillSurroundingNeighbours();
                }

            }
            while ((_Row < 1 || _Row > 8 || _Column < 1 || _Column > 8) || (_Cell[_Row, _Column].CellState != '*'));

            if (GetPosition(_Row, _Column) == -1)
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
            _GridIO.WriteLineOutput("\n     Lines");
            for (int line = 8; line > 0; line--)
            {
                _GridIO.WriteOutput("       " + line + " ");

                for (int column = 1; column < 9; column++)
                {
                    _GridIO.WriteOutput("   " + _Cell[line, column].CellState);
                }

                _GridIO.WriteLineOutput(string.Empty);
            }

            _GridIO.WriteLineOutput("\n            1   2   3   4   5   6   7   8");
            _GridIO.WriteLineOutput("                      Columns");
        }

        private void FillSurroundingNeighbours()
        {
            for (int line = 1; line < 9; line++)
            {
                for (int column = 1; column < 9; column++)
                {
                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            if (_Cell[line, column].MineValue != -1)
                            {
                                if (_Cell[line + i, column + j].MineValue == -1)
                                {
                                    _Cell[line, column].MineValue++;
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
                    if (_Cell[i, j].MineValue != -1)
                    {
                        _Cell[i, j].CellState = '#';
                    }
                }
            }
            ShowGrid();
        }

        private void InitializeBoard()
        {
            for (int i = 0; i < _RowLength; i++)
            {
                for (int j = 0; j < _ColumnLength; j++)
                {
                    _Cell[i, j] = new Cell();
                }
            }
        }

        private void PlaceMines(int initialLine, int initialColumn)
        {
            bool shuffle;
            int line, column;
            for (int i = 0; i < 10; i++)
            {
                do
                {
                    line = _GridIO.GetNextRandomNumber() + 1;
                    column = _GridIO.GetNextRandomNumber() + 1;
                    if (line == initialLine && column == initialColumn)
                    {
                        shuffle = true;
                    }
                    else if (_Cell[line, column].MineValue == -1)
                    {
                        shuffle = true;
                    }
                    else
                    {
                        shuffle = false;
                    }
                }
                while (shuffle);
                _Cell[line, column].MineValue = -1;
            }
        }
    }
}
