using System;

namespace Day21 {
	class MainClass {
		private class EquipmentItem {
			public string Name{ get; set; }

			public int Cost{ get; set; }

			public int Damage{ get; set; }

			public int Armor{ get; set; }

			public EquipmentItem(string name, int cost, int damage, int armor) {
				Name = name;
				Cost = cost;
				Damage = damage;
				Armor = armor;
			}

			public override string ToString() {
				return string.Format("[{0}: ${1} D: {2} A: {3}]", Name, Cost, Damage, Armor);
			}
		}

		#region weapons

		private static EquipmentItem dagger = new EquipmentItem("Dagger", 8, 4, 0);
		private static EquipmentItem shortsword = new EquipmentItem("Shortsword", 10, 5, 0);
		private static EquipmentItem warhammer = new EquipmentItem("Warhammer", 25, 6, 0);
		private static EquipmentItem longsword = new EquipmentItem("Longsword", 40, 7, 0);
		private static EquipmentItem greataxe = new EquipmentItem("Greataxe", 74, 8, 0);

		#endregion

		#region armor

		private static EquipmentItem leather = new EquipmentItem("Leather", 13, 0, 1);
		private static EquipmentItem chainmail = new EquipmentItem("Chainmail", 31, 0, 2);
		private static EquipmentItem splintmail = new EquipmentItem("Splintmail", 53, 0, 3);
		private static EquipmentItem bandedmail = new EquipmentItem("Bandedmail", 75, 0, 4);
		private static EquipmentItem platemail = new EquipmentItem("Platemail", 102, 0, 5);
		private static EquipmentItem noarmor = new EquipmentItem("No armor", 0, 0, 0);

		#endregion

		#region rings

		private static EquipmentItem damage1 = new EquipmentItem("Damage +1", 25, 1, 0);
		private static EquipmentItem damage2 = new EquipmentItem("Damage +2", 50, 2, 0);
		private static EquipmentItem damage3 = new EquipmentItem("Damage +3", 100, 3, 0);
		private static EquipmentItem defense1 = new EquipmentItem("Defense +1", 20, 0, 1);
		private static EquipmentItem defense2 = new EquipmentItem("Defense +2", 40, 0, 2);
		private static EquipmentItem defense3 = new EquipmentItem("Defense +3", 80, 0, 3);
		private static EquipmentItem noring1 = new EquipmentItem("No ring 1", 0, 0, 0);
		private static EquipmentItem noring2 = new EquipmentItem("No ring 2", 0, 0, 0);

		#endregion

		public static void Main(string[] args) {
			int result_part1, result_part2;

			Console.WriteLine("=== Advent of Code - day 21 ====");

			#region part 1

			Console.WriteLine("--- part 1 ---");

			result_part1 = 0;

			Console.WriteLine("Result is {0}", result_part1);

			#endregion

			#region part 2

			Console.WriteLine("--- part 2 ---");

			result_part2 = 0;

			Console.WriteLine("Result is {0}", result_part2);

			#endregion
		}
	}
}
