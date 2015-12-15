using System;
using System.ComponentModel.Design.Serialization;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Day07 {
	enum Operation {
		And,
		Or,
		Not,
		ShiftLeft,
		ShiftRight,
		Assign
	}

	class instruction {
		public string operand1 = null;
		public string operand2 = null;
		public Operation operation;
		public string result = null;
		public ushort const1 = 0;
		public ushort const2 = 0;

		public instruction() {
			operand1 = null;
			operand2 = null;
			const1 = 0;
			const2 = 0;
			operation = Operation.Assign;
			result = null;
		}

		public ushort Equate(ushort op1, ushort op2) {
			ushort result = 0, p1, p2;

			if (operand1 != null) {
				p1 = op1;
			}
			else {
				p1 = const1;
			}
			if (operand2 != null) {
				p2 = op2;
			}
			else {
				p2 = const2;
			}

			switch (operation) {
				case Operation.Assign:
					result = p1;
					break;
				case Operation.And:
					result = (ushort)(p1 & p2);
					break;
				case Operation.Not:
					result = (ushort)(~p1);
					break;
				case Operation.Or:
					result = (ushort)(p1 | p2);
					break;
				case Operation.ShiftLeft:
					result = (ushort)(p1 << p2);
					break;
				case Operation.ShiftRight:
					result = (ushort)(p1 >> p2);
					break;
			}

			return result;
		}

		public override string ToString() {
			//return string.Format("[instruction]");
			string p1 = string.Empty, p2 = string.Empty, res = string.Empty;

			if (operand1 != null) {
				p1 = operand1;
			}
			else {
				p1 = const1.ToString();
			}
			if (operand2 != null) {
				p2 = operand2;
			}
			else {
				p2 = const2.ToString();
			}

			switch (operation) {
				case Operation.Assign:
					res = string.Format("{0} = {1}", result, p1);
					break;
				case Operation.And:
					res = string.Format("{0} = {1} AND {2}", result, p1, p2);
					break;
				case Operation.Not:
					res = string.Format("{0} = NOT {1}", result, p1);
					break;
				case Operation.Or:
					res = string.Format("{0} = {1} OR {2}", result, p1, p2);
					break;
				case Operation.ShiftLeft:
					res = string.Format("{0} = {1} LSHIFT {2}", result, p1, p2);
					break;
				case Operation.ShiftRight:
					res = string.Format("{0} = {1} RSHIFT {2}", result, p1, p2);
					break;
			}
			return res;
		}
	}

	class MainClass {
		private const string input_path = "./input.txt";
		private const string divider = " -> ";
		private const string op_and = "AND";
		private const string op_or = "OR";
		private const string op_not = "NOT";
		private const string op_shift_left = "LSHIFT";
		private const string op_shift_right = "RSHIFT";

		public static void Main(string[] args) {
			string left, right, target;
			string[] input, parts;
			ushort val, result_part1, result_part2;
			Dictionary<string, ushort> values = new Dictionary<string, ushort>(), init_values = new Dictionary<string, ushort>(); ;
			Dictionary<string, instruction> instructions = new Dictionary<string, instruction>();
			Dictionary<string, instruction> way = new Dictionary<string, instruction>(), way_done = new Dictionary<string, instruction>();
			List<string> level = new List<string>();
			instruction curr_inst;
			string search_for;

			Console.WriteLine("=== Advent of Code - day 7 ====");

			if (!System.IO.File.Exists(input_path)) {
				Console.WriteLine("input file not found");
				return;
			}

			input = System.IO.File.ReadAllLines(input_path);

			#region part 1

			Console.WriteLine("--- part 1 ---");

			#region instruction parsing

			for (int i = 0; i < input.Length; i++) {
				if (input[i].Contains(divider)) {
					parts = input[i].Split(new string[] { divider }, StringSplitOptions.RemoveEmptyEntries);
					if (!parts.Length.Equals(2)) {
						Console.WriteLine("Unrecognized instruction at line {0}", i + 1);
						return;
					}

					left = parts[0].Trim();
					right = parts[1].Trim();
					parts = left.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

					if ((left.Contains(op_and) ||
						 left.Contains(op_or) ||
						 left.Contains(op_shift_left) ||
						 left.Contains(op_shift_right)) &&
						 parts.Length.Equals(3)) {
						curr_inst = new instruction();
						if (parts[1].Equals(op_and)) {
							curr_inst.operation = Operation.And;
						}
						else if (parts[1].Equals(op_or)) {
							curr_inst.operation = Operation.Or;
						}
						else if (parts[1].Equals(op_shift_right)) {
							curr_inst.operation = Operation.ShiftRight;
						}
						else if (parts[1].Equals(op_shift_left)) {
							curr_inst.operation = Operation.ShiftLeft;
						}
						else {
							Console.WriteLine("Unrecognized format of two-operand instruction", i + 1);
							return;
						}
						if (ushort.TryParse(parts[0], out val)) {
							curr_inst.const1 = val;
						}
						else {
							curr_inst.operand1 = parts[0];
						}
						if (ushort.TryParse(parts[2], out val)) {
							curr_inst.const2 = val;
						}
						else {
							curr_inst.operand2 = parts[2];
						}
						curr_inst.result = right;
						instructions.Add(right, curr_inst);
					}
					else if (left.Contains(op_not) && parts.Length.Equals(2)) {
						if (parts[0].Equals(op_not)) {
							curr_inst = new instruction();
							curr_inst.operation = Operation.Not;
							curr_inst.result = right;
							if (ushort.TryParse(parts[1], out val)) {
								curr_inst.const1 = val;
							}
							else {
								curr_inst.operand1 = parts[1];
							}
							instructions.Add(right, curr_inst);
						}
						else {
							Console.WriteLine("Unrecognized format of NOT instruction at line {0}", i);
							return;
						}
					}
					else if (ushort.TryParse(left, out val)) {
						values.Add(right, val);
					}
					else if (left.Length > 0) {
						curr_inst = new instruction();
						curr_inst.operand1 = left;
						curr_inst.operation = Operation.Assign;
						curr_inst.result = right;
						instructions.Add(right, curr_inst);
					}
					else {
						Console.WriteLine("Unknown instruction at line {0}", i + 1);
						return;
					}
				}
				else {
					Console.WriteLine("Invalid instruction at line {0}", i + 1);
					return;
				}
			}

			#endregion

			#region reverse parse of instructions to result wire "a"

			way.Clear();
			level.Clear();
			search_for = "a";

			level.Add(search_for);
			while (level.Count > 0) {
				target = level[0];
				level.RemoveAt(0);

				if (instructions.ContainsKey(target)) {
					if (!way.ContainsKey(target)) {
						way.Add(target, instructions[target]);
						if (instructions[target].operand1 != null) {
							level.Add(instructions[target].operand1);
						}
						if (instructions[target].operand2 != null) {
							level.Add(instructions[target].operand2);
						}
					}
				}
				else if (!values.ContainsKey(target)) {
					Console.WriteLine("Instruction or value for {0} not found", target);
					return;
				}
			}

			#endregion

			#region solve all instructions

			init_values.Clear();
			foreach (string item in values.Keys) {
				init_values.Add(item, values[item]);
			}

			SolveInstructions(values, way, way_done, search_for);

			#endregion

			result_part1 = values[search_for];
			Console.WriteLine("Result is {0}", result_part1);

			#endregion

			#region part 2

			Console.WriteLine("--- part 2 ---");
			way = way_done;
			way_done = new Dictionary<string, instruction>();
			values.Clear();

			foreach (string item in init_values.Keys) {
				values.Add(item, init_values[item]);
			}

			if (values.ContainsKey("b")){
				values["b"] = result_part1;
			}

			SolveInstructions(values, way, way_done, search_for);

			result_part2 = values[search_for];

			Console.WriteLine("Result is {0}", result_part2);
			#endregion
		}

		private static void SolveInstructions(Dictionary<string, ushort> values, Dictionary<string, instruction> way, Dictionary<string, instruction> way_done, string search_for) {
			while (!values.ContainsKey(search_for)) {
				bool found = false;
				string item_found = string.Empty;
				ushort value = 0, v1, v2;

				foreach (string item in way.Keys) {
					v1 = 0;
					v2 = 0;
					if (way[item].operand1 != null) {
						if (values.ContainsKey(way[item].operand1)) {
							v1 = values[way[item].operand1];
						}
						else {
							continue;
						}
					}
					if (way[item].operand2 != null) {
						if (values.ContainsKey(way[item].operand2)) {
							v2 = values[way[item].operand2];
						}
						else {
							continue;
						}
					}

					value = way[item].Equate(v1, v2);
					found = true;
					item_found = item;
					break;
				}
				if (found) {
					way_done.Add(item_found, way[item_found]);
					way.Remove(item_found);
					values.Add(item_found, value);
					found = false;
				}
				else {
					throw new FormatException("Unable to equate the instructions");
				}
			}
		}
	}
}
