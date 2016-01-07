using System;
using System.Collections.Generic;
using System.Linq;

namespace Day20 {
	class Program {
		private const int input_limit = 36000000;

		static void Main(string[] args) {
			int result_part1, result_part2;
			Console.WriteLine("=== Advent of Code - day 20 ====");

			#region part 1

			Console.WriteLine("--- part 1 ---");

			result_part1 = Calculate(10, input_limit);

			Console.WriteLine("Result is {0}", result_part1);

			#endregion

			#region part 2

			Console.WriteLine("--- part 2 ---");

			result_part2 = Calculate(11, input_limit, 50);

			Console.WriteLine("Result is {0}", result_part2);

			#endregion
		}

		private static int Calculate(int presents, int limit, int house_limit = -1) {
			int result = -1;
			int name = 1;
			int len = 1000000, len2;
			int[] map = new int[len];
			int amount;

			while(result < 0) {
				if(house_limit > 0) {
					len2 = house_limit * name;
					len2++;
					if(len2 > len) {
						len2 = len;
					}
				} else {
					len2 = len;
				}

				amount = name * presents;
				for(int i = name; i < len2; i += name) {
					map[i] += amount;
					if(map[i] >= limit) {
						result = i;
						break;
					}
				}
				name++;
			}
			return result;

			/*
			int[] temp = new int[100000000];
			for (int i = 1; i < 1000000; i++) {
				//for(int x=0;x<i*50;x+=i) { //x<temp.length for part 1
				for (int x = 0; x < temp.Length; x += i) { //x<temp.length for part 1
					temp[x] += i * 10; //adjust to be 10 or 11
				}
			}
			temp[0] = 0;
			for (int i = 0; i < temp.Length; i++)
				if (temp[i] >= 36000000) {
					return i;
				}

			return -1;
			*/
		}
	}
}
