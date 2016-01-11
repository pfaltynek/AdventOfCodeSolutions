using System;
using System.Collections.Generic;
using System.IO;

namespace Day23 {
	class Program {
		private const string input_path = "./input.txt";

		private enum InstructionType {
			hlf,
			tpl,
			inc,
			jmp,
			jie,
			jio
		}

		private struct code_line {
			public InstructionType instruction;
			public char register;
			public int value;

			public override string ToString() {
				return string.Format("{0} {1} {2}", instruction.ToString(), register, value);
			}
		}

		static void Main(string[] args) {
			int result_part1 = 0, result_part2 = 0, pc = 0;
			string[] lines, parts;
			List<code_line> code = new List<code_line>();
			uint a = 0, b = 0;

			Console.WriteLine("=== Advent of Code - day 23 ====");

			if (!System.IO.File.Exists(input_path)) {
				Console.WriteLine("input file not found");
				return;
			}

			lines = System.IO.File.ReadAllLines(input_path);
			for (int i = 0; i < lines.Length; i++) {
				parts = lines[i].Split(" ,".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
				if ((parts.Length < 2) || (parts.Length > 3)) {
					Console.WriteLine("Invalid instruction at line {0}", i + 1);
					return;
				}
				code_line cl = new code_line();
				cl.register = ' ';
				cl.value = 0;

				switch (parts[0]) {
					case "hlf":
						cl.instruction = InstructionType.hlf;
						if (parts.Length != 2) {
							Console.WriteLine("Invalid format of instruction 'hlf' at line {0}", i + 1);
							return;
						}
						else {
							switch (parts[1]) {
								case "a":
									cl.register = 'a';
									break;
								case "b":
									cl.register = 'b';
									break;
								default:
									Console.WriteLine("Invalid register in instruction at line {0}", i + 1);
									return;
							}
						}
						break;
					case "tpl":
						cl.instruction = InstructionType.tpl;
						if (parts.Length != 2) {
							Console.WriteLine("Invalid format of instruction 'tpl' at line {0}", i + 1);
							return;
						}
						else {
							switch (parts[1]) {
								case "a":
									cl.register = 'a';
									break;
								case "b":
									cl.register = 'b';
									break;
								default:
									Console.WriteLine("Invalid register in instruction at line {0}", i + 1);
									return;
							}
						}
						break;
					case "inc":
						cl.instruction = InstructionType.inc;
						if (parts.Length != 2) {
							Console.WriteLine("Invalid format of instruction 'inc' at line {0}", i + 1);
							return;
						}
						else {
							switch (parts[1]) {
								case "a":
									cl.register = 'a';
									break;
								case "b":
									cl.register = 'b';
									break;
								default:
									Console.WriteLine("Invalid register in instruction at line {0}", i + 1);
									return;
							}
						}
						break;
					case "jmp":
						cl.instruction = InstructionType.jmp;
						if (parts.Length != 2) {
							Console.WriteLine("Invalid format of instruction 'jmp' at line {0}", i + 1);
							return;
						}
						else {
							if (!int.TryParse(parts[1], out cl.value)) {
								Console.WriteLine("Invalid format of value at line {0}", i + 1);
								return;
							}
						}
						break;
					case "jie":
						cl.instruction = InstructionType.jie;
						if (parts.Length != 3) {
							Console.WriteLine("Invalid format of instruction 'jie' at line {0}", i + 1);
							return;
						}
						else {
							switch (parts[1]) {
								case "a":
									cl.register = 'a';
									break;
								case "b":
									cl.register = 'b';
									break;
								default:
									Console.WriteLine("Invalid register in instruction at line {0}", i + 1);
									return;
							}
							if (!int.TryParse(parts[2], out cl.value)) {
								Console.WriteLine("Invalid format of value at line {0}", i + 1);
								return;
							}
						}
						break;
					case "jio":
						cl.instruction = InstructionType.jio;
						if (parts.Length != 3) {
							Console.WriteLine("Invalid format of instruction 'jio' at line {0}", i + 1);
							return;
						}
						else {
							switch (parts[1]) {
								case "a":
									cl.register = 'a';
									break;
								case "b":
									cl.register = 'b';
									break;
								default:
									Console.WriteLine("Invalid register in instruction at line {0}", i + 1);
									return;
							}
							if (!int.TryParse(parts[2], out cl.value)) {
								Console.WriteLine("Invalid format of value at line {0}", i + 1);
								return;
							}
						}
						break;
					default:
						Console.WriteLine("Instruction not recognized at line {0}", i + 1);
						return;
				}
				code.Add(cl);
			}


			#region part 1

			Console.WriteLine("--- part 1 ---");


			a = 0;
			b = 0;
			result_part1 = (int)TraceCode(a, b, code);

			Console.WriteLine("Result is {0}", result_part1);

			#endregion

			#region part 2

			Console.WriteLine("--- part 2 ---");

			a = 1;
			b = 0;
			result_part2 = (int)TraceCode(a, b, code);

			Console.WriteLine("Result is {0}", result_part2);

			#endregion
		}

		private static uint TraceCode(uint a, uint b, List<code_line> code) {
			int pc = 0;
			while ((pc >= 0) && (pc < code.Count)) {
				switch (code[pc].instruction) {
					case InstructionType.hlf:
						switch (code[pc].register) {
							case 'a':
								a = a / 2;
								break;
							case 'b':
								b = b / 2;
								break;
						}
						pc++;
						break;
					case InstructionType.inc:
						switch (code[pc].register) {
							case 'a':
								a++;
								break;
							case 'b':
								b++;
								break;
						}
						pc++;
						break;
					case InstructionType.tpl:
						switch (code[pc].register) {
							case 'a':
								a = a * 3;
								break;
							case 'b':
								b = b * 3;
								break;
						}
						pc++;
						break;
					case InstructionType.jie:
						switch (code[pc].register) {
							case 'a':
								if ((a % 2).Equals(0)) {
									pc += code[pc].value;
								}
								else {
									pc++;
								}
								break;
							case 'b':
								if ((b % 2).Equals(0)) {
									pc += code[pc].value;
								}
								else {
									pc++;
								}
								break;
						}
						break;
					case InstructionType.jio:
						switch (code[pc].register) {
							case 'a':
								if (a.Equals(1)) {
									pc += code[pc].value;
								}
								else {
									pc++;
								}
								break;
							case 'b':
								if (b.Equals(1)) {
									pc += code[pc].value;
								}
								else {
									pc++;
								}
								break;
						}
						break;
					case InstructionType.jmp:
						pc += code[pc].value;
						break;
					default:
						throw new InvalidDataException(string.Format("Unknown instruction at {0} [{1}]", pc, code[pc].ToString()));
				}
			}
			return b;
		}
	}
}