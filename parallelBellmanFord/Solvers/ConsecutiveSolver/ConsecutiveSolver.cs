﻿using parallelBellmanFord.Common;
using parallelBellmanFord.Interfaces;

namespace parallelBellmanFord.Solvers.Consecutive
{
    public class ConsecutiveSolver : IBellmanFordSolver
    {
        private List<List<int>> _adjacencyMatrix;
        private List<int> _distancesToVerticles;
        private List<int> _comeFromIndex;

        public static List<int> times = new(); 

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

        public (List<int> distances, List<int> comeFrom) Solve()
        {
            CheckIfCorrectStartVerticle();

            _distancesToVerticles[_startVerticleIndex] = 0;
            for (int timesCount = 0; timesCount < _verticlesCount - 1; timesCount++)
            {
                makeIterationWave();
            }

            CheckIfAnyPathNotExist();

            CheckForNegativeCycle();

            printResult();

            return (_distancesToVerticles, _comeFromIndex);
        }

        private bool makeIterationWave()
        {
            bool ifUpdated = false;

            bool[] visited = new bool[_verticlesCount];
            List<int> verticles = new() { _startVerticleIndex };
            expandVerticles(verticles);
            visited[_startVerticleIndex] = true;

            List<int> nearVerticles = findNearVerticles(verticles, ref visited);
            while (nearVerticles.Count !=0)
            {
                if (expandVerticles(nearVerticles))
                {
                    ifUpdated = true;
                }
                
                nearVerticles = findNearVerticles(nearVerticles, ref visited);
            }
            return ifUpdated;
        }

        private List<int> findNearVerticles(List<int> borderVerticles, ref bool[] visited)
        {
            List<int> near = new();
            foreach (int borderVerticle in borderVerticles)
            {
                for(int i = 0; i < _verticlesCount; i++)
                {
                    if (borderVerticle != i && _adjacencyMatrix[borderVerticle][i] != 0 && !visited[i])
                    {
                        near.Add(i);
                        visited[i] = true;
                    }
                }
            }
            return near;
        }

        private bool expandVerticles(List<int> verticlesToExpand)
        {
            bool ifUpdated = false;
            foreach (int verticleToExpand in verticlesToExpand)
            {
                for (int j = 0; j < _verticlesCount; j++)
                {
                    if (verticleToExpand != j && _adjacencyMatrix[verticleToExpand][j] != 0)
                    {
                        if(Update(verticleToExpand, j))
                        {
                            ifUpdated = true;
                        }
                        
                    }
                }
            }
            return ifUpdated;
        }

        private bool Update(int fromVerticle, int toVerticle)
        {
            bool ifUpdated = false;
            if (_distancesToVerticles[fromVerticle] != int.MaxValue && toVerticle != _startVerticleIndex)
            {
                int newFromVerticalDistance = _distancesToVerticles[fromVerticle] + _adjacencyMatrix[fromVerticle][toVerticle];

                if (newFromVerticalDistance < _distancesToVerticles[toVerticle])
                {
                    _distancesToVerticles[toVerticle] = newFromVerticalDistance;
                    _comeFromIndex[toVerticle] = fromVerticle;
                    ifUpdated = true;
                }
            }
            return ifUpdated;
        }

        private void CheckForNegativeCycle()
        {
            if (makeIterationWave())
            {
                Console.WriteLine("The Graph has negative cycle. Can not solve.");
                System.Environment.Exit(0);
            }
        }

        private void CheckIfAnyPathNotExist()
        {
            if (_distancesToVerticles.Any(el => el.Equals(int.MaxValue)))
            {
                Console.WriteLine("There is no path from start verticle to the others");
                System.Environment.Exit(0);
            }
        }

        private void CheckIfCorrectStartVerticle()
        {
            if (_startVerticleIndex >= _verticlesCount)
            {
                Console.WriteLine("The start vertex is not correct");
                System.Environment.Exit(0);
            }
        }

        private void printResult()
        {
            ResultOutput.printDistances(_distancesToVerticles, _startVerticleIndex);
            ResultOutput.printPaths(_comeFromIndex, _startVerticleIndex, _adjacencyMatrix);
        }
        
    }
}
