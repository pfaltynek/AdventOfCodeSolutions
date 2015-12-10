using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day01 {
	class Program {
		private const string input_path = "./input.txt";
		static void Main(string[] args) {
			string input = string.Empty;
			int position, floor, result2;
			char c;

			Console.WriteLine("=== Advent of Code - day 1 ====");

			if (!System.IO.File.Exists(input_path)) {
				Console.WriteLine("input file not found");
				return;
			}
			#region part 1

			Console.WriteLine("--- part 1 ---");
			input = System.IO.File.ReadAllText(input_path);
			position = 0;
			floor = 0;
			while (position < input.Length) {
				c = input[position];
				switch (c) {
					case '(':
						floor++;
						break;
					case ')':
						floor--;
						break;
				}
				position++;
			}

			Console.WriteLine("Result is {0}", floor);

			#endregion

			#region part 2

			Console.WriteLine("--- part 2 ---");
			position = 0;
			floor = 0;
			while (position < input.Length) {
				c = input[position];
				switch (c) {
					case '(':
						floor++;
						break;
					case ')':
						floor--;
						break;
				}
				position++;
				if (floor < 0) {
					break;
				}
			}
			Console.WriteLine("Result is {0}", position);

			#endregion
		}
	}
}
