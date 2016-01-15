using System;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using System.IO;

namespace Day12 {
	class MainClass {
		private const string input_path = "./input.txt";

		public static void Main(string[] args) {
			string input;
			string[] items;
			int sum = 0, sum2 = 0;

			Console.WriteLine("=== Advent of Code - day 12 ====");

			if (!System.IO.File.Exists(input_path)) {
				Console.WriteLine("input file not found");
				return;
			}

			input = System.IO.File.ReadAllText(input_path);

			#region part 1

			Console.WriteLine("--- part 1 ---");

			items = input.Split(new char[] { ' ', ',', '[', ']', '{', '}', ':' }, StringSplitOptions.RemoveEmptyEntries);

			int value;
			for (int i = 0; i < items.Length; i++) {
				if (int.TryParse(items[i].Trim(), out value)) {
					sum += value;
				}
			}


			Console.WriteLine("Result is {0}", sum);

			#endregion

			#region part 2

			Console.WriteLine("--- part 2 ---");

			JsonTextReader reader = new JsonTextReader(new StringReader(input));

			sum2 = ParseJson(reader);

			Console.WriteLine("Result is {0}", sum2);

			#endregion
		}

		private static int ParseJson(JsonTextReader reader) {
			int sum = 0;
			bool is_red = false;

			while (reader.Read()) {
				switch (reader.TokenType) {
					case JsonToken.Integer:
						sum += Convert.ToInt32(reader.Value);
						break;
					case JsonToken.String:
						if ((reader.Value as string).Equals("red")) {
							is_red = true;
						}
						break;
					case JsonToken.StartObject:
						sum += ParseJson(reader);
						break;
					case JsonToken.StartArray:
						sum += ParseJson(reader);
						break;
					case JsonToken.EndObject:
						if (is_red) {
							return 0;
						}
						else {
							return sum;
						}
					case JsonToken.EndArray:
						return sum;
				}
			}

			return sum;
		}
	}
}
