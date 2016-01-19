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
    public class MinesweeperTest
    {
        private Mock<IGridIO> _GridOps = new Mock<IGridIO>();

        [TestInitialize]
        public void Initialise()
        {
            int[]  minePoints ={ 1, 1, 2, 1, 3, 1, 4, 1, 5, 1, 6, 1, 7, 1, 8, 1, 1, 2, 2, 2 };
            var mineQueue = new Queue<int>();
            foreach(int i in minePoints)
            {
                mineQueue.Enqueue(i-1);
            }
            _GridOps.Setup(x => x.GetNextRandomNumber()).Returns(mineQueue.Dequeue);
        }

        [TestMethod]
        public void MinesweeperTest_WinsGame()
        {
            Grid testGrid = new Grid(_GridOps.Object);
            int turn = 0;
            int[] valuePoints ={4,2,7,2,2,4,5,5,7,5,2,7,5,7,7,7};
            var valueQueue = new Queue<int>();
            foreach(var i in valuePoints)
            {
                valueQueue.Enqueue(i);
            }
            _GridOps.Setup(x =>x.ReadInputNumber()).Returns(valueQueue.Dequeue);
                
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
            Assert.IsTrue(testGrid.Win());
        }

        [TestMethod]
        public void MinesweeperTest_LosesGame()
        {
            Grid testGrid = new Grid(_GridOps.Object);
            int turn = 0;
            int[] valuePoints = { 4, 2, 1, 1 };
            var valueQueue = new Queue<int>();
            foreach (var i in valuePoints)
            {
                valueQueue.Enqueue(i);
            }
            _GridOps.Setup(x => x.ReadInputNumber()).Returns(valueQueue.Dequeue);

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
            Assert.IsFalse(testGrid.Win());
        }
    }
}
