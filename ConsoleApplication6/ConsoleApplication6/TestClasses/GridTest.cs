using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ConsoleApplication6
{
    [TestClass()]
    public class GridTest
    {
        private Mock<IGridIO> _GridIO = new Mock<IGridIO>();
        private string _Message = string.Empty;

        [TestInitialize]
        public void Initialise()
        {
            int[] minePoints = { 1, 1, 2, 1, 3, 1, 4, 1, 5, 1, 6, 1, 7, 1, 8, 1, 1, 2, 2, 2 };
            var mineQueue = new Queue<int>();
            foreach (int i in minePoints)
            {
                mineQueue.Enqueue(i - 1);
            }
            _GridIO.Setup(x => x.GetNextRandomNumber()).Returns(mineQueue.Dequeue);
            _GridIO.Setup(x => x.WriteLineOutput(It.IsAny<string>())).Callback<string>(r => _Message = r);
        }

        [TestMethod]
        public void GridTest_RowAndColumnGreaterThanMax()
        {
            Grid testGrid = new Grid(_GridIO.Object);
            int turn = 0;
            int[] valuePoints = { 4, 2, 9, 9, 1, 1 };
            var valueQueue = new Queue<int>();
            foreach (var i in valuePoints)
            {
                valueQueue.Enqueue(i);
            }
            _GridIO.Setup(x => x.ReadInputNumber()).Returns(valueQueue.Dequeue);

            bool finish;
            do
            {
                turn++;
                finish = testGrid.CheckCellPosition(turn);
                if (!finish)
                {
                    testGrid.RevealNeighbors();
                    finish = testGrid.Win();
                }
            }
            while (!finish);
            Assert.AreSame("Choose a number between 1 and 8", _Message);
        }

        [TestMethod]
        public void GridTest_RowAndColumnRepeated()
        {
            Grid testGrid = new Grid(_GridIO.Object);
            int turn = 0;
            int[] valuePoints = { 4, 2, 4, 2, 1, 1 };
            var valueQueue = new Queue<int>();
            foreach (var i in valuePoints)
            {
                valueQueue.Enqueue(i);
            }
            _GridIO.Setup(x => x.ReadInputNumber()).Returns(valueQueue.Dequeue);

            bool finish;
            do
            {
                turn++;
                finish = testGrid.CheckCellPosition(turn);
                if (!finish)
                {
                    testGrid.RevealNeighbors();
                    finish = testGrid.Win();
                }
            }
            while (!finish);
            Assert.AreSame("Field already shown", _Message);
        }
    }
}
