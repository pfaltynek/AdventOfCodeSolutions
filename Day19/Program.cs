using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Security.AccessControl;
using System.Dynamic;

namespace Day19 {
	class MainClass {
		private const string input_path = "./input.txt";

		public static void Main(string[] args) {
			string[] input, parts;
			int result_part1, result_part2, index;
			Dictionary<string, List<string>> replacements = new Dictionary<string, List<string>>();
			string medicine, derivate;
			List <string> medicines = new List<string>();

			Console.WriteLine("=== Advent of Code - day 19 ====");

			if(!System.IO.File.Exists(input_path)) {
				Console.WriteLine("input file not found");
				return;
			}

			input = System.IO.File.ReadAllLines(input_path);

			#region get all replacements

			index = 0;
			while(!input[index].Equals(string.Empty)) {
				string left, right;
				parts = input[index].Split(new string[] { " => " }, StringSplitOptions.RemoveEmptyEntries);
				if(!parts.Length.Equals(2)) {
					throw new InvalidDataException(string.Format("Invalid replacement format at line {0}", index + 1));
				}
				left = parts[0].Trim();
				right = parts[1].Trim();
				if(!replacements.ContainsKey(left)) {
					replacements.Add(left, new List<string>());
				}
				List<string> x = replacements[left];
				x.Add(right);
				replacements[left] = x;

				index++;
			}

			#endregion

			#region get medicine

			while(input[index].Equals(string.Empty)) {
				index++;
			}
			medicine = input[index].Trim();

			#endregion

			#region part 1

			Console.WriteLine("--- part 1 ---");

			foreach (string item in replacements.Keys) {
				foreach (string replacement in replacements[item]) {
					index = medicine.IndexOf(item);
					while(index >= 0) {
						derivate = medicine.Substring(0, index) + replacement + medicine.Substring(index + item.Length);
						if(!medicines.Contains(derivate)) {
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

			Console.WriteLine("Result is {0}", result_part2);

			#endregion		
		}
	}
}
