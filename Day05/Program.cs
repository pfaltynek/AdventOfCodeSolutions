using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Day05 {
	class Program {
		private const string input_path = "./input.txt";

		static void Main(string[] args) {
			string[] input;
			int position, nice1, nice2;
			string line;

			Regex vowels = new Regex("([aeiou].*?){3,}");
			Regex doubled = new Regex("(.)\\1+");
			Regex invalid = new Regex("ab|cd|pq|xy");
			Regex dblpairs = new Regex("(.{2})(.)*?\\1");
			Regex snglpairs = new Regex("(.)(.)\\1");

			Console.WriteLine("=== Advent of Code - day 5 ====");

			if (!System.IO.File.Exists(input_path)) {
				Console.WriteLine("input file not found");
				return;
			}
			#region part 1

			Console.WriteLine("--- part 1 ---");
			input = System.IO.File.ReadAllLines(input_path);
			position = 0;
			nice1 = 0;
			nice2 = 0;



			while (position < input.Length) {
				line = input[position];

				if (!invalid.IsMatch(line)) {
					if (vowels.IsMatch(line)) {
						if (doubled.IsMatch(line)) {
							nice1++;
						}
					}
				}

				if (dblpairs.IsMatch(line)) {
					if (snglpairs.IsMatch(line)) {
						nice2++;
					}
				}

				position++;
			}

			Console.WriteLine("Result is {0}", nice1);

			#endregion

			#region part 2

			Console.WriteLine("--- part 2 ---");

			Console.WriteLine("Result is {0}", nice2);

			#endregion

			#region tests
			/*
			bool result;
			result = vowels.IsMatch("aei");
			result = vowels.IsMatch("xazegov");
			result = vowels.IsMatch("aeiouaeiouaeiou");

			result = doubled.IsMatch("xx");
			result = doubled.IsMatch("abcdde");
			result = doubled.IsMatch("aabbccdd");

			result = vowels.IsMatch("ugknbfddgicrmopn");
			result = vowels.IsMatch("aaa");
			result = vowels.IsMatch("jchzalrnumimnmhp");
			result = vowels.IsMatch("haegwjzuvuyypxyu");
			result = vowels.IsMatch("dvszwmarrgswjxmb");

			result = doubled.IsMatch("ugknbfddgicrmopn");
			result = doubled.IsMatch("aaa");
			result = doubled.IsMatch("jchzalrnumimnmhp");
			result = doubled.IsMatch("haegwjzuvuyypxyu");
			result = doubled.IsMatch("dvszwmarrgswjxmb");

			result = invalid.IsMatch("ugknbfddgicrmopn");
			result = invalid.IsMatch("aaa");
			result = invalid.IsMatch("jchzalrnumimnmhp");
			result = invalid.IsMatch("haegwjzuvuyypxyu");
			result = invalid.IsMatch("dvszwmarrgswjxmb");

			result = dblpairs.IsMatch("aaa");
			result = dblpairs.IsMatch("xyxy");
			result = dblpairs.IsMatch("aabcdefgaa");


			result = dblpairs.IsMatch("qjhvhtzxzqqjkmpb");
			result = dblpairs.IsMatch("xxyxx");
			result = dblpairs.IsMatch("uurcxstgmygtbstg");
			result = dblpairs.IsMatch("ieodomkazucvgmuy");

			result = snglpairs.IsMatch("xyx");
			result = snglpairs.IsMatch("abcdefeghi");
			result = snglpairs.IsMatch("aaa");

			result = snglpairs.IsMatch("qjhvhtzxzqqjkmpb");
			result = snglpairs.IsMatch("xxyxx");
			result = snglpairs.IsMatch("uurcxstgmygtbstg");
			result = snglpairs.IsMatch("ieodomkazucvgmuy");
			*/
			#endregion
		}
	}
}
