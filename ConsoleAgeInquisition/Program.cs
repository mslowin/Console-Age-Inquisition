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

			var choice = Console.ReadLine();

			Console.WriteLine($"Choice is {choice}");


			// Użytkownik zostanie powitany
			// Dostanie możliwość wyboru akcji, które będą wyświetlone w menu
			//// a. zaplanowanie tego jak powinny być podzielone klasy (character/postać (życie, siła, imię, rasa, typ, armor, lista itemków),
			//// bohater (dziedziczy po character + metody odpowiednie), przeciwnicy (dziedziczy po character + metody odpowiednie),
			//// armor (czyli różnego rodzaju zbroje z różnymi parametrami), itemki (pierścienie itp, dodające zdrwoie, czy atak),
			//// cała mapa (lista pokoi), pokój (lista wrogów, bohater?), podstawowe typy postaci (mainCharacter, enemy, boss),
			//// dodatkowe typy (barbarian, knight, golem, ghul itp.))
			//// a. Stworzenie nowej gry
			////// a1. PRzygotowanie menu gry
			////// a2. Podanie szczegółów takich jak poziom trudności, nazwa save'u
			////// a3. Akceptacja wyborów
			//// b. Stworzenie swojej postaci
			////// b1. Przygotowanie menu tworzenia postaci
			////// b2. Imię bohatera, wybór klasy (opisy czym się harakteryzują), przypisanie początkowych zasobów takich jak życie, atak, mana
			////// b3. Akceptacja wyborów
			//// c. Dodanie instrukcji takich jak atak, leczenie, przejście do innego pokoju (jako komendy w konsoli np. "/attack" lub "/move 1" (drzwi numer 1))
			////// c1. przygotowanie jakiegoś helpa, który wypisywałby i opisywał te wszystkie funkcjonalności "/help"
			//// d. Wygenerowanie pierwszego dungeonu, w nim pokoi, itemków, wrogów i bossa (na początek hardcodced)
			////// d1. Stworzenie odpowiednich klas
			////// d2. Rozpisanie jak pierwszy dungeon miałby wyglądać i zhardcodować pokoje, przejścia, miejsca przeciwników, miejsce bossa, loot
			////// d3. Przetestować przechodzenie między pokojami
			//// e. Dodanie modelu walki
			//// f. Zapisanie stanu gry (to później do jakiegoś pliku txt/json albo w bazie później)
		}
	}
}