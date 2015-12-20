using System;
using System.Text.RegularExpressions;

namespace Day11 {
	class MainClass {
		private static string input = "vzbxkghb";
		private static Regex disabled_characters = new Regex("[iol]");
		private static Regex increasing_straight3character = new Regex("(abc|bcd|cde|def|efg|fgh|ghi|hij|ijk|jkl|klm|lmn|mno|nop|opq|pqr|qrs|rst|stu|tuv|uvw|vwx|wxy|xyz)");
		private static Regex single_pair = new Regex("([a-z])\\1");

		public static void Main(string[] args) {
			string result = string.Empty;
			/*
			Regex vowels = new Regex("([aeiou].*?){3,}");
			Regex doubled = new Regex("(.)\\1+");
			Regex invalid = new Regex("ab|cd|pq|xy");
			Regex dblpairs = new Regex("(.{2})(.)*?\\1");
			Regex snglpairs = new Regex("(.)(.)\\1");
			*/

			Console.WriteLine("=== Advent of Code - day 11 ====");

			#region part 1

			Console.WriteLine("--- part 1 ---");

			result = input;
			do {
				IncrementPassword(ref result);
			} while(!CheckPassword(result));

			Console.WriteLine("Result is {0}", result);

			#endregion

			#region part 2

			Console.WriteLine("--- part 2 ---");

			Console.WriteLine("Result is {0}", result);

			#endregion
		}

		private static void IncrementPassword(ref string current_password) {
			int index = current_password.Length - 1;
			bool overflow = false;

			do {
				current_password[i]++;
				if(current_password[i] > 'z') {
					current_password[i] = 'a';
					overflow = true;
				} else {
					overflow = false;
				}
			} while(overflow && (index >= 0));
		}

		private static bool CheckPassword(string password) {
			single_pair.Matches();
			return true;
		}
	}
}
