using parallelBellmanFord.Common;
using parallelBellmanFord.Solvers.Consecutive;

namespace parallelBellmanFord
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = "adjacency.txt";
            List<List<int>> adjacencyMatrix = MatrixHelper.readMatrixFromFile(path);
            MatrixHelper.printMatrix(adjacencyMatrix);
            Console.WriteLine();

            ConsecutiveSolver consSolver = new(adjacencyMatrix, 0);

            consSolver.Solve();
        }
    }
}