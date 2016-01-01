using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Day16 {
	class MainClass {
		private const string input_path = "./input.txt";
		private const string input_children = "children";
		private const string input_cats = "cats";
		private const string input_samoyeds = "samoyeds";
		private const string input_pomeranians = "pomeranians";
		private const string input_akitas = "akitas";
		private const string input_vizslas = "vizslas";
		private const string input_goldfish = "goldfish";
		private const string input_trees = "trees";
		private const string input_cars = "cars";
		private const string input_perfumes = "perfumes";

		private class AuntInfo {
			public int AuntNumber;
			public int Children;
			public int Cats;
			public int Cats2;
			public int Samoyeds;
			public int Pomeranians;
			public int Pomeranians2;
			public int Akitas;
			public int Vizslas;
			public int Goldfish;
			public int Goldfish2;
			public int Trees;
			public int Trees2;
			public int Cars;
			public int Perfumes;

			public AuntInfo() {
				Children = 3;
				Cats = 7;
				Cats2 = 8;
				Samoyeds = 2;
				Pomeranians = 3;
				Pomeranians2 = 2;
				Akitas = 0;
				Vizslas = 0;
				Goldfish = 5;
				Goldfish2 = 4;
				Trees = 3;
				Trees2 = 4;
				Cars = 2;
				Perfumes = 1;
			}
			/*
		children: 3
		cats: 7
		samoyeds: 2
		pomeranians: 3
		akitas: 0
		vizslas: 0
		goldfish: 5
		trees: 3
		cars: 2
		perfumes: 1
		*/
		}

		public static void Main(string[] args) {
			string[] input, parts;
			AuntInfo aunt;
			List<AuntInfo> aunts = new List<AuntInfo>();
			int value, result_part1, result_part2;

			Console.WriteLine("=== Advent of Code - day 16 ====");

			if(!System.IO.File.Exists(input_path)) {
				Console.WriteLine("input file not found");
				return;
			}

			input = System.IO.File.ReadAllLines(input_path);

			#region get all aunt infos

			for(int i = 0; i < input.Length; i++) {
				parts = input[i].Split(new string[]{ ": ", ", ", "Sue " }, StringSplitOptions.RemoveEmptyEntries);
				if(!(parts.Length % 2).Equals(1) || (parts.Length < 3)) {
					throw new InvalidDataException(string.Format("Invalid aunt info at line {0}", i + 1));
				}
				if(!int.TryParse(parts[0], out value)) {
					throw new InvalidDataException(string.Format("Unable parse aunt number at line {0}", i + 1));
				}
				aunt = new AuntInfo();
				aunt.AuntNumber = value;

				for(int j = 1; j < parts.Length; j += 2) {
					if(!int.TryParse(parts[j + 1], out value)) {
						throw new InvalidDataException(string.Format("Unable parse aunt detail '{1}' value at line {0}", i + 1, parts[j]));
					}	
					switch(parts[j]) {
						case input_akitas:
							aunt.Akitas = value;
							break;
						case input_cars:
							aunt.Cars = value;
							break;
						case input_cats:
							aunt.Cats = value;
							aunt.Cats2 = value;
							break;
						case input_children:
							aunt.Children = value;
							break;
						case input_goldfish:
							aunt.Goldfish = value;
							aunt.Goldfish2 = value;
							break;
						case input_perfumes:
							aunt.Perfumes = value;
							break;
						case input_pomeranians:
							aunt.Pomeranians = value;
							aunt.Pomeranians2 = value;
							break;
						case input_samoyeds:
							aunt.Samoyeds = value;
							break;
						case input_trees:
							aunt.Trees = value;
							aunt.Trees2 = value;
							break;
						case input_vizslas:
							aunt.Vizslas = value;
							break;
						default:
							throw new InvalidDataException(string.Format("Unknown aunt detail '{1}' at line {0}", i + 1, parts[j]));
					}
				}
				aunts.Add(aunt);
			}

			#endregion

			#region part 1

			Console.WriteLine("--- part 1 ---");

			var aunt_numbers = from a in aunts
			                   where (a.Akitas.Equals(0) &&
			                     a.Cars.Equals(2) &&
			                     a.Cats.Equals(7) &&
			                     a.Children.Equals(3) &&
			                     a.Goldfish.Equals(5) &&
			                     a.Perfumes.Equals(1) &&
			                     a.Pomeranians.Equals(3) &&
			                     a.Samoyeds.Equals(2) &&
			                     a.Trees.Equals(3) &&
			                     a.Vizslas.Equals(0))
			                   orderby a.AuntNumber
			                   select a;

			result_part1 = -1;
			foreach (var item in aunt_numbers) {
				if(result_part1 < 0) {
					result_part1 = item.AuntNumber;
				} else {
					throw new InvalidDataException("More than one result found");
				}
			}

			Console.WriteLine("Result is {0}", result_part1);

			#endregion

			#region part 2

			Console.WriteLine("--- part 2 ---");

			var aunt_numbers2 = from a in aunts
			                    where (a.Akitas.Equals(0) &&
			                      a.Cars.Equals(2) &&
			                      (a.Cats2 > 7) &&
			                      a.Children.Equals(3) &&
			                      (a.Goldfish2 < 5) &&
			                      a.Perfumes.Equals(1) &&
			                      (a.Pomeranians2 < 3) &&
			                      a.Samoyeds.Equals(2) &&
			                      (a.Trees2 > 3) &&
			                      a.Vizslas.Equals(0))
			                    orderby a.AuntNumber
			                    select a;

			result_part2 = -1;
			foreach (var item in aunt_numbers2) {
				if(result_part2 < 0) {
					result_part2 = item.AuntNumber;
				} else {
					throw new InvalidDataException("More than one result found");
				}
			}

			Console.WriteLine("Result is {0}", result_part2);

			#endregion
		}
	}
}
