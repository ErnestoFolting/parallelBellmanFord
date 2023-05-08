using parallelBellmanFord.Common;
using parallelBellmanFord.Solvers.Consecutive;
using parallelBellmanFord.Solvers.Parallel;

namespace parallelBellmanFord
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = "adjacencySimple6Verticles.txt";
            //List<List<int>> adjacencyMatrix = MatrixHelper.readMatrixFromFile(path);
            //List<List<int>> adjacencyMatrix = MatrixHelper.generateFullAdjacencyMatrix(1500);
            List<List<int>> adjacencyMatrix = MatrixHelper.generateMaxPathMatrix(1000);
            //MatrixHelper.printMatrix(adjacencyMatrix);

            //Console.WriteLine("Consecutive = 0, Parallel = 1:, Compare = 2\n");
            //int solverType = Convert.ToInt32(Console.ReadLine());

            int solverType = 2;

            if(solverType == 0)
            {
                ConsecutiveSolver consecutiveSolver = new(adjacencyMatrix, 0);
                long startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                consecutiveSolver.SolveConsecutive();
                long endTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                Console.WriteLine("Consecutive time: " + (endTime - startTime));
            }
            else if(solverType == 1)
            {
                ParallelSolver parallelSolver = new(adjacencyMatrix, 0);
                long startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                parallelSolver.SolveTasks();
                long endTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                Console.WriteLine("Parallel time: " + (endTime - startTime));
            }
            else if(solverType == 2) //check the answer correctness
            {
                ConsecutiveSolver consecutiveSolver = new(adjacencyMatrix, 0);
                (List<int> distancesConsecutive, List<int> comeFromConsecutive) = consecutiveSolver.SolveConsecutive();

                ParallelSolver parallelSolver = new(adjacencyMatrix, 0);
                (List<int> distancesParallel, List<int> comeFromParallel) = parallelSolver.SolveParallelFor();

                ResultsComparer.compareDistances(distancesConsecutive, distancesParallel);
                ResultsComparer.comparePaths(comeFromConsecutive, comeFromParallel);
                Console.WriteLine("Check");
            }
        }
    }
}