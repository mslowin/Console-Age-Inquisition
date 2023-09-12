using System;

namespace ConsoleAgeInquisition
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Welcome to Console Age: Inquisition");
			Console.WriteLine("Please let me know what do you want me to do:");
			Console.WriteLine("1. Start a new game");
			Console.WriteLine("2. Load game");
			Console.WriteLine("3. Exit");
			Console.WriteLine("Press 1, 2 or 3");

			string? choice = Console.ReadLine();

			Console.WriteLine($"Choice is {choice}");
		}
	}
}