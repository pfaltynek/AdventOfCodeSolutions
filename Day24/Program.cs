using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day24 {
	class Program {
		private const string input_path = "./input.txt";

		static void Main(string[] args) {
			long result_part1 = 0, result_part2 = 0;
			int value, sum;
			string[] lines;
			List<int> weights = new List<int>();

			Console.WriteLine("=== Advent of Code - day 23 ====");

			if (!System.IO.File.Exists(input_path)) {
				Console.WriteLine("input file not found");
				return;
			}

			lines = System.IO.File.ReadAllLines(input_path);
			foreach (string item in lines) {
				if (!int.TryParse(item, out value)) {
					Console.WriteLine("Unable parse weight {0} value", item);
					return;
				}
				weights.Add(value);
			}

			weights = (from w in weights orderby w descending select w).ToList();

			sum = weights.Sum();

			#region part 1

			Console.WriteLine("--- part 1 ---");

			if (!(sum % 3).Equals(0)) {
				Console.WriteLine("Given input cannot be sorted to three groups with same weight");
				return;
			}
			else {
				sum /= 3;
			}

			result_part1 = long.MaxValue;
			value = int.MaxValue;

			FindIdealGroup(weights, weights, new List<int>(), sum, ref value, ref result_part1);

			Console.WriteLine("Result is {0}", result_part1);

			#endregion

			#region part 2

			Console.WriteLine("--- part 2 ---");

			sum = weights.Sum();

			if (!(sum % 4).Equals(0)) {
				Console.WriteLine("Given input cannot be sorted to four groups with same weight");
				return;
			}
			else {
				sum /= 4;
			}

			result_part2 = long.MaxValue;
			value = int.MaxValue;

			FindIdealGroup(weights, weights, new List<int>(), sum, ref value, ref result_part2);

			Console.WriteLine("Result is {0}", result_part2);

			#endregion
		}

		private static void FindIdealGroup(List<int> all_weights, List<int> ungrouped, List<int> group, int sum, ref int count, ref long qe) {
			int grp_sum;
			long q_e;
			
			for (int i = 0; i < ungrouped.Count; i++) {
				List<int> grp = new List<int>(group);
				grp.Add(ungrouped[i]);
				grp_sum = grp.Sum();
				if (grp_sum.Equals(sum)) {
					if (grp.Count <= count) {
						q_e = GetQE(grp);
						if (grp.Count < count) {
							qe = q_e;
							count = grp.Count;
						}
						else {
							if (q_e < qe) {
								qe = q_e;
							}
						}
					}
				}
				else if ((grp_sum < sum) && (grp.Count < count)) {
					List<int> new_ungrouped = new List<int>(ungrouped);
					for (int j = 0; j <= i; j++) {
						new_ungrouped.Remove(ungrouped[j]);
					}
					FindIdealGroup(all_weights, new_ungrouped, grp, sum, ref count, ref qe);
				}
			}
		}

		private static long GetQE(List<int> grp) {
			long result = 1;

			foreach (int item in grp) {
				result *= (long)item;
			}

			return result;
		}
	}
}
