using System;
using System.Collections.Generic;

namespace Dijkstra
{
    class Program
    {
        static void Main()
        {
            int quantity_of_vertices = 6; // количество вершин
            int start_vertex = 1; // вершина отсчета

            // матрица смежности
            int[,] matrix = new int[,] {{int.MaxValue, 7, 9, int.MaxValue, int.MaxValue, 14},
            {7, int.MaxValue, 10, 15, int.MaxValue, int.MaxValue},
            {9, 10, int.MaxValue, 11, int.MaxValue, 2},
            {int.MaxValue, 15, 11, int.MaxValue, 6, int.MaxValue},
            {int.MaxValue, int.MaxValue, int.MaxValue, 6, int.MaxValue, 9},
            {14, int.MaxValue, 2, int.MaxValue, 9, int.MaxValue}};

            int[] num_tags_of_vertices = new int[quantity_of_vertices]; // нумерованные метки вершин

            bool[] tags_of_vertices = new bool[quantity_of_vertices]; // метки вершин (true - вкл/false - выкл)

            int[] previous_vertices = new int[quantity_of_vertices]; // массив предыдущих вершин

            List<List<int>> path_on_vertices = new List<List<int>>();

            Algorithm(matrix, start_vertex, quantity_of_vertices, ref tags_of_vertices, ref num_tags_of_vertices, ref previous_vertices);

            Console_Output(start_vertex, quantity_of_vertices, num_tags_of_vertices, previous_vertices);
        }

        // вывод в консоль
        public static void Console_Output(int start_vertex, int quantity_of_vertices, int[] num_tags_of_vertices, int[] previous_vertices)
        {
            Console.WriteLine($"Массив кратчайших путей от вершины {start_vertex} до всех остальных:");
            Console.Write("(");
            for (int i = 0; i < quantity_of_vertices; i++)
            {
                Console.Write(num_tags_of_vertices[i]);
                Console.Write(" ");
            }
            Console.Write(")\n");
            Console.WriteLine("Вывод кратчайших путей по вершинам:");
            for (int i = 0; i < quantity_of_vertices; i++)
            {
                Console.Write(i + 1);
                Console.Write(" <- ");
                int j = previous_vertices[i];
                while (j != start_vertex)
                {
                    Console.Write(j);
                    Console.Write(" <- ");
                    j = previous_vertices[j - 1];
                }

                if (j == start_vertex)
                {
                    Console.Write(j);
                    Console.WriteLine();
                }
            }
        }

        public static void Algorithm(int[,] matrix, int start_vertex, int quantity_of_vertices,
        ref bool[] tags_of_vertices, ref int[] num_tags_of_vertices, ref int[] previous_vertices)
        {
            int reference_vertex; // индекс контрольной вершины

            while (End_Check(ref tags_of_vertices, quantity_of_vertices) != true)
            {
                if (Start_Check(ref tags_of_vertices, quantity_of_vertices) == true)
                {
                    for (int i = 0; i < quantity_of_vertices; i++)
                    {
                        if (i != start_vertex - 1)
                        {
                            num_tags_of_vertices[i] = int.MaxValue;
                        } else { num_tags_of_vertices[i] = 0; }

                        previous_vertices[i] = 1;
                    }
                }

                reference_vertex = Min_Tag(num_tags_of_vertices, quantity_of_vertices, tags_of_vertices);
                Vertex_Processing(quantity_of_vertices, matrix, reference_vertex, ref num_tags_of_vertices,
                ref tags_of_vertices, ref previous_vertices);
            }
        }

        // найти вершину с минимальной меткой в массиве меток вершин
        public static int Min_Tag(int[] num_tags_of_vertices, int quantity_of_vertices, bool[] tags_of_vertices)
        {
            int temp = int.MaxValue;
            int temp_i = -1;
            for (int i = 0; i < quantity_of_vertices ; i++)
            {
               if(num_tags_of_vertices[i] < temp && tags_of_vertices[i] != true)
                {
                    temp = num_tags_of_vertices[i];
                    temp_i = i;
                }
            }
            return temp_i;
        }

        // найти смежные вершины с "контрольной" и пометить их
        public static void Vertex_Processing (int quantity_of_vertices, int[,] matrix, int reference_vertex, 
        ref int[] num_tags_of_vertices, ref bool[] tags_of_vertices, ref int[] previous_vertices)
        {
            for (int i = 0; i < quantity_of_vertices; i++)
            {
                if (matrix[reference_vertex, i] < int.MaxValue)
                {
                    if (num_tags_of_vertices[reference_vertex] + matrix[reference_vertex, i] < num_tags_of_vertices[i])
                    {
                        num_tags_of_vertices[i] = num_tags_of_vertices[reference_vertex] + matrix[reference_vertex, i];
                        previous_vertices[i] = reference_vertex + 1;
                    }
                }

            }
            tags_of_vertices[reference_vertex] = true;
        }

        // проверить на начало алгоритма
        public static bool Start_Check(ref bool[] tags_of_vertices, int quantity_of_vertices)
        {
            for (int i = 0; i < quantity_of_vertices; i++)
            {
                if(tags_of_vertices[i] == true)
                {
                    return false;
                }
            }
            return true;
        }

        // проверить на конец алгоритма
        public static bool End_Check(ref bool[] tags_of_vertices, int quantity_of_vertices)
        {
            for (int i = 0; i < quantity_of_vertices; i++)
            {
                if (tags_of_vertices[i] == false)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
