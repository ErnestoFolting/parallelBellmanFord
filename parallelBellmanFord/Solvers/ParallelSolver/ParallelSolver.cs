using parallelBellmanFord.Common;
using parallelBellmanFord.Interfaces;

namespace parallelBellmanFord.Solvers.Parallel
{
    public class ParallelSolver : IBellmanFordSolver
    {
        private List<List<int>> _adjacencyMatrix;
        private List<int> _distancesToVerticles;
        private List<int> _comeFromIndex;

        private int _startVerticleIndex;
        private int _verticlesCount;
        public ParallelSolver(List<List<int>> adjacencMatrix, int verticleToStartFrom)
        {
            _adjacencyMatrix = adjacencMatrix;
            _verticlesCount = adjacencMatrix.Count;
            _distancesToVerticles = new List<int>(Enumerable.Repeat(int.MaxValue, _verticlesCount));
            _comeFromIndex = new List<int>(Enumerable.Repeat(-1, _verticlesCount));
            _startVerticleIndex = verticleToStartFrom;
            _distancesToVerticles[_startVerticleIndex] = 0;
        }

        public (List<int> distances, List<int> comeFrom) SolveTasks()
        {
            Task[] tasks = new Task[_verticlesCount - 1];

            for (int timesCount = 0; timesCount < _verticlesCount - 1; timesCount++)
            {
                Task currentTask = new Task(() => makeIteration());
                tasks[timesCount] = currentTask;
                tasks[timesCount].Start();
            }

            Task.WaitAll(tasks);

            CheckForNegativeCycle();

            ResultOutput.printDistances(_distancesToVerticles, _startVerticleIndex);
            ResultOutput.printPaths(_comeFromIndex, _startVerticleIndex);

            return (_distancesToVerticles, _comeFromIndex);
        }

        public (List<int> distances, List<int> comeFrom) SolveParallelFor()
        {
            System.Threading.Tasks.Parallel.For(0, _verticlesCount - 1, itereationIndex => makeIteration());

            CheckForNegativeCycle();

            ResultOutput.printDistances(_distancesToVerticles, _startVerticleIndex);
            ResultOutput.printPaths(_comeFromIndex, _startVerticleIndex);

            return (_distancesToVerticles, _comeFromIndex);
        }

        private bool makeIteration()
        {
            bool ifWasUpdated = false;
            for (int i = 0; i < _verticlesCount; i++)
            {
                for (int j = 0; j < _verticlesCount; j++)
                {
                    if (i != j && _adjacencyMatrix[i][j] != 0) //only edges that have weight and not loops
                    {
                        if (Update(i, j))
                        {
                            ifWasUpdated = true;
                        }
                    }
                }
            }
            return ifWasUpdated;
        }

        private bool Update(int fromVerticle, int toVerticle)
        {
            bool ifUpdated = false;
            if (_distancesToVerticles[fromVerticle] != int.MaxValue && toVerticle != _startVerticleIndex) //if the fromVerticle is examined and not to startVerticle cycle
            {
                int newFromVerticalDistance = _distancesToVerticles[fromVerticle] + _adjacencyMatrix[fromVerticle][toVerticle];
                if (newFromVerticalDistance < _distancesToVerticles[toVerticle])
                {
                    lock (_distancesToVerticles)
                    {
                        _distancesToVerticles[toVerticle] = newFromVerticalDistance;
                    }
                    lock (_comeFromIndex)
                    {
                        _comeFromIndex[toVerticle] = fromVerticle;
                    }
                    ifUpdated = true;
                }
            }
            return ifUpdated;
        }

        private void CheckForNegativeCycle()
        {
            if (makeIteration())
            {
                Console.WriteLine("The Graph has negative cycle. Can not solve.");
                System.Environment.Exit(0);
            }
        }
    }
}
