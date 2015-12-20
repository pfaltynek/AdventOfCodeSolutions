﻿using System;
using System.Runtime.InteropServices;

namespace Day12 {
	class MainClass {
		private const string input_path = "./input.txt";

		public static void Main(string[] args) {
			string input;
			string[] items;
			int sum = 0;

			Console.WriteLine("=== Advent of Code - day 9 ====");

			if(!System.IO.File.Exists(input_path)) {
				Console.WriteLine("input file not found");
				return;
			}

			input = System.IO.File.ReadAllText(input_path);

			#region part 1

			Console.WriteLine("--- part 1 ---");

			items = input.Split(new char[]{ ' ', ',', '[', ']', '{', '}', ':' }, StringSplitOptions.RemoveEmptyEntries);

			int value;
			for(int i = 0; i < items.Length; i++) {
				if(int.TryParse(items[i].Trim(), out value)) {
					sum += value;
				}
			}


			Console.WriteLine("Result is {0}", sum);

			#endregion

			#region part 2

			Console.WriteLine("--- part 2 ---");

			Console.WriteLine("Result is {0}", sum);

			#endregion
		}
	}
}
