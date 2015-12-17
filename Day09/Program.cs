using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day09 {
	class Program {
		private const string input_path = "./input.txt";

		static void Main(string[] args) {
			string[] input, parts;
			uint distance;
			string loc1, loc2;
			Dictionary<string,Dictionary<string, uint>> distances = new Dictionary<string, Dictionary<string, uint>>();
			List<string> destinations = new List<string>();
			List<string> route = new List<string>();
			List<List<string>> valid_routes = new List<List<string>>();
			int[,] index_map;
			int max_routes, tmp;

			Console.WriteLine("=== Advent of Code - day 9 ====");

			if(!System.IO.File.Exists(input_path)) {
				Console.WriteLine("input file not found");
				return;
			}

			input = System.IO.File.ReadAllLines(input_path);

			#region part 1

			#region get all distances

			for(int i = 0; i < input.Length; i++) {
				parts = input[i].Trim().Split(new string[]{ " to ", " = " }, StringSplitOptions.RemoveEmptyEntries);
				if(!parts.Length.Equals(3)) {
					Console.WriteLine("Invalid direction at line {0}", i + 1);
					return;
				}
				if(!uint.TryParse(parts[2].Trim(), out distance)) {
					Console.WriteLine("Unable to parse distance at line {0}", i + 1);
					return;
				}

				loc1 = parts[0].Trim();
				loc2 = parts[1].Trim();

				if(!distances.ContainsKey(loc1)) {
					distances.Add(loc1, new Dictionary<string,uint>());
				}
				distances[loc1].Add(loc2, distance);

				if(!distances.ContainsKey(loc2)) {
					distances.Add(loc2, new Dictionary<string,uint>());
				}
				distances[loc2].Add(loc1, distance);
			}

			#endregion

			#region get all available routes

			foreach (string item in distances.Keys) {
				destinations.Add(item);
			}

			tmp = destinations.Count;
			max_routes = 1;
			while(tmp > 1) {
				max_routes = max_routes * tmp;
				tmp--;
			}

			index_map = new int[max_routes, destinations.Count];

			#endregion

			Console.WriteLine("--- part 1 ---");

			Console.WriteLine("Result is {0}", 0);

			#endregion

			#region part 2

			Console.WriteLine("--- part 2 ---");

			Console.WriteLine("Result is {0}", 0);

			#endregion
		}
	}
}
