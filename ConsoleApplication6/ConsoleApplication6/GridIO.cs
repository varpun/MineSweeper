using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication6
{
    public class GridIO : IGridIO
    {
        private Random _Random = new Random();

        public void WriteOutput(string output)
        {
            Console.Write(output);
        }

        public void WriteLineOutput(string output) 
        {
            Console.WriteLine(output);
        }

        public int ReadInputNumber()
        {
            return Convert.ToInt32(Console.ReadLine());
        }

        public int GetNextRandomNumber()
        {
            return _Random.Next(8);
        }
    }
}
