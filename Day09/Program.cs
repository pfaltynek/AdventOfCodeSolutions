using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day09 {
	class Program {
		private const string input_path = "./input.txt";

		static void Main(string[] args) {
			string[] input;
			string line, rest, result;
			int index;
			int sum_chars, sum_code;
			Console.WriteLine("=== Advent of Code - day 9 ====");

			if (!System.IO.File.Exists(input_path)) {
				Console.WriteLine("input file not found");
				return;
			}

			input = System.IO.File.ReadAllLines(input_path);

			#region part 1

			sum_code = 0;
			sum_chars = 0;

			for (int i = 0; i < input.Length; i++) {
				line = input[i].Trim();


			}

			Console.WriteLine("--- part 1 ---");
			Console.WriteLine("Result is {0}", sum_chars - sum_code);

			#endregion

			#region part 2

			Console.WriteLine("--- part 2 ---");


			Console.WriteLine("Result is {0}", sum_code - sum_chars);

			#endregion
		}
	}
}
