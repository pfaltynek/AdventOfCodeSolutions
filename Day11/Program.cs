using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Security.Cryptography;

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
			/*
			bool test;
			test = CheckStraight3Chars("hijklmmn");
			test = CheckDisabledCharacters("hijklmmn");
			test = CheckDifferentPairs("hijklmmn");

			test = CheckStraight3Chars("abbceffg");
			test = CheckDisabledCharacters("abbceffg");
			test = CheckDifferentPairs("abbceffg");

			test = CheckStraight3Chars("abcdefgh");
			test = CheckDisabledCharacters("abcdefgh");
			test = CheckDifferentPairs("abcdefgh");

			test = CheckStraight3Chars("abcdffaa");
			test = CheckDisabledCharacters("abcdffaa");
			test = CheckDifferentPairs("abcdffaa");

			test = CheckStraight3Chars("ghijklmn");
			test = CheckDisabledCharacters("ghijklmn");
			test = CheckDifferentPairs("ghijklmn");

			test = CheckStraight3Chars("ghjaabcc");
			test = CheckDisabledCharacters("ghjaabcc");
			test = CheckDifferentPairs("ghjaabcc");
			*/

			result = input;
			do {
				IncrementPassword(ref result);
			} while(!CheckPassword(result));

			Console.WriteLine("Result is {0}", result);

			#endregion

			#region part 2

			Console.WriteLine("--- part 2 ---");

			do {
				IncrementPassword(ref result);
			} while(!CheckPassword(result));

			Console.WriteLine("Result is {0}", result);

			#endregion
		}

		private static void IncrementPassword(ref string current_password) {
			bool overflow = false;
			char[] pwd = current_password.ToCharArray();
			int i = pwd.Length - 1;

			if(i < 0) {
				return;
			}

			do {
				pwd[i]++;
				if(pwd[i] > 'z') {
					pwd[i] = 'a';
					overflow = true;
				} else {
					overflow = false;
				}
				i--;
			} while(overflow && (i >= 0));

			current_password = new string(pwd);
		}

		private static bool CheckPassword(string password) {
			return CheckStraight3Chars(password) && CheckDifferentPairs(password) && CheckDisabledCharacters(password);
		}

		private static bool CheckDisabledCharacters(string password) {
			return !disabled_characters.IsMatch(password);
		}

		private static bool CheckDifferentPairs(string password) {
			List<string> pairs = new List<string>();

			foreach (Match item in single_pair.Matches(password)) {
				if(!pairs.Contains(item.Value)) {
					pairs.Add(item.Value);
				}
			}

			return pairs.Count >= 2;
		}

		private static bool CheckStraight3Chars(string password) {
			return increasing_straight3character.IsMatch(password);
		}
	}
}
