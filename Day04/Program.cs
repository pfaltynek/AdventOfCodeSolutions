using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Day04 {
	class Program {
		private const string input = "bgvyzdsv";
		static void Main(string[] args) {
			int number = 1;
			bool found = false;
			string msg;
			byte[] hash, src;
			MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

			Console.WriteLine("=== Advent of Code - day 3 ====");

			#region part 1

			Console.WriteLine("--- part 1 ---");

			while (!found) {
				msg = string.Format("{0}{1}", input, number);
				src = ASCIIEncoding.ASCII.GetBytes(msg);
				hash = md5.ComputeHash(src);
				if (hash[0].Equals(0) &&
					hash[1].Equals(0) &&
					(hash[2] < 0x10)) {
					found = true;
				}
				else {
					number++;
				}
			}
			Console.WriteLine("Result is {0}", number);

			#endregion

			#region part2
			Console.WriteLine("--- part 2 ---");

			found = false;
			while (!found) {
				msg = string.Format("{0}{1}", input, number);
				src = ASCIIEncoding.ASCII.GetBytes(msg);
				hash = md5.ComputeHash(src);
				if (hash[0].Equals(0) &&
					hash[1].Equals(0) &&
					hash[2].Equals(0)) {
					found = true;
				}
				else {
					number++;
				}
			}
			Console.WriteLine("Result is {0}", number);

			#endregion
		}
	}
}
