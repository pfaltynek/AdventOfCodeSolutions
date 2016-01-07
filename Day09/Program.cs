using System;
using System.Collections.Generic;

namespace Day09 {
	class Program {
		private const string input_path = "./input.txt";

		static void Main(string[] args) {
			string[] input, parts;
			uint distance;
			string loc1, loc2;
			Dictionary<string,Dictionary<string, uint>> distances = new Dictionary<string, Dictionary<string, uint>>();
			Dictionary<string,uint> available_routes = new Dictionary<string, uint>();
			List<string> destinations = new List<string>();
			uint min_route, max_route;

			Console.WriteLine("=== Advent of Code - day 9 ====");

			if(!System.IO.File.Exists(input_path)) {
				Console.WriteLine("input file not found");
				return;
			}

			input = System.IO.File.ReadAllLines(input_path);

			#region part 1

			#region get all distances

			for(int i = 0; i < input.Length; i++) {
				parts = input[i].Trim().Split(new string[]{ " to ", " = " }, StringSplitOptions.RemoveEmptyEntries);
				if(!parts.Length.Equals(3)) {
					Console.WriteLine("Invalid direction at line {0}", i + 1);
					return;
				}
				if(!uint.TryParse(parts[2].Trim(), out distance)) {
					Console.WriteLine("Unable to parse distance at line {0}", i + 1);
					return;
				}

				loc1 = parts[0].Trim();
				loc2 = parts[1].Trim();

				if(!distances.ContainsKey(loc1)) {
					distances.Add(loc1, new Dictionary<string,uint>());
				}
				distances[loc1].Add(loc2, distance);

				if(!distances.ContainsKey(loc2)) {
					distances.Add(loc2, new Dictionary<string,uint>());
				}
				distances[loc2].Add(loc1, distance);
			}

			#endregion

			#region get all available routes

			foreach (string item in distances.Keys) {
				destinations.Add(item);
			}

			// test the route:
			destinations.Clear();
			destinations.AddRange(new string[] {
				"Tristram",
				"AlphaCentauri",
				"Snowdin",
				"Arbre",
				"Tambi",
				"Faerun",
				"Norrath",
				"Straylight"
			});

			FindAllAvailableRoutes(new List<string>(), destinations, distances, 0, ref available_routes);
			min_route = uint.MaxValue;
			max_route = uint.MinValue;
			foreach (string item in available_routes.Keys) {
				if(min_route > available_routes[item]) {
					min_route = available_routes[item];
				}
				if(max_route < available_routes[item]) {
					max_route = available_routes[item];
				}
			}

			#endregion

			Console.WriteLine("--- part 1 ---");

			Console.WriteLine("Result is {0}", min_route);

			#endregion

			#region part 2

			Console.WriteLine("--- part 2 ---");

			Console.WriteLine("Result is {0}", max_route);

			#endregion
		}

		private static void FindAllAvailableRoutes(
			List<string> current_route,
			List<string> available_destinations,
			Dictionary<string,Dictionary<string, uint>> distances,
			uint current_distance,
			ref Dictionary<string,uint> available_routes) {

			if(current_route.Count.Equals(0)) {
				for(int i = 0; i < available_destinations.Count; i++) {
					List<string> next_destinations = new List<string>(available_destinations);
					next_destinations.Remove(available_destinations[i]);
					List<string> updated_route = new List<string>(current_route);
					updated_route.Add(available_destinations[i]);
					FindAllAvailableRoutes(updated_route, next_destinations, distances, current_distance, ref available_routes);
				}
			} else {
				string current_dest = current_route[current_route.Count - 1];
				for(int i = 0; i < available_destinations.Count; i++) {
					if(distances[current_dest].ContainsKey(available_destinations[i])) {
						uint next_distance = current_distance + distances[current_dest][available_destinations[i]];
						if(available_destinations.Count.Equals(1)) {
							current_route.Add(available_destinations[i]);
							available_routes.Add(string.Join("->", current_route), next_distance);
						} else {
							List<string> next_destinations = new List<string>(available_destinations);
							next_destinations.Remove(available_destinations[i]);
							List<string> updated_route = new List<string>(current_route);
							updated_route.Add(available_destinations[i]);
							FindAllAvailableRoutes(updated_route, next_destinations, distances, next_distance, ref available_routes);
						}
					}
				}				
			}
		}
	}
}
