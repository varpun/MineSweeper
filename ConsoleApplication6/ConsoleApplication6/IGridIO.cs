using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication6
{
    public interface IGridIO
    {
        void WriteOutput(string output);
        void WriteLineOutput(string output);
        int ReadInputNumber();
        int GetNextRandomNumber();
    }
}
