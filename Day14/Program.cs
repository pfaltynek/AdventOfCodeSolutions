using System;
using System.Security.Authentication;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace Day14 {
	class MainClass {
		private const string input_path = "./input.txt";
		private const int duration_part1 = 2503;

		private class reindeer_speed {
			public string Name;
			public int Speed;
			public int SpeedDuration;
			public int RestTime;

			public reindeer_speed(string name, int speed, int speed_duration, int rest_time) {
				Name = name;
				Speed = speed;
				SpeedDuration = speed_duration;
				RestTime = rest_time;
			}
		}

		public static void Main(string[] args) {
			string[] input, parts;
			int max_distance = int.MinValue;
			Dictionary<reindeer_speed, int> distances = new Dictionary<reindeer_speed, int>();

			Console.WriteLine("=== Advent of Code - day 14 ====");

			if(!System.IO.File.Exists(input_path)) {
				Console.WriteLine("input file not found");
				return;
			}

			input = System.IO.File.ReadAllLines(input_path);

			#region get all speeds

			for(int i = 0; i < input.Length; i++) {
				int speed, time1, time2, distance;
				reindeer_speed info;

				parts = input[i].Split(new string[]{ " can fly ", " km/s for ", " seconds, but then must rest for ", " seconds." }, StringSplitOptions.RemoveEmptyEntries);
				if(!parts.Length.Equals(4)) {
					throw new InvalidDataException(string.Format("Invalid reindeer info at line {0}", i + 1));
				}
				if(!int.TryParse(parts[1], out speed)) {
					throw new InvalidDataException(string.Format("Invalid speed info at line {0}", i + 1));
				}
				if(!int.TryParse(parts[2], out time1)) {
					throw new InvalidDataException(string.Format("Invalid speed duration at line {0}", i + 1));
				}
				if(!int.TryParse(parts[3], out time2)) {
					throw new InvalidDataException(string.Format("Invalid speed rest time at line {0}", i + 1));
				}
				info = new reindeer_speed(parts[0], speed, time1, time2);
				distance = CalculateDistance(info, duration_part1);
				if(max_distance < distance) {
					max_distance = distance;
				}
				distances.Add(info, distance);
			}

			#endregion

			#region part 1

			Console.WriteLine("--- part 1 ---");


			Console.WriteLine("Result is {0}", max_distance);

			#endregion

			#region part 2

			Console.WriteLine("--- part 2 ---");

			Console.WriteLine("Result is {0}", max_distance);

			#endregion
		}

		private static int CalculateDistance(reindeer_speed info, int time) {
			int cycle_time = info.SpeedDuration + info.RestTime;
			int cycles = time / cycle_time;
			int rest_time = time % cycle_time;
			int result = cycles * info.SpeedDuration * info.Speed;
			if(rest_time >= info.SpeedDuration) {
				result += (info.SpeedDuration * info.Speed);
			} else {
				result += (rest_time * info.Speed);
			}
			return result;
		}
	}
}
