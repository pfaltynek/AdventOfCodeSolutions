using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Day15 {
	class MainClass {
		private const string input_path = "./input.txt";
		private const string input_capacity = "capacity";
		private const string input_durability = "durability";
		private const string input_flavor = "flavor";
		private const string input_texture = "texture";
		private const string input_calories = "calories";
		private const int recipe_spoons_count = 100;
		private const ulong recipe_calories_required = 500;

		private class score {
			public ulong score_without_calories = 0;
			public ulong score_calories = 0;

			public score(ulong score_without_cals, ulong score_cals) {
				score_without_calories = score_without_cals;
				score_calories = score_cals;
			}
		}

		private class ingredience {
			public string Name;
			public int Capacity;
			public int Durability;
			public int Flavor;
			public int Texture;
			public int Calories;

			public ingredience(string name, int capacity, int durability, int flavor, int texture, int calories) {
				Name = name;
				Capacity = capacity;
				Durability = durability;
				Flavor = flavor;
				Texture = texture;
				Calories = calories;
			}
		}

		private static ulong max_score_without_calories = 0, max_score_with_calories = 0;

		public static void Main(string[] args) {
			string[] input, parts, property;
			Dictionary<string, ingredience> ingrediences = new Dictionary<string, ingredience>();
			Dictionary<Dictionary<string,int>, score> recipe_scores = new Dictionary<Dictionary<string, int>, score>();
			List<string> ingredience_names;
			byte[] spoons;

			Console.WriteLine("=== Advent of Code - day 15 ====");

			if(!System.IO.File.Exists(input_path)) {
				Console.WriteLine("input file not found");
				return;
			}

			input = System.IO.File.ReadAllLines(input_path);

			#region get all ingrediences

			for(int i = 0; i < input.Length; i++) {
				int capacity = 0, durability = 0, flavor = 0, texture = 0, calories = 0;

				parts = input[i].Split(new string[]{ ": ", ", " }, StringSplitOptions.RemoveEmptyEntries);
				if(!parts.Length.Equals(6)) {
					throw new InvalidDataException(string.Format("Invalid ingredience info at line {0}", i + 1));
				}
				for(int j = 1; j < 6; j++) {
					property = parts[j].Split(new char[]{ ' ' }, StringSplitOptions.RemoveEmptyEntries);
					if(!property.Length.Equals(2)) {
						throw new InvalidDataException(string.Format("Invalid ingredience property format at line {0}/{1}", i + 1, j));
					}

					switch(property[0]) {
						case input_capacity:
							if(!int.TryParse(property[1], out capacity)) {
								throw new InvalidDataException(string.Format("Invalid ingredience capacity format at line {0}/{1}", i + 1, j));
							}
							break;
						case input_calories:
							if(!int.TryParse(property[1], out calories)) {
								throw new InvalidDataException(string.Format("Invalid ingredience calories format at line {0}/{1}", i + 1, j));
							}
							break;
						case input_durability:
							if(!int.TryParse(property[1], out durability)) {
								throw new InvalidDataException(string.Format("Invalid ingredience durability format at line {0}/{1}", i + 1, j));
							}
							break;
						case input_flavor:
							if(!int.TryParse(property[1], out flavor)) {
								throw new InvalidDataException(string.Format("Invalid ingredience flavor format at line {0}/{1}", i + 1, j));
							}
							break;
						case input_texture:
							if(!int.TryParse(property[1], out texture)) {
								throw new InvalidDataException(string.Format("Invalid ingredience texture format at line {0}/{1}", i + 1, j));
							}
							break;
						default:
							throw new InvalidDataException(string.Format("Invalid ingredience property at line {0}/{1}", i + 1, j));
					}
				}
				ingrediences.Add(parts[0], new ingredience(parts[0], capacity, durability, flavor, texture, calories));
			}
			ingredience_names = new List<string>(ingrediences.Keys);

			spoons = new byte[ingredience_names.Count];
			for(int i = 0; i < ingredience_names.Count; i++) {
				spoons[i] = 0;
			}
			CheckAllRecipes(ingredience_names, spoons, 0, ref ingrediences, ref recipe_scores);

			#endregion

			#region part 1

			Console.WriteLine("--- part 1 ---");

			Console.WriteLine("Result is {0}", max_score_without_calories);

			#endregion

			#region part 2

			Console.WriteLine("--- part 2 ---");

			Console.WriteLine("Result is {0}", max_score_with_calories);

			#endregion

		}

		private static void CheckAllRecipes(List<string> ingrediences, byte[] spoons, int index, ref Dictionary<string, ingredience> ingrediences_info, ref Dictionary<Dictionary<string, int>, score> score_list) {
			byte sum = 0;
			for(int i = 0; i < spoons.Length; i++) {
				sum += spoons[i];
			}
			if(index.Equals(spoons.Length - 1)) {
				spoons[index] = (byte)(recipe_spoons_count - sum);
				Dictionary<string,int> recipe = new Dictionary<string, int>();
				ulong score_cals, score_without_cals;

				for(int i = 0; i < spoons.Length; i++) {
					recipe.Add(ingrediences[i], spoons[i]);
				}
				CalculateRecipeScore(recipe, ref ingrediences_info, out score_without_cals, out score_cals);
				if(score_without_cals > max_score_without_calories) {
					max_score_without_calories = score_without_cals;
				}
				if(score_cals.Equals(recipe_calories_required) && (score_without_cals > max_score_with_calories)) {
					max_score_with_calories = score_without_cals;
				}
				score_list.Add(recipe, new score(score_without_cals, score_cals));
			} else {
				for(int i = 0; i < (recipe_spoons_count - sum); i++) {
					for(int j = index + 1; j < ingrediences.Count; j++) {
						spoons[j] = 0;
					}
					spoons[index] = (byte)i;
					CheckAllRecipes(ingrediences, spoons, index + 1, ref ingrediences_info, ref score_list);
				}
			}
		}

		private static void CalculateRecipeScore(Dictionary<string, int> recipe, ref Dictionary<string, ingredience> ingrediences, out ulong result_without_calories, out ulong result_calories) {
			result_without_calories = 1;
			result_calories = 1;
			int capacity = 0, durability = 0, flavor = 0, texture = 0, calories = 0;

			foreach (string ingredience in recipe.Keys) {
				capacity += recipe[ingredience] * ingrediences[ingredience].Capacity;
				durability += recipe[ingredience] * ingrediences[ingredience].Durability;
				flavor += recipe[ingredience] * ingrediences[ingredience].Flavor;
				texture += recipe[ingredience] * ingrediences[ingredience].Texture;
				calories += recipe[ingredience] * ingrediences[ingredience].Calories;
			}
			if(capacity < 0) {
				capacity = 0;
			}
			if(durability < 0) {
				durability = 0;
			}
			if(flavor < 0) {
				flavor = 0;
			}
			if(texture < 0) {
				texture = 0;
			}
			if(calories < 0) {
				calories = 0;
			}
			result_without_calories = (ulong)capacity * (ulong)durability * (ulong)flavor * (ulong)texture;
			result_calories = (ulong)calories;
		}
	}
}
