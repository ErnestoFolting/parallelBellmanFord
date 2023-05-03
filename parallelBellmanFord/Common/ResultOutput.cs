namespace parallelBellmanFord.Common
{
    public static class ResultOutput
    {
        public static void printDistances(List<int> distances, int startVertexIndex)
        {
            Console.WriteLine("-----------Solved.-----------");
            Console.WriteLine("Evaluated distances:");
            for(int i =0;i< distances.Count; i++){
                if (distances[i] != int.MaxValue)
                {
                    Console.WriteLine(startVertexIndex + " ---> " + i + " = " + distances[i]);
                }
                else
                {
                    Console.WriteLine(startVertexIndex + " ---> " + i + " = infinity" );
                }
            }
        }

        public static void printPaths(List<int> comeFromIndex, int startVertexIndex)
        {
            Console.WriteLine("\nPaths:");
            for(int i =0; i < comeFromIndex.Count; i++)
            {
                List<int> reversePath = new();
                if(i != startVertexIndex)
                {
                    int vertexInPath = comeFromIndex[i];
                    reversePath.Add(i);
                    while (vertexInPath != startVertexIndex)
                    {
                        reversePath.Add(vertexInPath);
                        vertexInPath = comeFromIndex[vertexInPath];
                    }
                    reversePath.Add(startVertexIndex);
                }
                else
                {
                    reversePath.Add(i);
                }
                pathOuput(reversePath);
            }
        }
        private static void pathOuput(List<int> reversePath)
        {
            reversePath.Reverse();
            for(int i =0;i< reversePath.Count; i++)
            {
                Console.Write(reversePath[i]);
                if (i != reversePath.Count - 1)
                {
                    Console.Write(" -> ");
                }
            }
            Console.WriteLine();
        }
    }
}