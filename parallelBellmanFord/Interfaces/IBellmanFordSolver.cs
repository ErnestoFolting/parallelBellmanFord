namespace parallelBellmanFord.Interfaces
{
    public interface IBellmanFordSolver
    {
        (List<int> distances, List<int> comeFrom) Solve();
    }
}
