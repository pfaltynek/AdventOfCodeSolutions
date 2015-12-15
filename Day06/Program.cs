using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Runtime.CompilerServices;

namespace Day06 {
	class Program {
		private const string input_path = "./input.txt";
		private const string turn_off = "turn off ";
		private const string turn_on = "turn on ";
		private const string toggle = "toggle ";
		private const string thru = " through ";

		private enum action {
			on,
			off,
			toggle
		}

		;

		private struct coords {
			int x;
			int y;

			public coords(int valx, int valy) {
				x = valx;
				y = valy;
			}
		}

		static void Main(string[] args) {
			string line;
			string[] input, corners;
			int position, u, l, b, r, sum;
			action cmd_act;
			bool[,] grid = new bool[1000, 1000];
			int[,] bright = new int[1000, 1000];

			Console.WriteLine("=== Advent of Code - day 6 ====");

			if (!System.IO.File.Exists(input_path)) {
				Console.WriteLine("input file not found");
				return;
			}

			for (int x = 0; x < 1000; x++) {
				for (int y = 0; y < 1000; y++) {
					grid[x, y] = false;
					bright[x, y] = 0;
				}
			}

			#region part 1

			Console.WriteLine("--- part 1 ---");

			for (int x = 0; x < 1000; x++) {
				for (int y = 0; y < 1000; y++) {
					grid[x, y] = false;
					bright[x, y] = 0;
				}
			}

			input = System.IO.File.ReadAllLines(input_path);
			position = 0;

			while (position < input.Length) {
				line = input[position].Trim().ToLower();
				if (line.StartsWith(turn_off)) {
					line = line.Replace(turn_off, string.Empty);
					cmd_act = action.off;
				}
				else if (line.StartsWith(turn_on)) {
					line = line.Replace(turn_on, string.Empty);
					cmd_act = action.on;
				}
				else if (line.StartsWith(toggle)) {
					line = line.Replace(toggle, string.Empty);
					cmd_act = action.toggle;
				}
				else {
					Console.WriteLine("Unknown instruction at line {0}", position + 1);
					return;
				}

				if (line.IndexOf(thru) < 0) {
					Console.WriteLine("Unknown instruction format at line {0} - {1} not found", position + 1, thru.Trim());
					return;
				}
				else {
					line = line.Replace(thru, ",");
				}
				corners = line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
				if (corners.Length != 4) {
					Console.WriteLine("Unknown instruction format at line {0} - invalid corners count found", position + 1);
					return;
				}

				l = int.Parse(corners[0]);
				u = int.Parse(corners[1]);
				r = int.Parse(corners[2]);
				b = int.Parse(corners[3]);

				if ((u > 999) || (u < 0) || (l > 999) || (l < 0) || (r > 999) || (r < 0) || (b > 999) || (b < 0)) {
					Console.WriteLine("Unknown instruction format at line {0} - invalid corners definition", position + 1);
					return;
				}

				//Console.WriteLine("{0}: {1}, {2} x {3}, {4}",cmd_act.ToString(), l, u, r, b);

				for (int x = l; x <= r; x++) {
					for (int y = u; y <= b; y++) {
						switch (cmd_act) {
							case action.off:
								grid[x, y] = false;
								if (bright[x, y] >= 1) {
									bright[x, y]--;
								}
								break;
							case action.on:
								grid[x, y] = true;
								bright[x, y]++;
								break;
							case action.toggle:
								grid[x, y] = !grid[x, y];
								bright[x, y]++;
								bright[x, y]++;
								break;
						}
					}
				}

				position++;
			}

			sum = 0;

			for (int x = 0; x < 1000; x++) {
				for (int y = 0; y < 1000; y++) {
					if (grid[x, y]) {
						sum++;
					}
				}
			}
			Console.WriteLine("Result is {0}", sum);

			#endregion

			#region part 2

			sum = 0;

			for (int x = 0; x < 1000; x++) {
				for (int y = 0; y < 1000; y++) {
					sum += bright[x, y];
				}
			}
			Console.WriteLine("Result is {0}", sum);

			#endregion

		}
	}
}
