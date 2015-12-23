using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day13 {
	class Program {
		private const string input_path = "./input.txt";

		static void Main(string[] args) {
			string[] input, parts;
			int happines;
			string person1, person2, line;
			Dictionary<string, Dictionary<string, int>> happines_units = new Dictionary<string, Dictionary<string, int>>();
			Dictionary<List<string>, int> available_orders = new Dictionary<List<string>, int>();
			List<string> persons = new List<string>();
			int max_happines = int.MinValue, max_happines2 = int.MinValue;

			Console.WriteLine("=== Advent of Code - day 13 ====");

			if (!System.IO.File.Exists(input_path)) {
				Console.WriteLine("input file not found");
				return;
			}

			input = System.IO.File.ReadAllLines(input_path);

			#region part 1

			#region get all scores

			for (int i = 0; i < input.Length; i++) {
				line = input[i].Replace("lose ", "-");
				line = line.Replace("gain ", "+");
				parts = line.Trim().Split(new string[] { " happiness units by sitting next to ", " would ", "."}, StringSplitOptions.RemoveEmptyEntries);
				if (!parts.Length.Equals(3)) {
					Console.WriteLine("Invalid info at line {0}", i + 1);
					return;
				}
				if (!int.TryParse(parts[1].Trim(), out happines)) {
					Console.WriteLine("Unable to parse distance at line {0}", i + 1);
					return;
				}

				person1 = parts[0].Trim();
				person2 = parts[2].Trim();

				if (!happines_units.ContainsKey(person1)) {
					happines_units.Add(person1, new Dictionary<string, int>());
				}
				happines_units[person1].Add(person2, happines);


			}

			#endregion

			#region get all available routes and calculate their happines

			persons.Clear();
			foreach (string item in happines_units.Keys) {
				persons.Add(item);
			}

			available_orders.Clear();
			FillAllSittingOrders(persons, new List<string>(), ref available_orders, ref happines_units);

			foreach (List<string> item in available_orders.Keys) {
				if (available_orders[item] > max_happines) {
					max_happines = available_orders[item];
				}
			}

			#endregion

			Console.WriteLine("--- part 1 ---");

			foreach (string item in happines_units.Keys) {
				happines_units[item].Add("Me", 0);
			}
			happines_units.Add("Me", new Dictionary<string, int>());
			foreach (string name in persons) {
				happines_units["Me"].Add(name, 0);
			}
			persons.Add("Me");

			available_orders.Clear();
			FillAllSittingOrders(persons, new List<string>(), ref available_orders, ref happines_units);

			foreach (List<string> item in available_orders.Keys) {
				if (available_orders[item] > max_happines2) {
					max_happines2 = available_orders[item];
				}
			}

			Console.WriteLine("Result is {0}", max_happines);

			#endregion

			#region part 2

			Console.WriteLine("--- part 2 ---");

			Console.WriteLine("Result is {0}", max_happines2);

			#endregion
		}

		private static void FillAllSittingOrders(List<string> free_persons, List<string> sitting_order, ref Dictionary<List<string>, int> available_orders, ref Dictionary<string, Dictionary<string, int>> units) {
			if (free_persons.Count.Equals(1)) {
				List<string> next_sitting_order = new List<string>(sitting_order);
				next_sitting_order.Add(free_persons[0]);
				available_orders.Add(next_sitting_order, CalculateSittingOrderHappines(next_sitting_order, ref units));
			}
			else {
				for (int i = 0; i < free_persons.Count; i++) {
					string item = free_persons[i];
					List<string> next_sitting_order = new List<string>(sitting_order);
					List<string> next_free_persons = new List<string>(free_persons);
					next_sitting_order.Add(item);
					next_free_persons.Remove(item);
					FillAllSittingOrders(next_free_persons, next_sitting_order, ref available_orders, ref units);
				}
			}
		}

		private static int CalculateSittingOrderHappines(List<string> sitting_order, ref Dictionary<string, Dictionary<string, int>> units) {
			int result = 0;

			for (int i = 0; i < sitting_order.Count; i++) {
				string left, right;

				if (i.Equals(0)) {
					left = sitting_order[sitting_order.Count - 1];
					right = sitting_order[i + 1];
				}
				else if (i.Equals(sitting_order.Count - 1)) {
					left = sitting_order[i - 1];
					right = sitting_order[0];
				}
				else {
					left = sitting_order[i - 1];
					right = sitting_order[i + 1];
				}
				result += units[sitting_order[i]][left];
				result += units[sitting_order[i]][right];
			}
			return result;
		}

		private static void FindAllAvailableRoutes(
			List<string> current_route,
			List<string> available_destinations,
			Dictionary<string, Dictionary<string, uint>> distances,
			uint current_distance,
			ref Dictionary<string, uint> available_routes) {

			if (current_route.Count.Equals(0)) {
				for (int i = 0; i < available_destinations.Count; i++) {
					List<string> next_destinations = new List<string>(available_destinations);
					next_destinations.Remove(available_destinations[i]);
					List<string> updated_route = new List<string>(current_route);
					updated_route.Add(available_destinations[i]);
					FindAllAvailableRoutes(updated_route, next_destinations, distances, current_distance, ref available_routes);
				}
			}
			else {
				string current_dest = current_route[current_route.Count - 1];
				for (int i = 0; i < available_destinations.Count; i++) {
					if (distances[current_dest].ContainsKey(available_destinations[i])) {
						uint next_distance = current_distance + distances[current_dest][available_destinations[i]];
						if (available_destinations.Count.Equals(1)) {
							current_route.Add(available_destinations[i]);
							available_routes.Add(string.Join("->", current_route), next_distance);
						}
						else {
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
