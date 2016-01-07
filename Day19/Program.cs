using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Day19 {
	class MainClass {
		private const string input_path = "./input.txt";

		public static void Main(string[] args) {
			string[] input, parts;
			int result_part1, result_part2, index;
			Dictionary<string, List<string>> replacements = new Dictionary<string, List<string>>();
			string medicine, derivate, reverse;
			List<string> medicines = new List<string>();

			Console.WriteLine("=== Advent of Code - day 19 ====");

			if (!System.IO.File.Exists(input_path)) {
				Console.WriteLine("input file not found");
				return;
			}

			input = System.IO.File.ReadAllLines(input_path);

			#region get all replacements

			index = 0;
			while (!input[index].Equals(string.Empty)) {
				string left, right;
				parts = input[index].Split(new string[] { " => " }, StringSplitOptions.RemoveEmptyEntries);
				if (!parts.Length.Equals(2)) {
					throw new InvalidDataException(string.Format("Invalid replacement format at line {0}", index + 1));
				}
				left = parts[0].Trim();
				right = parts[1].Trim();
				if (!replacements.ContainsKey(left)) {
					replacements.Add(left, new List<string>());
				}
				List<string> x = replacements[left];
				x.Add(right);
				replacements[left] = x;

				index++;
			}

			#endregion

			#region get medicine

			while (input[index].Equals(string.Empty)) {
				index++;
			}
			medicine = input[index].Trim();
			reverse = medicine;

			#endregion

			#region part 1

			Console.WriteLine("--- part 1 ---");

			foreach (string item in replacements.Keys) {
				foreach (string replacement in replacements[item]) {
					index = medicine.IndexOf(item);
					while (index >= 0) {
						derivate = medicine.Substring(0, index) + replacement + medicine.Substring(index + item.Length);
						if (!medicines.Contains(derivate)) {
							medicines.Add(derivate);
						}
						index = medicine.IndexOf(item, index + item.Length);
					}
				}
			}

			result_part1 = medicines.Count;

			Console.WriteLine("Result is {0}", result_part1);

			#endregion

			#region part 2

			Console.WriteLine("--- part 2 ---");

			result_part2 = 0;

			Dictionary<string, string> rev_replacements = new Dictionary<string, string>();
			foreach (string key in replacements.Keys) {
				foreach (string value in replacements[key]) {
					rev_replacements.Add(value, key);
				}
			}

			result_part2 = ReverseMedicine(reverse, rev_replacements);
			
			Console.WriteLine("Result is {0}", result_part2);

			#endregion
		}


		/// <summary>
		/// Idea from http://markheath.net/post/advent-of-code-day-19%E2%80%93mutating
		/// not needed at all if replace routine is well written
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <returns></returns>
		public static IEnumerable<T> Shuffle<T>(IEnumerable<T> source) {
			Random rnd = new Random();
			return source.OrderBy<T, int>((item) => rnd.Next());
		}

		private static int ReverseMedicine(string medicine, Dictionary<string, string> rev_replacements) {
			int index, result = 0;
			string wide, replace, reverse_part, tmp;

			List<string> order = new List<string>(rev_replacements.Keys);

			reverse_part = medicine;

			while (!reverse_part.Equals("e")) {
				tmp = reverse_part;
				foreach (string item in order) {
					wide = item;
					replace = rev_replacements[item];
					index = reverse_part.IndexOf(wide);
					if (index >= 0) {
						reverse_part = reverse_part.Substring(0, index) + replace + reverse_part.Substring(index + wide.Length);
						result++;
					}
				}

				if (tmp.Equals(reverse_part)) {
					result = 0;
					order = Shuffle(order).ToList();
					reverse_part = medicine;
				}
			}

			return result;
		}
	}
}
