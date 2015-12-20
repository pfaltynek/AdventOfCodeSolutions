using System;
using System.Net.WebSockets;

namespace Day10 {
	class MainClass {
		private static string input = "1113122113";

		public static void Main(string[] args) {
			string result = string.Empty;

			Console.WriteLine("=== Advent of Code - day 9 ====");

			#region part 1

			for(int i = 0; i < 40; i++) {
				
			}

			Console.WriteLine("--- part 1 ---");

			result = input;
			for(int i = 0; i < 40; i++) {
				Console.WriteLine("{0}: {1}", i + 1, result);
				Console.WriteLine();
				result = LookAndSay(result);
			}

			Console.WriteLine("Result is {0}", result.Length);

			#endregion

			#region part 2

			Console.WriteLine("--- part 2 ---");

			for(int i = 40; i < 50; i++) {
				Console.WriteLine("{0}: {1}", i + 1, result);
				Console.WriteLine();
				result = LookAndSay(result);
			}

			Console.WriteLine("Result is {0}", result.Length);

			#endregion
		}

		private static string LookAndSay(string input) {
			string result = string.Empty;
			char current;
			int counter;

			if(input.Length > 0) {
				current = input[0];
				counter = 1;

				for(int i = 1; i < input.Length; i++) {
					if(input[i].Equals(current)) {
						counter++;
					} else {
						result += string.Format("{0}{1}", counter, current);
						current = input[i];
						counter = 1;
					}
				}
			} else {
				return string.Empty;
			}

			result += string.Format("{0}{1}", counter, current);

			return result;
		}
	}
}
