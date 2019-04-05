using System;

/// <summary>
/// #contest #yandex
/// official.contest.yandex.ru/contest
/// Kirill & Old LCD calculator
/// </summary>
namespace CalcYandex
{
	class Program
    {
		/// <summary>
		/// array of costs each symbol
		/// </summary>
        private static readonly int[] cost_array = new int[10]
        {
        //  0  1  2  3  4  5  6  7  8  9
            6, 2, 5, 5, 4, 5, 6, 3, 7, 6
        };

		/// <summary>
		/// Array for minimal number
		/// </summary>
        private static int[] arr_min;

		/// <summary>
		/// Array for maximal number
		/// </summary>
        private static int[] arr_max;

		static void Main(string[] args)
		{
			do
			{
				#region Prepare Console

				Console.ForegroundColor = ConsoleColor.Green;

				Console.WriteLine("Any key - Run task\n" +
				  "Esc - Exit\n");
				ConsoleKeyInfo key = Console.ReadKey();
				Console.WriteLine();
				
				#endregion

				switch (key.Key)
				{
					case ConsoleKey.Escape:
						return;
					default:

						{
							#region Input data
							
							var n = RequestIntValue("Please type N:");

							var k = RequestIntValue("Please type K:");
							
							var startTime = new DateTime();
							var endTime = new DateTime();
							startTime = DateTime.Now;

							Console.WriteLine($"N: {n}; K: {k}\nStart time: {startTime}");

							#endregion

							#region Check if NO SOLUTION

							// Check if no solution and break
							if (( n * 2 > k ) | ( n * 7 < k ) | n < 0 | k < 0)
							{
								Console.WriteLine("NO SOLUTION");
								Console.ReadKey();
								Console.Clear();
								break;
							}

							#endregion

							#region Prepare arrays

							// Set min value of number
							arr_min = new int[n];
							for (int i = 1; i < n; i++)
								arr_min[i] = 0;
							arr_min[0] = 1;

							// Set sum of black lines
							int sum_min = n * 6 - 4;

							// Set Max value of number
							arr_max = new int[n];
							for (int i = 0; i < n; i++)
								arr_max[i] = 9;

							// Set sum of black lines
							int sum_max = n * 6;

							#endregion

							#region Calculation

							MinNumber(n - 1, sum_min, k);
							MaxNumber(n - 1, sum_max, k);

							#endregion

							#region Output data

							endTime = DateTime.Now;
							Console.WriteLine($"Elapsed Time: {endTime - startTime}");

							Console.WriteLine("Min value is:");
							for (int i = 0; i < n; i++)
							{
								Console.Write($"{arr_min[i]} ");
							}
							Console.WriteLine();

							Console.WriteLine("Max value is:");
							for (int i = 0; i < n; i++)
							{
								Console.Write($"{arr_max[i]} ");
							}
							Console.WriteLine();

							Console.ReadKey();
							Console.Clear();

							#endregion
						}
						break;
				}
			} while (true);

		}

		/// <summary>
		/// Input value and convert them from Str to Int
		/// repeat until value become integer and can be converted
		/// </summary>
		/// <param name="message">string incoming from programm</param>
		/// <returns>integer value number</returns>
		public static int RequestIntValue(string message)
		{
			int number;

			Console.ForegroundColor = ConsoleColor.Red;
			do
			{ Console.WriteLine(message); }
			while (!Int32.TryParse(Console.ReadLine(), out number));

			Console.ForegroundColor = ConsoleColor.Green;
			return number;
		}

		/// <summary>
		/// Method Update value and Calculate new sum
		/// </summary>
		/// <param name="value">new value</param>
		/// <param name="cursor">current position in array</param>
		/// <param name="sum">current sum</param>
		/// <param name="arr">current array</param>
		/// <returns>new sum of black lines</returns>
		static int UpdateValue(int value, int cursor, int sum, int[] arr)
			{
			sum = sum + cost_array[value] - cost_array[arr[cursor]];
			arr[cursor] = value;
			return sum;
			}

		/// <summary>
		/// Method find Minimal Number with requested sum of black lines
		/// </summary>
		/// <param name="cursor">current position in array</param>
		/// <param name="sum">current sum</param>
		/// <param name="k">requested sum</param>
        static void MinNumber(int cursor, int sum, int k)
        {
            while (sum > k)
				sum = UpdateValue(1, cursor--, sum, arr_min);

			cursor = arr_min.Length - 1;
            while (sum < k)
            {
                switch (arr_min[cursor])
                {
                    case 0:
					case 6:
					case 9:
						sum = UpdateValue(8, cursor--, sum, arr_min);
                        break;
                    case 1:              
                        sum = UpdateValue(7, cursor, sum, arr_min);
                        break;
                    case 2:
					case 3:
					case 5:
                        sum = UpdateValue(0, cursor, sum, arr_min);
                        break;
                    case 4:
                        sum = UpdateValue(2, cursor, sum, arr_min);
                        break;
                    case 7:
                        sum = UpdateValue(4, cursor, sum, arr_min);
                        break;
                    case 8:
                        cursor--;
                        break;
                }
            }
        }

		/// <summary>
		/// Method find Maximal Number with requested sum of black lines
		/// </summary>
		/// <param name="cursor">current position in array</param>
		/// <param name="sum">current sum</param>
		/// <param name="k">requested sum</param>
		static void MaxNumber(int cursor, int sum, int k)
        {
			while (sum > k)
			{
				sum = UpdateValue(1, cursor, sum, arr_max);
				if (sum > k) cursor--;
			}

            while (sum < k & cursor < arr_max.Length)
            {
                switch (arr_max[cursor])
                {
                    case 0:
					case 6:
					case 9:
						sum = UpdateValue(8, cursor++, sum, arr_max);                        
                        break;
                    case 1:
						sum = UpdateValue(7, cursor, sum, arr_max);
                        break;
                    case 2:
					case 3:
					case 5:
						sum = UpdateValue(9, cursor, sum, arr_max);
                        break;
                    case 4:
						sum = UpdateValue(5, cursor, sum, arr_max);
                        break;
                    case 7:
                        sum = UpdateValue(4, cursor, sum, arr_max);
                        break;
                    case 8:
                        cursor++;
                        break;
                }
            }

			cursor = arr_max.Length - 1;
			while (sum < k)
            {
				switch (arr_max[cursor])
				{
					case 0:
					case 6:
					case 9:
						sum = UpdateValue(8, cursor--, sum, arr_max);
						break;
					case 1:
						sum = UpdateValue(7, cursor, sum, arr_max);
						break;
					case 2:
					case 3:
					case 5:
						sum = UpdateValue(9, cursor, sum, arr_max);
						break;
					case 4:
						sum = UpdateValue(5, cursor, sum, arr_max);
						break;
					case 7:
						sum = UpdateValue(4, cursor, sum, arr_max);
						break;
					case 8:
						cursor--;
						break;
				}
            }
        }
    }
}
