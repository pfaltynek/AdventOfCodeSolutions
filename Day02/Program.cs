using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day02 {
	class Program {
		private const string input_path = "./input.txt";

		static void Main(string[] args) {
			string[] input, edges;
			int position, a, b, c, w1, w2, w3, slack, total;
			int ribbon, bow, e1, e2;
			string line;

			Console.WriteLine("=== Advent of Code - day 2 ====");

			if (!System.IO.File.Exists(input_path)) {
				Console.WriteLine("input file not found");
				return;
			}
			#region part 1

			Console.WriteLine("--- part 1 ---");
			input = System.IO.File.ReadAllLines(input_path);
			position = 0;
			total = 0;
			ribbon = 0;
			while (position < input.Length) {
				line = input[position].Trim();
				edges = line.Split(new char[] { 'x' }, StringSplitOptions.RemoveEmptyEntries);
				if (edges.Length != 3) {
					Console.WriteLine("Invalid parameters at line {0}", position + 1);
					return;
				}
				a = int.Parse(edges[0]);
				b = int.Parse(edges[1]);
				c = int.Parse(edges[2]);

				bow = a * b * c;

				if (a < b) {
					e1 = a;
					e2 = b;
				}
				else {
					e1 = b;
					e2 = a;
				}

				if (c <= e1) {
					e2 = e1;
					e1 = c;
				}
				else if (c <= e2) {
					e2 = c;
				}

				w1 = a * b;
				w2 = b * c;
				w3 = a * c;
				slack = w1;

				if (w2 < slack) {
					slack = w2;
				}
				if (w3 < slack) {
					slack = w3;
				}

				total += (2 * w1) + (2 * w2) + (2 * w3) + slack;

				ribbon += bow + (2 * e1) + (2 * e2);
				position++;
			}

			Console.WriteLine("Result is {0}", total);

			#endregion

			#region part 2

			Console.WriteLine("--- part 2 ---");
			Console.WriteLine("Result is {0}", ribbon);

			#endregion
		}
	}
}
