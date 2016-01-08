using System;
using System.Collections.Generic;

namespace Day21 {
	class MainClass {
		#region embedded types

		private class EquipmentItem {
			public string Name { get; set; }
			public int Cost { get; set; }
			public int Damage { get; set; }
			public int Armor { get; set; }

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

		private class Fighter {
			public virtual int HitPoints { get; set; }
			public virtual int Damage { get; set; }
			public virtual int Defense { get; set; }

			private int hitpoints_init = 0;

			public Fighter(int hitpoints, int damage = 0, int armor = 0) {
				HitPoints = hitpoints;
				hitpoints_init = hitpoints;
				Damage = damage;
				Defense = armor;
			}

			public void Reset() {
				HitPoints = hitpoints_init;
			}

			public override string ToString() {
				return string.Format("HitPoints: {0} D: {1} A: {2}", HitPoints, Damage, Defense);
			}
		}

		private class EquippedFighter : Fighter {
			public int Cost { get; set; }
			private EquipmentItem weapon = null;
			public EquipmentItem Weapon
			{
				get { return weapon; }
				set { weapon = value; ReviewEquipment(); }
			}
			private EquipmentItem armor = null;
			public EquipmentItem Armor
			{
				get { return armor; }
				set { armor = value; ReviewEquipment(); }
			}
			private EquipmentItem ringleft = null;
			public EquipmentItem RingLeft
			{
				get { return ringleft; }
				set { ringleft = value; ReviewEquipment(); }
			}
			private EquipmentItem ringright = null;
			public EquipmentItem RingRight
			{
				get { return ringright; }
				set { ringright = value; ReviewEquipment(); }
			}
			public EquippedFighter(int hitpoints, int damage = 0, int armor = 0) : base(hitpoints, damage, armor) {
				Cost = 0;
			}

			private void ReviewEquipment() {
				int def = 0, dam = 0, cost = 0;

				if (weapon != null) {
					def += weapon.Armor;
					dam += weapon.Damage;
					cost += weapon.Cost;
				}

				if (armor != null) {
					def += armor.Armor;
					dam += armor.Damage;
					cost += armor.Cost;
				}

				if (ringleft != null) {
					def += ringleft.Armor;
					dam += ringleft.Damage;
					cost += ringleft.Cost;
				}

				if (ringright != null) {
					def += ringright.Armor;
					dam += ringright.Damage;
					cost += ringright.Cost;
				}

				Defense = def;
				Damage = dam;
				Cost = cost;
			}

			public override string ToString() {
				return base.ToString() + string.Format(" $: {0}", Cost);
			}
		}

		#endregion

		#region weapons

		private static EquipmentItem dagger = new EquipmentItem("Dagger", 8, 4, 0);
		private static EquipmentItem shortsword = new EquipmentItem("Shortsword", 10, 5, 0);
		private static EquipmentItem warhammer = new EquipmentItem("Warhammer", 25, 6, 0);
		private static EquipmentItem longsword = new EquipmentItem("Longsword", 40, 7, 0);
		private static EquipmentItem greataxe = new EquipmentItem("Greataxe", 74, 8, 0);

		private static List<EquipmentItem> weapons = new List<EquipmentItem>(new EquipmentItem[] { dagger, shortsword, warhammer, longsword, greataxe });

		#endregion

		#region armor

		private static EquipmentItem noarmor = new EquipmentItem("No armor", 0, 0, 0);
		private static EquipmentItem leather = new EquipmentItem("Leather", 13, 0, 1);
		private static EquipmentItem chainmail = new EquipmentItem("Chainmail", 31, 0, 2);
		private static EquipmentItem splintmail = new EquipmentItem("Splintmail", 53, 0, 3);
		private static EquipmentItem bandedmail = new EquipmentItem("Bandedmail", 75, 0, 4);
		private static EquipmentItem platemail = new EquipmentItem("Platemail", 102, 0, 5);

		private static List<EquipmentItem> armors = new List<EquipmentItem>(new EquipmentItem[] { noarmor, leather, chainmail, splintmail, bandedmail, platemail });

		#endregion

		#region rings

		private static EquipmentItem noring1 = new EquipmentItem("No ring 1", 0, 0, 0);
		private static EquipmentItem noring2 = new EquipmentItem("No ring 2", 0, 0, 0);
		private static EquipmentItem damage1 = new EquipmentItem("Damage +1", 25, 1, 0);
		private static EquipmentItem damage2 = new EquipmentItem("Damage +2", 50, 2, 0);
		private static EquipmentItem damage3 = new EquipmentItem("Damage +3", 100, 3, 0);
		private static EquipmentItem defense1 = new EquipmentItem("Defense +1", 20, 0, 1);
		private static EquipmentItem defense2 = new EquipmentItem("Defense +2", 40, 0, 2);
		private static EquipmentItem defense3 = new EquipmentItem("Defense +3", 80, 0, 3);

		private static List<EquipmentItem> rings = new List<EquipmentItem>(new EquipmentItem[] { noring1, noring2, damage1, damage2, damage3, defense1, defense2, defense3 });

		#endregion

		public static void Main(string[] args) {
			int result_part1, result_part2;
			List<EquippedFighter> wins = new List<EquippedFighter>(), loses = new List<EquippedFighter>();
			Fighter boss;
			EquippedFighter me;

			Console.WriteLine("=== Advent of Code - day 21 ====");

			#region part 1

			Console.WriteLine("--- part 1 ---");

			#region test
			/*
			boss = new Fighter(12, 7, 2);
			me = new EquippedFighter(8, 5, 5);
			bool x = Fight(me, boss);
			*/
			#endregion

			boss = new Fighter(109, 8, 2);  //*used values from input file directly
			result_part1 = int.MaxValue;
			result_part2 = int.MinValue;

			for (int w = 0; w < weapons.Count; w++) {
				for (int a = 0; a < armors.Count; a++) {
					for (int l = 0; l < rings.Count - 1; l++) {
						for (int r = l + 1; r < rings.Count; r++) {
							me = new EquippedFighter(100);
							me.Weapon = weapons[w];
							me.Armor = armors[a];
							me.RingLeft = rings[l];
							me.RingRight = rings[r];
							if (Fight(me, boss)) {
								wins.Add(me);
								if (result_part1 > me.Cost) {
									result_part1 = me.Cost;
								}
							}
							else {
								loses.Add(me);
								if (result_part2 < me.Cost) {
									result_part2 = me.Cost;
								}
							}
							boss.Reset();
						}
					}
				}
			}

			Console.WriteLine("Result is {0}", result_part1);

			#endregion

			#region part 2

			Console.WriteLine("--- part 2 ---");

			Console.WriteLine("Result is {0}", result_part2);

			#endregion
		}

		private static bool Fight(EquippedFighter me, Fighter boss) {
			bool result = false;
			int boss_damage, my_damage;

			boss_damage = boss.Damage - me.Defense;
			my_damage = me.Damage - boss.Defense;

			while (true) {
				boss.HitPoints -= my_damage;
				if (boss.HitPoints <= 0) {
					result = true;
					break;
				}
				me.HitPoints -= boss_damage;
				if (me.HitPoints <= 0) {
					break;
				}
			}

			return result;
		}
	}
}
