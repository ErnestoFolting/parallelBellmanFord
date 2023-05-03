using parallelBellmanFord.Common;
using parallelBellmanFord.Solvers.Consecutive;

namespace parallelBellmanFord
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = "adjacencySimple6Verticles.txt";
            //List<List<int>> adjacencyMatrix = MatrixHelper.readMatrixFromFile(path);
            List<List<int>> adjacencyMatrix = MatrixHelper.generateFullAdjacencyMatrix(500);
            //MatrixHelper.printMatrix(adjacencyMatrix);
            Console.WriteLine();

            ConsecutiveSolver consSolver = new(adjacencyMatrix, 0);
            long startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            consSolver.Solve();
            long endTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            Console.WriteLine("Time: " + (endTime - startTime));
        }
    }
}