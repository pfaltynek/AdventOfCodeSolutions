using System;
using System.Text.RegularExpressions;

namespace Day25 {
	class Program {
		private const string input_path = "./input.txt";
		//To continue, please consult the code grid in the manual.  Enter the code at row 2947, column 3029.
		//private const int input_row = 2947;
		//private const int input_col = 3029;
		private const uint multiplier = 252533;
		private const uint divider = 33554393;
		private const uint first_code = 20151125;

		static void Main(string[] args) {
			uint result_part1 = 0;
			string[] parts;
			string input;
			int input_row = -1, input_col = -1;
			Regex row = new Regex("row \\d{1,}");
			Regex column = new Regex("column \\d{1,}");
			Match match;
			Console.WriteLine("=== Advent of Code - day 25 ====");

			if (!System.IO.File.Exists(input_path)) {
				Console.WriteLine("input file not found");
				return;
			}

			input = System.IO.File.ReadAllText(input_path);

			match = row.Match(input);
			if (match.Success) {
				parts = match.Value.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
				if (!(parts.Length.Equals(2) && int.TryParse(parts[1], out input_row))) {
					Console.WriteLine("Row number not found in input file");
					return;
				}
			}
			else {
				Console.WriteLine("Row number not found in input file");
				return;
			}
			match = column.Match(input);
			if (match.Success) {
				parts = match.Value.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
				if (!(parts.Length.Equals(2) && int.TryParse(parts[1], out input_col))) {
					Console.WriteLine("Column number not found in input file");
					return;
				}
			}
			else {
				Console.WriteLine("Column number not found in input file");
				return;
			}

			#region test

			ulong test = FillTable(first_code, 6, 6, true);

			#endregion

			result_part1 = FillTable(first_code, input_row, input_col);

			Console.WriteLine("Result is {0}", result_part1);

		}

		private static uint FillTable(uint first, int row, int col, bool print = false) {
			int size, start_row = 0, r = 0, c = 0; ;
			uint[,] map;
			uint prev;

			size = (Math.Max(row, col) + 1) * 2;
			map = new uint[size, size];
			prev = first;
			map[0, 0] = prev;
			start_row = 0;
			r = 0;
			c = 0;

			do {
				prev = GetNextCode(prev);
				c++;
				r--;
				if (r < 0) {
					start_row++;
					r = start_row;
					c = 0;
				}
				map[r, c] = prev;
			} while ((!r.Equals(row - 1)) || (!c.Equals(col - 1)));

			if (print) {
				Console.WriteLine();
				Console.WriteLine("Test:");
				for (int rp = 0; rp < row; rp++) {
					for (int cp = 0; cp < col; cp++) {
						Console.Write("{0} ", map[rp,cp].ToString().PadLeft(10));
					}
					Console.WriteLine();
				}
			}
			return prev;
		}
		private static uint GetNextCode(uint prev_code) {
			ulong result;
			result = prev_code;
			result *= multiplier;
			result %= divider;
			
			return (uint)result;
		}
	}
}
