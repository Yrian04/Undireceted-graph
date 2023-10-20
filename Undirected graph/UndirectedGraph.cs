using System.Collections;

namespace Undirected_graph
{
    public class UndirectedGraph<T> : ICollection<T>, ICollection, ICloneable
    {
        public List<T> Vertexes { get; }
        public bool[,]? IncideceMatrix { get; private set; }

        public int Count => Vertexes.Count;

        public bool IsReadOnly {get; private set;} = false;

        public bool IsEmpty => Vertexes.Count == 0;

        public bool IsSynchronized => true;

        public object SyncRoot => throw new NotImplementedException();

        public UndirectedGraph() : this(new List<T>()) { }
        public UndirectedGraph(params T[] vertexes) : this(new List<T>(vertexes)) { }
        public UndirectedGraph(bool[,]? matrix, params T[] vertexes) : this(matrix, new List<T>(vertexes)) { }
        public UndirectedGraph(List<T> vertexes) : this(vertexes.Count != 0 ? new bool[vertexes.Count, vertexes.Count] : null, vertexes) { }
        public UndirectedGraph(bool[,]? matrix, List<T> vertexes)
        {
            Vertexes = vertexes;
            IncideceMatrix = matrix;
        }

        public T this[int index]
        {
            get 
            {
                if (index < 0 || index >= Vertexes.Count)
                    throw new ArgumentOutOfRangeException("index");
                return Vertexes[index]; 
            }

            set
            {
                if (index < 0 || index >= Vertexes.Count)
                    throw new ArgumentOutOfRangeException("index");

                if (value == null)
                    throw new ArgumentNullException("value");

                Vertexes[index] = value;
            }
        }

        public bool this[int index1, int index2]
        {
            get
            {
                if (Vertexes.Count == 0 || IncideceMatrix is null)
                    throw new Exception("Graph is empty");

                if (index1 < 0 || index1 > Vertexes.Count)
                    throw new ArgumentOutOfRangeException("index1");

                if (index2 < 0 || index2 > Vertexes.Count)
                    throw new ArgumentOutOfRangeException("index2");

                return IncideceMatrix[index1, index2];
            }

            set
            {
                if (Vertexes.Count == 0 || IncideceMatrix is null)
                    throw new Exception("Graph is empty");

                if (index1 < 0 || index1 > Vertexes.Count)
                    throw new ArgumentOutOfRangeException("index1");

                if (index2 < 0 || index2 > Vertexes.Count)
                    throw new ArgumentOutOfRangeException("index2");

                IncideceMatrix[index1, index2] = value;
            }
        }

        public bool this[T obj1, T obj2]
        {
            get
            {
                if (Vertexes.Count == 0 || IncideceMatrix is null)
                    throw new Exception("Graph is empty");

                if (!Vertexes.Contains(obj1))
                    throw new Exception($"Graph not contains object {obj1}");

                if (!Vertexes.Contains(obj2))
                    throw new Exception($"Graph not contains object {obj2}");

                int index1 = Vertexes.IndexOf(obj1);
                int index2 = Vertexes.IndexOf(obj2);

                return this[index1, index2];
            }

            set
            {
                if (Vertexes.Count == 0 || IncideceMatrix is null)
                    throw new Exception("Graph is empty");

                if (!Vertexes.Contains(obj1))
                    throw new Exception($"Graph not contains object {obj1}");

                if (!Vertexes.Contains(obj2))
                    throw new Exception($"Graph not contains object {obj2}");

                int index1 = Vertexes.IndexOf(obj1);
                int index2 = Vertexes.IndexOf(obj2);

                this[index1, index2] = value;
            }
        }

        public void Add(T item)
        {
            Vertexes.Add(item);
            bool[,] newMatrix = new bool[Vertexes.Count, Vertexes.Count];

            if (IncideceMatrix is not null)
            {
                for (int i = 0; i < IncideceMatrix.GetLength(0); i++)
                    for (int j = 0; j < IncideceMatrix.GetLength(1); j++)
                        newMatrix[i, j] = IncideceMatrix[i, j];
            }
            else
            {
                IncideceMatrix = new bool[Vertexes.Count, Vertexes.Count];
            }
            IncideceMatrix = newMatrix;
        }

        public void Clear()
        {
            Vertexes.Clear();
            IncideceMatrix = null;
        }

        public bool Contains(T item) => Vertexes.Contains(item);

        public void CopyTo(T[] array, int arrayIndex) => Vertexes.CopyTo(array, arrayIndex);

        public void CopyTo(Array array, int index) => Vertexes.CopyTo((T[])array, index);

        public bool Remove(T item)
        {
            int index = Vertexes.IndexOf(item);
            bool success = Vertexes.Remove(item);

            if (success)
            {
                bool[,] newMatrix = new bool[Vertexes.Count, Vertexes.Count];

                for (int i = 0; i < IncideceMatrix!.GetLength(0); i++)
                    for (int j = 0; j < IncideceMatrix!.GetLength(1); j++)
                        if (i != index || j != index)
                            newMatrix[i > index ? i - 1 : i, j > index ? j - 1 : j] = IncideceMatrix![i, j];

                IncideceMatrix = newMatrix;
            }

            return success;
        }

        public IEnumerator<T> GetEnumerator() => Vertexes.GetEnumerator();

        public IEnumerable<(T, T)> GetPair()
        {
            foreach (var subj in Vertexes)
                foreach (var obj in Vertexes)
                    if (this[subj, obj])
                        yield return (subj, obj);
        }

        IEnumerator IEnumerable.GetEnumerator() => Vertexes.GetEnumerator();

        public object Clone()
        {
            List<T> vertexes = new List<T>((T[])Vertexes.ToArray().Clone());
            bool[,]? incidentMatrix = (bool[,]?)IncideceMatrix?.Clone();
            return new UndirectedGraph<T>(incidentMatrix, vertexes);
        }

        public static bool operator ==(UndirectedGraph<T> graph1, UndirectedGraph<T> graph2) => graph1.Equals(graph2);
        public static bool operator !=(UndirectedGraph<T> graph1, UndirectedGraph<T> graph2) => !graph1.Equals(graph2);
        public static bool operator >(UndirectedGraph<T> graph1, UndirectedGraph<T> graph2) => graph1.Count > graph2.Count;
        public static bool operator <(UndirectedGraph<T> graph1, UndirectedGraph<T> graph2) => graph1.Count < graph2.Count;
        public static bool operator >=(UndirectedGraph<T> graph1, UndirectedGraph<T> graph2) => !(graph1 < graph2);
        public static bool operator <=(UndirectedGraph<T> graph1, UndirectedGraph<T> graph2) => !(graph1 > graph2);

        public override string ToString()
        {
            if (Vertexes.Count == 0 || IncideceMatrix is null)
                return "Empty graph";

            string output = "V";
            foreach (var vertex in Vertexes)
                output += "\t" + vertex ;

            for (var i = 0; i < IncideceMatrix.GetLength(0); i++)
            {
                output += "\r\n" + Vertexes[i];

                for (var j = 0; j < IncideceMatrix.GetLength(1); j++)
                    output += "\t" + (IncideceMatrix[i, j] ? '1' : '0');
            }

            return output;
        }

        public override bool Equals(object? obj)
        {
            if (obj is UndirectedGraph<T> graph)
                return Vertexes.SequenceEqual(graph.Vertexes) && IncideceMatrix == graph.IncideceMatrix;
            
            return false;
        }

        public override int GetHashCode() => base.GetHashCode();
    }
}