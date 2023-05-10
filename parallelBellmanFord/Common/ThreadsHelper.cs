namespace parallelBellmanFord.Common
{
    public static class ThreadsHelper
    {
        public static void SetThreadsNumber(int number)
        {
            ThreadPool.GetMinThreads(out _, out var IOMin);
            ThreadPool.SetMinThreads(number, IOMin);

            ThreadPool.GetMaxThreads(out _, out var IOMax);
            ThreadPool.SetMaxThreads(number, IOMax);
        }
    }
}
