using System;
using System.Collections.Generic;

namespace Dijkstra
{
    class Program
    {
        static int Main()
        {
            // матрица смежности
            int[,] matrix = new int[,] {{int.MaxValue, 7, 9, int.MaxValue, int.MaxValue, 14},
            {7, int.MaxValue, 10, 15, int.MaxValue, int.MaxValue},
            {9, 10, int.MaxValue, 11, int.MaxValue, 2},
            {int.MaxValue, 15, 11, int.MaxValue, 6, int.MaxValue},
            {int.MaxValue, int.MaxValue, int.MaxValue, 6, int.MaxValue, 9},
            {14, int.MaxValue, 2, int.MaxValue, 9, int.MaxValue}};

            int quantity_of_vertices = matrix.GetLength(1); // количество вершин
            int start_vertex = 1; // вершина отсчета

            int[] num_tags_of_vertices = new int[quantity_of_vertices]; // нумерованные метки вершин

            bool[] tags_of_vertices = new bool[quantity_of_vertices]; // метки вершин (true - вкл/false - выкл)

            int[] previous_vertices = new int[quantity_of_vertices]; // массив предыдущих вершин

            List<List<int>> path_on_vertices = new List<List<int>>(); // лист кратчайших путей по вершинам

            if(Algorithm(matrix, start_vertex, quantity_of_vertices, ref tags_of_vertices, ref num_tags_of_vertices, ref previous_vertices) == -1)
            {
                Console.WriteLine("Ошибка в работе функции Min_Tag");
                return -1;
            }
            List_Filling(start_vertex, quantity_of_vertices, previous_vertices, ref path_on_vertices);
            Console_Output(quantity_of_vertices, path_on_vertices, num_tags_of_vertices, start_vertex);

            return 0;
        }

        // вывод в консоль
        private static void Console_Output(int quantity_of_vertices, List<List<int>> path_on_vertices, int[] num_tags_of_vertices, int start_vertex)
        {
            Console.WriteLine($"Вывод кратчайших путей от вершины {start_vertex} до всех остальных:");
            for(int i = 0; i < quantity_of_vertices; i++)
            {
                if(i + 1 != start_vertex)
                {
                    Console.WriteLine($"Кратчайший путь от вершины {start_vertex} до вершины {i + 1} = {num_tags_of_vertices[i]}");
                }
            }
            Console.WriteLine();
            Console.WriteLine("Вывод кратчайших путей по вершинам:");
            for (int i = 0; i < quantity_of_vertices; i++)
            {
                if(i + 1 != start_vertex)
                {
                    for (int j = path_on_vertices[i].Count - 1; j >= 0; j--)
                    {
                        if (j > 0)
                        {
                            Console.Write(path_on_vertices[i][j] +
                            $" ({num_tags_of_vertices[path_on_vertices[i][j - 1] - 1] - num_tags_of_vertices[path_on_vertices[i][j] - 1]})-> ");
                        }
                        else
                        {
                            Console.Write(path_on_vertices[i][j]);
                        }

                    }
                    Console.WriteLine();
                }
            }
        }

        // запись кратчайших путей по вершинам в двумерный лист
        private static void List_Filling(int start_vertex, int quantity_of_vertices, int[] previous_vertices, ref List<List<int>> path_on_vertices)
        {
            for(int i = 0; i < quantity_of_vertices; i++)
            {
                path_on_vertices.Add(new List<int>());
                path_on_vertices[i].Add(i + 1);
                int j = previous_vertices[i];
                while(j != start_vertex)
                {
                    path_on_vertices[i].Add(j);
                    j = previous_vertices[j - 1];
                }

                if(j == start_vertex)
                {
                    path_on_vertices[i].Add(j);
                }
            }
        }

        private static int Algorithm(int[,] matrix, int start_vertex, int quantity_of_vertices,
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
                if(reference_vertex == -1)
                {
                    return -1;
                }
                Vertex_Processing(quantity_of_vertices, matrix, reference_vertex, ref num_tags_of_vertices,
                ref tags_of_vertices, ref previous_vertices);
            }
            return 0;
        }

        // найти вершину с минимальной меткой в массиве меток вершин
        private static int Min_Tag(int[] num_tags_of_vertices, int quantity_of_vertices, bool[] tags_of_vertices)
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
        private static void Vertex_Processing (int quantity_of_vertices, int[,] matrix, int reference_vertex, 
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
        private static bool Start_Check(ref bool[] tags_of_vertices, int quantity_of_vertices)
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
        private static bool End_Check(ref bool[] tags_of_vertices, int quantity_of_vertices)
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
