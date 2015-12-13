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

		public override string ToString() {
			//return string.Format("[instruction]");
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
			ushort val;
			int position;
			Dictionary<string, ushort> values = new Dictionary<string, ushort>();
			List<instruction> instructions = new List<instruction>();
			List<instruction> way = new List<instruction>();
			List <string> level = new List<string>();
			instruction curr_inst;

			Console.WriteLine("=== Advent of Code - day 7 ====");

			if(!System.IO.File.Exists(input_path)) {
				Console.WriteLine("input file not found");
				return;
			}

			input = System.IO.File.ReadAllLines(input_path);
			position = 0;

			#region part 1

			Console.WriteLine("--- part 1 ---");

			#region instruction parsing

			for(int i = 0; i < input.Length; i++) {
				if(input[i].Contains(divider)) {
					parts = input[i].Split(new string[]{ divider }, StringSplitOptions.RemoveEmptyEntries);
					if(!parts.Length.Equals(2)) {
						Console.WriteLine("Unrecognized instruction at line {0}", i + 1);
						return;
					}

					left = parts[0].Trim();
					right = parts[1].Trim();
					parts = left.Split(new char[]{ ' ' }, StringSplitOptions.RemoveEmptyEntries);

					if((left.Contains(op_and) ||
					   left.Contains(op_or) ||
					   left.Contains(op_shift_left) ||
					   left.Contains(op_shift_right)) &&
					   parts.Length.Equals(3)) {
						curr_inst = new instruction();
						if(parts[1].Equals(op_and)) {
							curr_inst.operation = Operation.And;
						} else if(parts[1].Equals(op_or)) {
							curr_inst.operation = Operation.Or;
						} else if(parts[1].Equals(op_shift_right)) {
							curr_inst.operation = Operation.ShiftRight;
						} else if(parts[1].Equals(op_shift_left)) {
							curr_inst.operation = Operation.ShiftLeft;
						} else {
							Console.WriteLine("Unrecognized format of two-operand instruction", i + 1);
							return;
						}
						if(ushort.TryParse(parts[0], out val)) {
							curr_inst.const1 = val;
						} else {
							curr_inst.operand1 = parts[0];
						}
						if(ushort.TryParse(parts[2], out val)) {
							curr_inst.const2 = val;
						} else {
							curr_inst.operand2 = parts[2];
						}
						curr_inst.result = right;
						instructions.Add(curr_inst);
					} else if(left.Contains(op_not) && parts.Length.Equals(2)) {
						if(parts[0].Equals(op_not)) {
							curr_inst = new instruction();
							curr_inst.operation = Operation.Not;
							curr_inst.result = right;
							if(ushort.TryParse(parts[1], out val)) {
								curr_inst.const1 = val;
							} else {
								curr_inst.operand1 = parts[1];
							}
							instructions.Add(curr_inst);
						} else {
							Console.WriteLine("Unrecognized format of NOT instruction at line {0}", i);
							return;
						}
					} else if(ushort.TryParse(left, out val)) {
						values.Add(right, val);
					} else if(left.Length > 0) {
						curr_inst = new instruction();
						curr_inst.operand1 = left;
						curr_inst.operation = Operation.Assign;
						curr_inst.result = right;
						instructions.Add(curr_inst);
					} else {
						Console.WriteLine("Unknown instruction at line {0}", i + 1);
						return;
					}
				} else {
					Console.WriteLine("Invalid instruction at line {0}", i + 1);
					return;
				}
			}

			#endregion

			#region reverse parse of instructions to result wire "a"

			way.Clear();
			position = 0;
			level.Clear();
			level.Add("a");
			while(level.Count > 0) {
				target = level[0];
				level.RemoveAt(0);

				for(int i = 0; i < instructions.Count; i++) {
					if(instructions[i].result.Equals(target)) {
						way.Add(instructions[i]);
						if(instructions[i].operand1 != null) {
							level.Add(instructions[i].operand1);
						}
						if(instructions[i].operand2 != null) {
							level.Add(instructions[i].operand2);
						}
					}
				}
			}
			#endregion

			#endregion

			#region part 2

			Console.WriteLine("--- part 2 ---");

			#endregion
		}
	}
}
