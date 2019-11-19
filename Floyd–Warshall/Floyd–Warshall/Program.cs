using System;

namespace Floyd_Warshall
{
    class Program
    {
        static void Main()
        {
            // матрица смежности
            int[,] matrix = new int[,] {{0, 8, 5}, {3, 0, int.MaxValue}, {int.MaxValue, 2, 0} };

            int quantity_of_vertices = matrix.GetLength(1); // количество вершин

            Algorithm(ref matrix, quantity_of_vertices);
            Console_Output(matrix, quantity_of_vertices);
        }

        private static void Console_Output(int[,] matrix, int quantity_of_vertices)
        {
            for(int i = 0; i < quantity_of_vertices; i++)
            {
                for(int j = 0; j < quantity_of_vertices; j++)
                {
                    Console.Write($"{matrix[i, j]}, ");
                }
                Console.Write("\n");
            }
        }
        
        private static void Algorithm(ref int[,] matrix, int quantity_of_vertices)
        {
            for(int k = 0; k < quantity_of_vertices; k++)
            {
                for(int i = 0; i < quantity_of_vertices; i++)
                {
                    for(int j = 0; j < quantity_of_vertices; j++)
                    {
                        if(matrix[i, j] > Math.Abs(matrix[i,k] + matrix[k, j]))
                        {
                            matrix[i, j] = matrix[i, k] + matrix[k, j];
                        }
                        
                    }
                }
            }
        }
    }
}
