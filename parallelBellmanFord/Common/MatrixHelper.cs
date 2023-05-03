
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
                    Console.Write(matr[i][j] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
