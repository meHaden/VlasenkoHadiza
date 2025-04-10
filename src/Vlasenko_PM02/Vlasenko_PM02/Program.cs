using System;
using System.Globalization;

// ТЕКСТ ЗАДАНИЯ: . В приложении пользователь должен ввести данные карты, расход топлива в литрах на 100 км пути,
// а далее иметь возможность указывать две любые из возможных точек по их номерам.
// Результат - расход топлива в литрах.
class Program
{
    private static int n;

    static void Main()
    {
        Console.WriteLine("==============================================");
        Console.WriteLine(" Ввод осуществляется по данным предоставленной ");
        Console.WriteLine("      карты города Кольчугино                ");
        Console.WriteLine("==============================================\n");

        Console.WriteLine("Введите количество точек на карте:");
        n = ReadPositiveInt();

        double[,] distances = new double[n, n];

        // Инициализация матрицы расстояний
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                distances[i, j] = double.PositiveInfinity;
            }
            distances[i, i] = 0;
        }

        Console.WriteLine("\nВведите расстояния между точками:");
        for (int userI = 1; userI <= n; userI++)
        {
            for (int userJ = userI + 1; userJ <= n; userJ++)
            {
                Console.WriteLine($"Расстояние между точкой {userI} и точкой {userJ} (км, 0 если нет пути):");
                double distance = ReadNonNegativeDouble();

                if (distance > 0)
                {
                    int i = userI - 1;
                    int j = userJ - 1;
                    distances[i, j] = distance;
                    distances[j, i] = distance;
                }
            }
        }

        Console.WriteLine("\nВведите расход топлива (л/100 км):");
        double fuelConsumptionPer100Km = ReadPositiveDouble();

        Console.WriteLine("\nДоступные точки:");
        for (int userPoint = 1; userPoint <= n; userPoint++)
        {
            Console.WriteLine($"Точка {userPoint}");
        }

        Console.WriteLine("\nВведите номер начальной точки:");
        int userStartPoint = ReadIntInRange(1, n);

        Console.WriteLine("Введите номер конечной точки:");
        int userEndPoint = ReadIntInRange(1, n);

        int startPoint = userStartPoint - 1;
        int endPoint = userEndPoint - 1;
        double[] shortestDistances = Dijkstra(distances, startPoint);

        if (shortestDistances[endPoint] == double.MaxValue)
        {
            Console.WriteLine("\n==============================================");
            Console.WriteLine(" Между выбранными точками нет пути!");
            Console.WriteLine("==============================================");
        }
        else
        {
            double fuelConsumed = (shortestDistances[endPoint] * fuelConsumptionPer100Km) / 100;
            Console.WriteLine("\n==============================================");
            Console.WriteLine($" Расход топлива: {fuelConsumed:F2} л");
            Console.WriteLine("==============================================");
        }
    }


    // Алгоритм Дейкстры
    public static double[] Dijkstra(double[,] a, int v0)
    {
        double[] dist = new double[n];
        bool[] vis = new bool[n];
        int unvis = n;
        int v;

        for (int i = 0; i < n; i++)
            dist[i] = double.MaxValue;
        dist[v0] = 0.0;

        while (unvis > 0)
        {
            v = -1;
            for (int i = 0; i < n; i++)
            {
                if (vis[i])
                    continue;
                if ((v == -1) || (dist[v] > dist[i]))
                    v = i;
            }
            vis[v] = true;
            unvis--;
            for (int i = 0; i < n; i++)
            {
                if (dist[i] > dist[v] + a[v, i])
                    dist[i] = dist[v] + a[v, i];
            }
        }
        return dist;
    }

    //обработка ошибок
    private static int ReadPositiveInt()
    {
        while (true)
        {
            string input = Console.ReadLine();
            if (int.TryParse(input, out int result) && result > 0)
                return result;
            Console.WriteLine("Ошибка: введите целое число больше 0!");
        }
    }

    private static double ReadNonNegativeDouble()
    {
        while (true)
        {
            string input = Console.ReadLine().Replace(',', '.');
            if (double.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out double result) && result >= 0)
                return result;
            Console.WriteLine("Ошибка: введите число больше или равное 0!");
        }
    }

    private static double ReadPositiveDouble()
    {
        while (true)
        {
            string input = Console.ReadLine();
            if (double.TryParse(input, out double result) && result > 0)
                return result;
            Console.WriteLine("Ошибка: введите число больше 0!");
        }
    }

    private static int ReadIntInRange(int min, int max)
    {
        while (true)
        {
            string input = Console.ReadLine();
            if (int.TryParse(input, out int result) && result >= min && result <= max)
                return result;
            Console.WriteLine($"Ошибка: введите число от {min} до {max}!");
        }
    }

}