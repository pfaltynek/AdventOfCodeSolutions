using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Day22 {
	class Program {
		private const string input_path = "./input.txt";
		private const string input_hitpoints = "Hit Points";
		private const string input_damage = "Damage";

		#region embedded types

		private class Fighter {
			public virtual int HitPoints { get; set; }

			public virtual int Damage { get; set; }

			private int hitpoints_init = 0;

			public Fighter(int hitpoints, int damage = 0) {
				HitPoints = hitpoints;
				hitpoints_init = hitpoints;
				Damage = damage;
			}

			public Fighter(Fighter o) {
				HitPoints = o.HitPoints;
				Damage = o.Damage;
				hitpoints_init = o.hitpoints_init;
			}

			public Fighter(string path) {
				string[] input, parts;
				Dictionary<string, int> items = new Dictionary<string, int>();
				int value;

				input = System.IO.File.ReadAllLines(path);
				foreach (string item in input) {
					parts = item.Split(new string[] { ": " }, StringSplitOptions.RemoveEmptyEntries);
					if (!parts.Length.Equals(2)) {
						throw new InvalidDataException(string.Format("Invalid input format: '{0}'", item));
					}
					if (items.ContainsKey(parts[0])) {
						throw new InvalidDataException(string.Format("Duplicit input information: '{0}'", item));
					}
					if (!int.TryParse(parts[1], out value)) {
						throw new InvalidDataException(string.Format("Unable to parse value: '{0}'", parts[1]));
					}
					items.Add(parts[0], value);
				}

				if (!items.ContainsKey(input_hitpoints)) {
					throw new InvalidDataException("Hit points value not found");
				}
				else {
					HitPoints = items[input_hitpoints];
					hitpoints_init = items[input_hitpoints];
				}
				if (!items.ContainsKey(input_damage)) {
					throw new InvalidDataException("Damage value not found");
				}
				else {
					Damage = items[input_damage];
				}
			}

			public void Reset() {
				HitPoints = hitpoints_init;
			}

			public override string ToString() {
				return string.Format("HitPoints: {0} D: {1}", HitPoints, Damage);
			}
		}

		private class Wizard {
			private int hitpoints_init = 0;
			private int mana_init = 0;
			private int mana = 0;
			private int mana_spent = 0;

			public virtual int HitPoints { get; set; }

			public virtual int ManaSpent
			{
				get { return mana_spent; }
			}

			public virtual int Mana
			{
				get { return mana; }
			}

			public void Reset() {
				HitPoints = hitpoints_init;
				mana = mana_init;
				mana_spent = 0;
			}

			public Wizard(int hitpoints, int mana) {
				hitpoints_init = hitpoints;
				mana_init = mana;
				Reset();
			}

			public Wizard(Wizard o) {
				mana = o.mana;
				mana_spent = o.mana_spent;
				mana_init = o.mana_init;
				hitpoints_init = o.hitpoints_init;
				HitPoints = o.HitPoints;
			}

			public void CastSpell(int cost) {
				mana -= cost;
				mana_spent += cost;
			}

			public void ReplenishMana(int replenish) {
				mana += replenish;
			}
		}

		private class Spell {
			public string Name { get; set; }
			public int Cost { get; set; }

			public int Armor { get; set; }

			public int Durability { get; set; }

			public int Damage { get; set; }

			public int Heal { get; set; }

			public int Replenish { get; set; }

			public Spell(string name, int cost, int armor, int durability, int damage, int heal, int replenish) {
				Name = name;
				Cost = cost;
				Armor = armor;
				Durability = durability;
				Damage = damage;
				Heal = heal;
				Replenish = replenish;
			}

			public override string ToString() {
				return Name;
			}
		}

		private static Spell MagicMissile = new Spell("MagicMissile", 53, 0, 0, 4, 0, 0);
		private static Spell Drain = new Spell("Drain", 73, 0, 0, 2, 2, 0);
		private static Spell Shield = new Spell("Shield", 113, 7, 6, 0, 0, 0);
		private static Spell Poison = new Spell("Poison", 173, 0, 6, 3, 0, 0);
		private static Spell Recharge = new Spell("Recharge", 229, 0, 5, 0, 0, 101);

		#endregion

		static void Main(string[] args) {
			int result_part1 = 0, result_part2 = 0;
			Fighter boss;
			Wizard me;
			Dictionary<Spell, int> active_effects = new Dictionary<Spell, int>();
			Dictionary<List<Spell>, Tuple<int, bool>> fight_log = new Dictionary<List<Spell>, Tuple<int, bool>>();

			Console.WriteLine("=== Advent of Code - day 22 ====");

			if (!System.IO.File.Exists(input_path)) {
				Console.WriteLine("input file not found");
				return;
			}

			//*
			#region test1

			bool res, lost;
			boss = new Fighter(13, 8);
			me = new Wizard(10, 250);
			active_effects.Clear();

			res = PerformTurn(me, boss, false, Poison, active_effects, true, out lost);
			res = PerformTurn(me, boss, false, null, active_effects, true, out lost);

			res = PerformTurn(me, boss, false, MagicMissile, active_effects, true, out lost);
			res = PerformTurn(me, boss, false, null, active_effects, true, out lost);

			#endregion

			#region test2

			boss = new Fighter(14, 8);
			me = new Wizard(10, 250);
			active_effects.Clear();

			res = PerformTurn(me, boss, false, Recharge, active_effects, true, out lost);
			res = PerformTurn(me, boss, false, null, active_effects, true, out lost);

			res = PerformTurn(me, boss, false, Shield, active_effects, true, out lost);
			res = PerformTurn(me, boss, false, null, active_effects, true, out lost);

			res = PerformTurn(me, boss, false, Drain, active_effects, true, out lost);
			res = PerformTurn(me, boss, false, null, active_effects, true, out lost);

			res = PerformTurn(me, boss, false, Poison, active_effects, true, out lost);
			res = PerformTurn(me, boss, false, null, active_effects, true, out lost);

			res = PerformTurn(me, boss, false, MagicMissile, active_effects, true, out lost);
			res = PerformTurn(me, boss, false, null, active_effects, true, out lost);

			#endregion
			//*/


			#region part 1

			Console.WriteLine("--- part 1 ---");

			boss = new Fighter(input_path);
			me = new Wizard(50, 500);

			active_effects.Clear();
			fight_log.Clear();
			result_part1 = int.MaxValue;

			SimulateFights(me, boss, false, active_effects, ref result_part1);

			Console.WriteLine("Result is {0}", result_part1);

			#endregion

			#region part 2

			Console.WriteLine("--- part 2 ---");

			boss = new Fighter(input_path);
			me = new Wizard(50, 500);

			active_effects.Clear();
			fight_log.Clear();
			result_part2 = int.MaxValue;

			SimulateFights(me, boss, true, active_effects, ref result_part2);

			Console.WriteLine("Result is {0}", result_part2);

			#endregion
		}

		private static void SimulateFights(Wizard me, Fighter boss, bool hard, Dictionary<Spell, int> active_effects, ref int min_mana_spent) {
			bool boss_won, result;
			List<Spell> spells = new List<Spell>(new Spell[] { MagicMissile, Drain, Shield, Poison, Recharge });

			// prepare available spells
			foreach (Spell item in active_effects.Keys) {
				if (active_effects[item] > 1) {
					spells.Remove(item);
				}
			}
			for (int i = 0; i < spells.Count; i++) {
				Wizard me1 = new Wizard(me);
				Fighter boss1 = new Fighter(boss);
				Dictionary<Spell, int> act_eff = new Dictionary<Spell, int>(active_effects);

				result = PerformTurn(me1, boss1, hard, spells[i], act_eff, false, out boss_won);
				if (result) {
					if (!boss_won) {
						if (me1.ManaSpent < min_mana_spent) {
							min_mana_spent = me1.ManaSpent;
						}
					}
				}
				else {
					result = PerformTurn(me1, boss1, hard, null, act_eff, false, out boss_won);

					if (result) {
						if (!boss_won) {
							if (me1.ManaSpent < min_mana_spent) {
								min_mana_spent = me1.ManaSpent;
							}
						}
					}
					else {
						if (me1.ManaSpent < min_mana_spent) {
							SimulateFights(me1, boss1, hard, act_eff, ref min_mana_spent);
						}
					}
				}
			}

		}

		private static bool PerformTurn(Wizard me, Fighter boss, bool hard, Spell spell, Dictionary<Spell, int> active_effects, bool print, out bool boss_won) {
			bool finished = false;
			int armor = 0, hit_damage = 0;

			boss_won = false;

			if (active_effects.ContainsKey(Shield)) {
				armor = Shield.Armor;
			}

			if (print) {
				Console.WriteLine();

				if (spell == null) {
					Console.WriteLine("-- Boss turn --");
				}
				else {
					Console.WriteLine("--Player turn--");
				}
				Console.WriteLine("- Player has {0} hit points, {1} armor, {2} mana", me.HitPoints, armor, me.Mana);
				Console.WriteLine("- Boss has {0} hit points", boss.HitPoints);
			}

			if (hard) {
				if (spell != null) {
					me.HitPoints--;

					if (print) {
						Console.WriteLine("Due hard mode player loses 1 hit point.");
						Console.WriteLine("- Now player has {0} hit points, {1} armor, {2} mana", me.HitPoints, armor, me.Mana);
					}
					if (me.HitPoints <= 0) {
						if (print) {
							Console.WriteLine("This kills the player and boss wins");
						}
						boss_won = true;
						return true;
					}
				}
			}

			List<Spell> tmp = new List<Spell>(active_effects.Keys);
			foreach (Spell effect in tmp) {
				if (effect.Equals(Shield)) {
					active_effects[effect]--;
					if (print) {
						Console.WriteLine("Shield's timer is now {0}.", active_effects[effect]);
					}
					if (active_effects[effect].Equals(0)) {
						active_effects.Remove(effect);
						if (print) {
							Console.WriteLine("Shield wears off, decreasing armor by {0}.", effect.Armor);
						}
					}
				}
				else if (effect.Equals(Poison)) {
					active_effects[effect]--;
					boss.HitPoints -= effect.Damage;

					if (print) {
						Console.Write("Poison deals {0} damage; ", effect.Damage);
					}
					if (boss.HitPoints <= 0) {
						if (print) {
							Console.WriteLine("This kills the boss, and the player wins.");
						}
						return true;
					}
					else {
						if (print) {
							Console.WriteLine("its timer is now {0}.", active_effects[effect]);
						}
						if (active_effects[effect].Equals(0)) {
							active_effects.Remove(effect);
							if (print) {
								Console.WriteLine("Poison wears off.");
							}
						}
					}
				}
				else if (effect.Equals(Recharge)) {
					active_effects[effect]--;
					me.ReplenishMana(effect.Replenish);
					if (print) {
						Console.WriteLine("Recharge provides {0} mana; its timer is now {1}.", effect.Replenish, active_effects[effect]);
					}
					if (active_effects[effect].Equals(0)) {
						active_effects.Remove(effect);
						if (print) {
							Console.WriteLine("Recharge wears off");
						}
					}
				}
				else {
					throw new InvalidDataException(string.Format("Invalid effect '{0}'", effect.ToString()));
				}
			}

			if (spell == null) {
				if (armor > 0) {
					hit_damage = boss.Damage - armor;
					if (hit_damage < 1) {
						hit_damage = 1;
					}
					if (print) {
						Console.WriteLine("Boss attacks for {0} - {1} => {2} damage!", boss.Damage, armor, hit_damage);
					}
				}
				else {
					hit_damage = boss.Damage;
					if (print) {
						Console.WriteLine("Boss attacks for {0} damage!", hit_damage);
					}
				}
				me.HitPoints -= hit_damage;
				if (me.HitPoints <= 0) {
					if (print) {
						Console.WriteLine("This kills the player and boss wins");
					}
					boss_won = true;
					return true;
				}
			}
			else {
				me.CastSpell(spell.Cost);
				if (me.Mana <= 0) {
					boss_won = true;
					return true;
				}

				if (spell.Equals(MagicMissile)) {
					if (print) {
						Console.WriteLine("Player casts Magic Missile, dealing {0} damage.", spell.Damage);
					}
					boss.HitPoints -= spell.Damage;
					if (boss.HitPoints <= 0) {
						if (print) {
							Console.WriteLine("This kills the boss, and the player wins.");
						}
						return true;
					}
				}
				else if (spell.Equals(Drain)) {
					if (print) {
						Console.WriteLine("Player casts Drain, dealing {0} damage, and healing {1} hit points.", spell.Damage, spell.Heal);
					}
					boss.HitPoints -= spell.Damage;
					me.HitPoints += spell.Heal;
					if (boss.HitPoints <= 0) {
						if (print) {
							Console.WriteLine("This kills the boss, and the player wins.");
						}
						return true;
					}
				}
				else if (spell.Equals(Shield)) {
					if (print) {
						Console.WriteLine("Player casts Shield, increasing armor by {0}.", spell.Armor);
					}
					active_effects.Add(spell, spell.Durability);
				}
				else if (spell.Equals(Poison)) {
					if (print) {
						Console.WriteLine("Player casts Poison.");
					}
					active_effects.Add(spell, spell.Durability);
				}
				else if (spell.Equals(Recharge)) {
					if (print) {
						Console.WriteLine("Player casts Recharge.");
					}
					active_effects.Add(spell, spell.Durability);
				}
				else {
					throw new InvalidDataException(string.Format("Invalid spell '{0}'", spell.ToString()));
				}
			}

			return finished;
		}

	}
}
