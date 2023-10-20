namespace Undirected_graph.Tests
{
    [TestClass]
    public class UndirectedGraphTests
    {
        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        [DataRow(-1)]
        [DataRow(4)]
        public void IntIndexerGetTest(int index)
        {
            //arrange
            var graph = new UndirectedGraph<int>(1,2,3);

            //act
            _ = graph[index];
        }

        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        [DataRow(-1)]
        [DataRow(4)]
        public void IntIndexerSetTest(int index)
        {
            //arrange
            var graph = new UndirectedGraph<int>(1, 2, 3);

            //act
            graph[index] = 0;
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void IntIndexerSetNullTest()
        {
            //arrange
            var graph = new UndirectedGraph<string?>("1", "2", "3");

            //act
            graph[0] = null;
        }

        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        [DataRow(-1,1)]
        [DataRow(1,-1)]
        [DataRow(4, 1)]
        [DataRow(1, 4)]
        public void IntIntIndexerGetTest(int index1, int index2)
        {
            //arrange
            var graph = new UndirectedGraph<int>(1, 2, 3);

            //act
            _ = graph[index1, index2];
        }

        [TestMethod, ExpectedException(typeof(Exception))]
        public void IntIntIndexerGetEmptyGraphTest()
        {
            //arrange
            var graph = new UndirectedGraph<int>();

            //act
            _ = graph[0, 0];
        }

        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        [DataRow(-1, 1)]
        [DataRow(1, -1)]
        [DataRow(4, 1)]
        [DataRow(1, 4)]
        public void IntIntIndexerSetTest(int index1, int index2)
        {
            //arrange
            var graph = new UndirectedGraph<int>(1, 2, 3);

            //act
            graph[index1, index2] = true;
        }

        [TestMethod, ExpectedException(typeof(Exception))]
        public void IntIntIndexerSetEmptyGraphTest()
        {
            //arrange
            var graph = new UndirectedGraph<int>();

            //act
            graph[0, 0] = true;
        }

        [TestMethod()]
        public void AddTest()
        {
            //arrange
            var actual = new UndirectedGraph<int>(1);
            var expected = new UndirectedGraph<int>(1, 2);

            //act
            actual.Add(2);

            //assert
            CollectionAssert.AreEquivalent(expected, actual);
        }

        [TestMethod()]
        public void AddTest_NullGraph()
        {
            //arrange
            var actual = new UndirectedGraph<int>();
            var expected = new UndirectedGraph<int>(1);

            //act
            actual.Add(1);

            //assert
            CollectionAssert.AreEquivalent(expected, actual);
        }

        [TestMethod()]
        public void ClearTest()
        {
            //arrange
            var actual = new UndirectedGraph<int>(1, 2, 3);
            var expeted = new UndirectedGraph<int>();

            //act
            actual.Clear();

            //assert
            CollectionAssert.AreEquivalent(expeted, actual);
        }

        [TestMethod()]
        public void ContainsTest()
        {
            //arrange
            var graph = new UndirectedGraph<int>(1, 2, 3);

            //act
            var flag = graph.Contains(1);

            //assert
            Assert.IsTrue(flag);
        }

        [TestMethod()]
        public void NotContainsTest()
        {
            //arrange
            var graph = new UndirectedGraph<int>(1, 2, 3);

            //act
            var flag = graph.Contains(4);

            //assert
            Assert.IsFalse(flag);
        }

        [TestMethod()]
        public void CopyToTest()
        {
            //arrange
            var graph = new UndirectedGraph<int>(1, 2, 3);
            int[] expected = { 1, 2, 3 };
            int[] actual = new int[3];

            //act
            graph.CopyTo(actual, 0);

            //assert
            CollectionAssert.AreEquivalent(expected, actual);
        }

        [TestMethod()]
        public void RemoveTest()
        {
            //arrange
            var graph = new UndirectedGraph<int>(1, 2, 3);

            //act
            var flag = graph.Remove(1);

            //assert
            Assert.IsFalse(graph.Contains(1) && flag, flag.ToString());
        }

        [TestMethod()]
        public void RemoveTest_NotContainedItem()
        {
            //arrange
            var graph = new UndirectedGraph<int>(1, 2, 3);

            //act
            var flag = graph.Remove(4);

            //assert
            Assert.IsFalse(flag);
        }

        [TestMethod()]
        public void GetPairTest()
        {
            //arrange
            var vertexes = new int[] { 1, 2, 3 };
            bool[,]? matrix = new bool[,]
            {
                {false, true, false},
                {true, false, false},
                {false, false, true},
            };
            var graph = new UndirectedGraph<int>(matrix, new List<int>(vertexes));
            var expected = new List<(int, int)> { (1, 2), (2, 1), (3, 3) };
            var actual = new List<(int, int)>();

            //act
            foreach ((int, int) item in graph.GetPair())
            {
                actual.Add(item);
            }

            //assert
            CollectionAssert.AreEquivalent(expected, actual);
        }

        [TestMethod()]
        public void CloneTest()
        {
            //arrange
            bool[,]? matrix =
            {
                {false, true, false},
                {true, false, false},
                {false, false, true},
            };
            var expected = new UndirectedGraph<int>(matrix, 1, 2, 3);

            //act
            var actual = (UndirectedGraph<int>)expected.Clone();

            //assert
            CollectionAssert.AreEquivalent(expected, actual);
        }

        [TestMethod()]
        public void ToStringTest()
        {
            //arrange
            var expected = "V\t1\t2\t3\r\n1\t0\t1\t0\r\n2\t1\t0\t0\r\n3\t0\t0\t1";
            bool[,]? matrix =
            {
                {false, true, false},
                {true, false, false},
                {false, false, true},
            };
            var graph = new UndirectedGraph<int>(matrix, 1, 2, 3);

            //act
            var actual = graph.ToString();

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ToStringTest_NullGraph()
        {
            //arrange
            var expected = "Empty graph";
            var graph = new UndirectedGraph<int>();

            //act
            var actual = graph.ToString();

            //assert
            Assert.AreEqual(expected, actual);
        }


        [TestMethod()]
        public void EqualsTest()
        {
            //arrange
            bool[,]? matrix =
            {
                { false, true, false},
                { true, false, false},
                { false, false, true},
            };
            var graph1 = new UndirectedGraph<int>(matrix, 1, 2, 3);
            var graph2 = new UndirectedGraph<int>(matrix, 1, 2, 3);

            //act
            bool condition = graph1.Equals(graph2);

            //assert
            Assert.IsTrue(condition);
        }

        [TestMethod()]
        public void IsEqualTest()
        {
            //arrange
            bool[,]? matrix =
            {
                { false, true, false},
                { true, false, false},
                { false, false, true},
            };
            var graph1 = new UndirectedGraph<int>(matrix, 1, 2, 3);
            var graph2 = new UndirectedGraph<int>(matrix, 1, 2, 3);

            //act
            bool flag = graph1 == graph2;

            //assert
            Assert.IsTrue(flag);
        }

        [TestMethod()]
        public void IsNotEqualTest()
        {
            //arrange
            bool[,]? matrix =
            {
                { false, true, false},
                { true, false, false},
                { false, false, true},
            };
            var graph1 = new UndirectedGraph<int>(matrix, 1, 2, 4);
            var graph2 = new UndirectedGraph<int>(matrix, 1, 2, 3);

            //act
            bool flag = graph1 != graph2;

            //assert
            Assert.IsTrue(flag);
        }

        [TestMethod()]
        public void IsMoreThanTest()
        {
            //arrange
            bool[,]? matrix =
            {
                { false, true, false},
                { true, false, false},
                { false, false, true},
            };
            var graph1 = new UndirectedGraph<int>(matrix, 1, 2, 3, 4);
            var graph2 = new UndirectedGraph<int>(matrix, 1, 2, 3);

            //act
            bool flag = graph1 > graph2;

            //assert
            Assert.IsTrue(flag);
        }

        [TestMethod()]
        public void IsLessThanTest()
        {
            //arrange
            bool[,]? matrix =
            {
                { false, true, false},
                { true, false, false},
                { false, false, true},
            };
            var graph1 = new UndirectedGraph<int>(matrix, 1, 2);
            var graph2 = new UndirectedGraph<int>(matrix, 1, 2, 3);

            //act
            bool flag = graph1 < graph2;

            //assert
            Assert.IsTrue(flag);
        }

        [TestMethod()]
        public void IsMoreThanOrEqualTest()
        {
            //arrange
            bool[,]? matrix =
            {
                { false, true, false},
                { true, false, false},
                { false, false, true},
            };
            var graph1 = new UndirectedGraph<int>(matrix, 1, 2, 3);
            var graph2 = new UndirectedGraph<int>(matrix, 1, 2, 3);

            //act
            bool flag = graph1 == graph2;

            //assert
            Assert.IsTrue(flag);
        }

        [TestMethod()]
        [DataRow(new int[] { 1, 2, 3 }, new int[] {1,2,3})]
        [DataRow(new int[] { 1, 2, 3 }, new int[] { 1,2,3,4})]
        public void IsLessThanOrEqualTest(int[] array1, int[] array2)
        {
            //arrange
            bool[,]? matrix =
            {
                { false, true, false},
                { true, false, false},
                { false, false, true},
            };
            var graph1 = new UndirectedGraph<int>(matrix, array1);
            var graph2 = new UndirectedGraph<int>(matrix, array2);

            //act
            bool flag = graph1 <= graph2;

            //assert
            Assert.IsTrue(flag);
        }
    }
}