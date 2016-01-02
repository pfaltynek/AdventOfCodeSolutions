using System;
using System.IO;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using System.Diagnostics.CodeAnalysis;

namespace Day17 {
	class MainClass {
		private const string input_path = "./input.txt";
		private const int input_target = 150;

		public static void Main(string[] args) {
			string[] input;
			List<int> sizes = new List<int>();
			int value, result_part1, result_part2;
			List<List<int>> combinations = new List<List<int>>();

			Console.WriteLine("=== Advent of Code - day 17 ====");

			if(!System.IO.File.Exists(input_path)) {
				Console.WriteLine("input file not found");
				return;
			}

			input = System.IO.File.ReadAllLines(input_path);

			#region get all barrel sizes info

			for(int i = 0; i < input.Length; i++) {
				if(!int.TryParse(input[i], out value)) {
					throw new InvalidDataException(string.Format("Unable to parse barell size at line {0}", i + 1));
				}
				sizes.Add(value);
			}
			sizes = sizes.OrderByDescending(i => i).ToList();

			#endregion

			#region part 1

			Console.WriteLine("--- part 1 ---");

			FindCombinations(new List<int>(), sizes, ref combinations);
			result_part1 = combinations.Count;

			Console.WriteLine("Result is {0}", result_part1);

			#endregion

			#region part 2

			Console.WriteLine("--- part 2 ---");
			int min = combinations.Min(c => c.Count);
			var mins = from m in combinations
			           where m.Count.Equals(min)
			           select m;
			result_part2 = mins.ToList().Count;

			Console.WriteLine("Result is {0}", result_part2);

			#endregion		
		}

		private static void FindCombinations(List<int> current, List<int> available, ref List<List<int>> combinations) {
			int sum = current.Sum();
			if(sum < input_target) {
				for(int i = 0; i < available.Count; i++) {
					List<int> curr_new = new List<int>(current);
					List<int> avail_new = new List<int>();
					for(int j = i + 1; j < available.Count; j++) {
						avail_new.Add(available[j]);
					}
					curr_new.Add(available[i]);
					FindCombinations(curr_new, avail_new, ref combinations);
				}
			} else if(sum.Equals(input_target)) {
				combinations.Add(current);
			}
		}
	}
}
