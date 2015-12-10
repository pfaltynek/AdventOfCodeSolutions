using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day03 {
	class Program {
		private const string input_path = "./input.txt";

		private struct coords {
			int x;
			int y;

			public coords(int valx, int valy) {
				x = valx;
				y = valy;
			}
		}

		static void Main(string[] args) {
			string input;
			int position;
			int x, y, rx, ry;
			char c;
			coords q;

      Console.WriteLine("=== Advent of Code - day 3 ====");

			if (!System.IO.File.Exists(input_path)) {
				Console.WriteLine("input file not found");
				return;
			}
			#region part 1

			Console.WriteLine("--- part 1 ---");

			input = System.IO.File.ReadAllText(input_path);
			position = 0;
			x = y = 0;

			Dictionary<coords, int> map = new Dictionary<coords, int>();
			map.Clear();
			map.Add(new coords(0, 0), 2);

			while (position < input.Length) {
				c = input[position];
				switch (c) {
					case '>':
						x++;
						break;
					case '<':
						x--;
						break;
					case '^':
						y++;
						break;
					case 'v':
						y--;
						break;
					default:
						Console.WriteLine("Invalid input character '{0}' at position {1}", c, position + 1);
						return;
				}
				q = new coords(x, y);
				if (map.ContainsKey(q)) {
					map[q]++;
				}
				else {
					map.Add(q, 1);
				}
				position++;
			}

			Console.WriteLine("Result is {0}", map.Count);

			#endregion

			#region part 2

			Console.WriteLine("--- part 2 ---");

			map.Clear();
			map.Add(new coords(0, 0), 2);
			x = y = rx = ry = 0;
			position = 0;
			bool santa = true;

			while (position < input.Length) {
				c = input[position];
				switch (c) {
					case '>':
						if (santa) {
							x++;
						}
						else {
							rx++;
						}
						break;
					case '<':
						if (santa) {
							x--;
						}
						else {
							rx--;
						}
						break;
					case '^':
						if (santa) {
							y++;
						}
						else {
							ry++;
						}
						break;
					case 'v':
						if (santa) {
							y--;
						}
						else {
							ry--;
						}
						break;
					default:
						Console.WriteLine("Invalid input character '{0}' at position {1}", c, position + 1);
						return;
				}
				if (santa) {
					q = new coords(x, y);
				}
				else {
					q = new coords(rx, ry);
				}
				if (map.ContainsKey(q)) {
					map[q]++;
				}
				else {
					map.Add(q, 1);
				}
				position++;
				santa = !santa;
			}

			Console.WriteLine("Result is {0}", map.Count);

			#endregion
		}
	}
}
