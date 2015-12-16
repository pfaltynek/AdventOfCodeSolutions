using System;
using System.Runtime.InteropServices;
using System.Diagnostics.CodeAnalysis;

namespace Day08 {
	class MainClass {
		private const string input_path = "./input.txt";
		private const string backslash = "\\";
		private const string backslash_escaped = "\\\\";
		private const string dquotes = "\"";
		private const string dquotes_escaped = "\\\"";
		private const string hex_escaped = "\\x";

		public static void Main(string[] args) {
			string[] input;
			string line, rest, result;
			int index;
			int sum_chars, sum_code;
			Console.WriteLine("=== Advent of Code - day 8 ====");

			if(!System.IO.File.Exists(input_path)) {
				Console.WriteLine("input file not found");
				return;
			}

			input = System.IO.File.ReadAllLines(input_path);

			#region part 1

			sum_code = 0;
			sum_chars = 0;

			for(int i = 0; i < input.Length; i++) {
				line = input[i].Trim();
				sum_chars += line.Length;

				if(line.StartsWith(dquotes)) {
					line = line.Remove(0, 1);
				} else {
					Console.WriteLine("Leading double quotes not found on line {0}", i + 1);
				}
				if(line.EndsWith(dquotes)) {
					line = line.Remove(line.Length - 1, 1);
				} else {
					Console.WriteLine("Trailing double quotes not found on line {0}", i + 1);
				}

				rest = line;
				result = string.Empty;
				index = rest.IndexOf(backslash);
				while(index >= 0) {
					if(rest.Length >= 2) {
						result += rest.Substring(0, index);
						rest = rest.Substring(index);
						switch(rest[1]) {
							case '\\':
								result += backslash;
								rest = rest.Substring(2);
								break;
							case '\"':
								result += dquotes;
								rest = rest.Substring(2);
								break;
							case 'x':
								result += 'X';
								if(rest.Length >= 4) {
									rest = rest.Substring(4);
								} else {
									throw new FormatException(string.Format("invalid escaped hex characters: {0}", rest));
								}
								break;
							default:
								throw new FormatException(string.Format("Invalid escaped characters: {0}", rest));
						}
						index = rest.IndexOf(backslash);
					} else {
						result += rest;
					}
				}
				result += rest;
				sum_code += result.Length;
			}

			Console.WriteLine("--- part 1 ---");
			Console.WriteLine("Result is {0}", sum_chars - sum_code);

			#endregion

			#region part 2

			Console.WriteLine("--- part 2 ---");
			sum_code = 0;
			//input = new string[] { "\"\"", "\"abc\"", "\"aaa\\\"aaa\"", "\"\\x27\"" };
			for (int i = 0; i < input.Length; i++) {
				line = input[i].Trim();
				result = string.Empty;
				for (int j = 0; j < line.Length; j++) {
					switch (line[j]) {
						case '\"':
							result += dquotes_escaped;
							break;
						case '\\':
							result += backslash_escaped;
							break;
						default:
							result += line[j];
							break;
					}
				}

				result = string.Format("\"{0}\"",result);
				sum_code += result.Length;
			}

			Console.WriteLine("Result is {0}", sum_code - sum_chars);
			#endregion
		}
	}
}
