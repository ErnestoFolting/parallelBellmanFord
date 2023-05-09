
namespace parallelBellmanFord.Common
{
    public static class MatrixHelper
    {
        public static List<List<int>> readMatrixFromFile(string Path)
        {
            List<List<int>> matrix = new List<List<int>>();
            try
            {
                using (StreamReader reader = new StreamReader(Path))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        List<int> row = new List<int>();

                        string[] tokens = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                        foreach (string token in tokens)
                        {
                            int value;
                            if (int.TryParse(token, out value))
                            {
                                row.Add(value);
                            }
                        }
                        matrix.Add(row);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading matrix from file: " + ex.Message);
            }

            return matrix;
        }

        public static void printMatrix(List<List<int>> matr)
        {
            for(int i =0;i< matr.Count;i++)
            {
                for(int j = 0; j < matr[i].Count;j++)
                {
                    Console.Write("{0,-5}",matr[i][j]);
                }
                Console.WriteLine();
            }
        }

        public static List<List<int>> generateFullAdjacencyMatrix(int vertexCount)
        {
            List<List<int>> matrix = new();
            Random rand = new Random();

            for (int i = 0; i < vertexCount; i++)
            {
                List<int> temp = new();
                for (int j = 0; j < vertexCount; j++)
                {
                    if (i != j)
                    {
                        int weight = rand.Next(1, 10);
                        temp.Add(weight);
                    }
                    else
                    {
                        temp.Add(0);
                    }
                }
                matrix.Add(temp);
            }
            return matrix;
        }

        public static List<List<int>> generateAdjacencyMatrix(int vertexCount, int avgEdges)
        {
            if(avgEdges < 2 || avgEdges >= vertexCount-2)
            {
                Console.WriteLine("Select more avgEdges.");
                System.Environment.Exit(0);
            }  
            List<List<int>> matrix = makeSpanning(vertexCount);

            Random rand = new Random();

            for (int i = 0; i < vertexCount; i++)
            {
                int currentEdgesToAdd = rand.Next(avgEdges - 2, avgEdges + 3);
                for (int j = 0; j < currentEdgesToAdd; j++)
                {
                    int toVertex = rand.Next(0, vertexCount);
                    int ifNegative = rand.Next(0, 3);
                    int weight = 0;
                    if (ifNegative == 0)
                    {
                        weight = rand.Next(-10, -1);
                    }
                    else
                    {
                        weight = rand.Next(100, 200);
                    }
                    matrix[i][toVertex] = weight;
                }
            }

            return matrix;
        }

        private static List<List<int>> makeSpanning(int vertexCount)
        {
            List<List<int>> matrix = new();
            Random rand = new Random();

            for(int i = 0; i < vertexCount; i++)
            {
                List<int> temp = new();
                for(int j = 0; j < vertexCount; j++)
                {
                    temp.Add(0);
                }
                matrix.Add(temp);
            }

            for (int i = 0; i < matrix.Count - 1; i++)
            {
                matrix[i][i+1] = rand.Next(100, 200);
            }

            return matrix;
        }

        public static List<List<int>> generateMaxIterationMatrix(int vertexCount)
        {
            List<List<int>> matrix = new();
            Random rand = new Random();
            for (int i = 0; i < vertexCount; i++)
            {
                List<int> temp = new();
                for (int j = 0; j < vertexCount; j++)
                {
                    temp.Add(0);
                }
                matrix.Add(temp);
            }

            matrix[vertexCount - 1][0] = vertexCount + 1;

            for (int i = 1; i < vertexCount; i++)
            {
                matrix[i][i - 1] = 1;
            }
            return matrix;
        }
    }
}
