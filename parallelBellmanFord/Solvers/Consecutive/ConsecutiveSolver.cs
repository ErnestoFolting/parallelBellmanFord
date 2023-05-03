using parallelBellmanFord.Interfaces;

namespace parallelBellmanFord.Solvers.Consecutive
{
    public class ConsecutiveSolver : IBellmanFordSolver
    {
        private List<List<int>> _adjacencyMatrix;
        private List<int> _distancesToVerticles;
        private List<int> _comeFromIndex;

        private int _startVerticleIndex;
        private int _verticlesCount;
        public ConsecutiveSolver(List<List<int>> adjacencMatrix, int verticleToStartFrom)
        {
           _adjacencyMatrix = adjacencMatrix;
            _verticlesCount = adjacencMatrix.Count;
           _distancesToVerticles = new List<int>(Enumerable.Repeat(int.MaxValue,_verticlesCount));
           _comeFromIndex = new List<int>(Enumerable.Repeat(-1,_verticlesCount));
            _startVerticleIndex = verticleToStartFrom;
        }

        public void Solve()
        {
            _distancesToVerticles[_startVerticleIndex] = 0;
            for(int timesCount = 0; timesCount < _verticlesCount-1; timesCount++)
            {
                for(int i=0;i<_verticlesCount;i++)
                {
                    for(int j=0;j<_verticlesCount;j++)
                    {
                        Update(i, j);
                    }
                }
            }
            _distancesToVerticles.ForEach(el => Console.WriteLine(el));

        }
        private void Update(int fromVerticle,int toVerticle)
        {
            if (_adjacencyMatrix[fromVerticle][toVerticle]!= 0 && fromVerticle != toVerticle && _distancesToVerticles[fromVerticle] != int.MaxValue)
            {
                int newFromVerticalDistance = _distancesToVerticles[fromVerticle] + _adjacencyMatrix[fromVerticle][toVerticle];
                if (newFromVerticalDistance < _distancesToVerticles[toVerticle])
                {
                    _distancesToVerticles[toVerticle] = newFromVerticalDistance;
                    _comeFromIndex[toVerticle] = fromVerticle;
                }
            }
        }

        private void CheckForNegativeCycle()
        {

        }
    }
}
