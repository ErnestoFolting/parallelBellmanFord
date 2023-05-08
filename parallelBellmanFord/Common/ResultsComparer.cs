

namespace parallelBellmanFord.Common
{
    public static class ResultsComparer
    {
        public static void compareDistances(List<int> consecutiveDistances, List<int> parallelDistances)
        {
            if(consecutiveDistances.SequenceEqual(parallelDistances))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Nice. Found distances are equal");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Unfortunately,found distances are not equal");
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void comparePaths(List<int> consecutivePaths, List<int> parallelPaths)
        {
            if (consecutivePaths.SequenceEqual(parallelPaths))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Nice. Found paths are equal");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Found paths are not equal");
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

    }
}
