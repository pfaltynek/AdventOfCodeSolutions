using System;
using System.IO;

namespace Day18 {
	class MainClass {
		private const string input_path = "./input.txt";
		private const int input_steps = 100;

		public static void Main(string[] args) {
			string[] input;
			int result_part1, result_part2, neighbors, steps, size;
			bool[,] current, next, tmp, backup;

			Console.WriteLine("=== Advent of Code - day 18 ====");

			if(!System.IO.File.Exists(input_path)) {
				Console.WriteLine("input file not found");
				return;
			}

			input = System.IO.File.ReadAllLines(input_path);

			if(!input.Length.Equals(100)) {
				throw new InvalidDataException("Invalid input line count");
			}

			steps = input_steps;
			/*
			steps = 4;
			input = new string[] {
				".#.#.#",
				"...##.",
				"#....#",
				"..#...",
				"#.#..#",
				"####.."

				"##.#.#",
				"...##.",
				"#....#",
				"..#...",
				"#.#..#",
				"####.#"
			};
			*/
			#region get all lights input states

			size = input.Length;
			current = new bool[size, size];
			next = new bool[size, size];
			backup = new bool[size, size];
			for(int i = 0; i < size; i++) {
				if(!input[i].Length.Equals(size)) {
					throw new InvalidDataException(string.Format("Invalid characters count on line {0}", i + 1));
				}
				for(int j = 0; j < input[i].Length; j++) {
					switch(input[i][j]) {
						case '#':
							current[i, j] = true;
							break;
						case '.':
							current[i, j] = false;
							break;
						default:
							throw new InvalidDataException(string.Format("Invalid input character on line {0} at position {1}", i + 1, j + 1));
					}
				}
			}
			Array.Copy(current, backup, size * size);

			#endregion

			#region part 1

			Console.WriteLine("--- part 1 ---");

			for(int step = 0; step < steps; step++) {
				for(int x = 0; x < size; x++) {
					for(int y = 0; y < size; y++) {
						neighbors = GetNeghborsCount(x, y, current);
						if(current[x, y]) {
							if(neighbors.Equals(2) || neighbors.Equals(3)) {
								next[x, y] = true;
							} else {
								next[x, y] = false;
							}
						} else {
							if(neighbors.Equals(3)) {
								next[x, y] = true;
							} else {
								next[x, y] = false;
							}
						}
					}
				}
				tmp = next;
				next = current;
				current = tmp;
			}
			result_part1 = 0;
			for(int x = 0; x < size; x++) {
				for(int y = 0; y < size; y++) {
					if(current[x, y]) {
						result_part1++;						
					}
				}
			}
			Console.WriteLine("Result is {0}", result_part1);

			#endregion

			#region part 2

			Console.WriteLine("--- part 2 ---");

			current = backup;
			for(int step = 0; step < steps; step++) {
				for(int x = 0; x < size; x++) {
					for(int y = 0; y < size; y++) {
						if((x.Equals(0) || x.Equals(size - 1)) && (y.Equals(0) || y.Equals(size - 1))) {
							next[x, y] = true;
						} else {
							neighbors = GetNeghborsCount(x, y, current);
							if(current[x, y]) {
								if(neighbors.Equals(2) || neighbors.Equals(3)) {
									next[x, y] = true;
								} else {
									next[x, y] = false;
								}
							} else {
								if(neighbors.Equals(3)) {
									next[x, y] = true;
								} else {
									next[x, y] = false;
								}
							}
						}
					}
				}
				tmp = next;
				next = current;
				current = tmp;
			}
			result_part2 = 0;
			for(int x = 0; x < size; x++) {
				for(int y = 0; y < size; y++) {
					if(current[x, y]) {
						result_part2++;						
					}
				}
			}

			Console.WriteLine("Result is {0}", result_part2);

			#endregion		
		}

		private static int GetNeghborsCount(int x, int y, bool[,] lights) {
			int maxx, maxy, minx, miny, result = 0;

			maxx = x.Equals(lights.GetUpperBound(0)) ? lights.GetUpperBound(0) : x + 1;
			maxy = y.Equals(lights.GetUpperBound(1)) ? lights.GetUpperBound(1) : y + 1;
			minx = x > 0 ? x - 1 : 0;
			miny = y > 0 ? y - 1 : 0;
			for(int i = minx; i <= maxx; i++) {
				for(int j = miny; j <= maxy; j++) {
					if(!(i.Equals(x) && j.Equals(y))) {
						if(lights[i, j]) {
							result++;
						}
					}
				}
			}
			return result;
		}
	}
}
