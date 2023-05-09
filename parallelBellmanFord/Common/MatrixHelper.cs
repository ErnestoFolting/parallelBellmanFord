
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
                        int weight = rand.Next(1, 11);
                        if (weight == 0) weight++;
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
