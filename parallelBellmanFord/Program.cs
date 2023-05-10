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

            List<List<int>> adjacencyMatrix = MatrixHelper.readMatrixFromFile(path);
            //List<List<int>> adjacencyMatrix = MatrixHelper.generateFullAdjacencyMatrix(500);
            //List<List<int>> adjacencyMatrix = MatrixHelper.generateAdjacencyMatrix(1000, 6);
            //List<List<int>> adjacencyMatrix = MatrixHelper.generateMaxIterationMatrix(1000);
            //MatrixHelper.printMatrix(adjacencyMatrix);

            //Console.WriteLine("Consecutive = 0, Parallel = 1:, Compare = 2\n");+
            //int solverType = Convert.ToInt32(Console.ReadLine());

            int solverType = 0;
            int startTop = 0;

            const int threadsNumber = 16;
            ThreadsHelper.SetThreadsNumber(threadsNumber);

            if (solverType == 0)
            {
                ConsecutiveSolver consecutiveSolver = new(adjacencyMatrix, startTop);
                long startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                consecutiveSolver.SolveConsecutiveWave();
                long endTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                Console.WriteLine("Consecutive time: " + (endTime - startTime));
            }
            else if (solverType == 1)
            {
                ParallelSolver parallelSolver = new(adjacencyMatrix, startTop, threadsNumber);
                long startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                parallelSolver.SolveParallelWave();
                long endTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                Console.WriteLine("Parallel time: " + (endTime - startTime));
            }
            else if (solverType == 2) //check the answer correctness
            {
                long consecutiveStartTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                ConsecutiveSolver consecutiveSolver = new(adjacencyMatrix, startTop);
                (List<int> distancesConsecutive, List<int> comeFromConsecutive) = consecutiveSolver.SolveConsecutiveWave();
                long consecutiveEndTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();

                long parallelStartTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                ParallelSolver parallelSolver = new(adjacencyMatrix, startTop, threadsNumber);
                (List<int> distancesParallel, List<int> comeFromParallel) = parallelSolver.SolveParallelWave();
                long parallelEndTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();

                ResultsComparer.compareDistances(distancesConsecutive, distancesParallel);
                ResultsComparer.comparePaths(comeFromConsecutive, comeFromParallel);
                Console.WriteLine("Consecutive time: " + (consecutiveEndTime - consecutiveStartTime));
                Console.WriteLine("Parallel time: " + (parallelEndTime - parallelStartTime));
            }
        }
    }
}