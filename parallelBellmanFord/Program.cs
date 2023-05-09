using parallelBellmanFord.Common;
using parallelBellmanFord.Solvers.Consecutive;
using parallelBellmanFord.Solvers.Parallel;

namespace parallelBellmanFord
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = "adjacencySimple6Verticles2.txt";

            //List<List<int>> adjacencyMatrix = MatrixHelper.readMatrixFromFile(path);
            List<List<int>> adjacencyMatrix = MatrixHelper.generateAdjacencyMatrix(500, 2);
            //List<List<int>> adjacencyMatrix = MatrixHelper.generateMaxIterationMatrix(1000);
            //MatrixHelper.printMatrix(adjacencyMatrix);

            //Console.WriteLine("Consecutive = 0, Parallel = 1:, Compare = 2\n");
            //int solverType = Convert.ToInt32(Console.ReadLine());

            int solverType = 2;
            const int threadsNumber = 2;
            int startTop = 0;

            ThreadPool.GetMinThreads(out _, out var IOMin);
            ThreadPool.SetMinThreads(threadsNumber, IOMin);

            ThreadPool.GetMaxThreads(out _, out var IOMax);
            ThreadPool.SetMaxThreads(threadsNumber, IOMax);

            if (solverType == 0)
            {
                ConsecutiveSolver consecutiveSolver = new(adjacencyMatrix, startTop);
                long startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                consecutiveSolver.SolveConsecutive();
                long endTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                Console.WriteLine("Consecutive time: " + (endTime - startTime));
            }
            else if (solverType == 1)
            {
                ParallelSolver parallelSolver = new(adjacencyMatrix, startTop);
                long startTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                parallelSolver.SolveTasks();
                long endTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                Console.WriteLine("Parallel time: " + (endTime - startTime));
            }
            else if (solverType == 2) //check the answer correctness
            {
                long consecutiveStartTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                ConsecutiveSolver consecutiveSolver = new(adjacencyMatrix, startTop);
                (List<int> distancesConsecutive, List<int> comeFromConsecutive) = consecutiveSolver.SolveConsecutive();
                long consecutiveEndTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();

                long parallelStartTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                ParallelSolver parallelSolver = new(adjacencyMatrix, startTop);
                (List<int> distancesParallel, List<int> comeFromParallel) = parallelSolver.SolveTasks();
                long parallelEndTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();

                ResultsComparer.compareDistances(distancesConsecutive, distancesParallel);
                ResultsComparer.comparePaths(comeFromConsecutive, comeFromParallel);
                Console.WriteLine("Consecutive time: " + (consecutiveEndTime - consecutiveStartTime));
                Console.WriteLine("Parallel time: " + (parallelEndTime - parallelStartTime));
            }
        }
    }
}