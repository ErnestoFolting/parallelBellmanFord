using parallelBellmanFord.Solvers.Consecutive;
using parallelBellmanFord.Solvers.Parallel;

namespace parallelBellmanFord.Common
{
    public static class TimeMeasureHelper
    {
        public static void measureConsecutive(List<List<int>> matr, int times)
        {
            Console.WriteLine("Consecutive algo:");
            ConsecutiveSolver solver = new(matr, times);
            List<long> executionTimes = new();
            for (int i = 0; i < times; i++)
            {
                long parallelStartTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                (List<int> distancesParallel, List<int> comeFromParallel) = solver.SolveConsecutiveWave();
                long parallelEndTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                executionTimes.Add(parallelEndTime - parallelStartTime);
            }
            executionTimes.ForEach(el => Console.WriteLine("Time: " + el));
            Console.WriteLine("Avg: " + executionTimes.Average());
        }

        public static void measureParallel(List<List<int>> matr, int times)
        {
            Console.WriteLine("Parallel algo:");
            ParallelSolver solver = new(matr, times);
            List<long> executionTimes = new();
            for (int i = 0; i < times; i++)
            {
                long parallelStartTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                (List<int> distancesParallel, List<int> comeFromParallel) = solver.SolveParallelWave();
                long parallelEndTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                executionTimes.Add(parallelEndTime - parallelStartTime);
            }
            executionTimes.ForEach(el => Console.WriteLine( "Time: " + el));
            Console.WriteLine("Avg: " + executionTimes.Average());
        }
    }
}
