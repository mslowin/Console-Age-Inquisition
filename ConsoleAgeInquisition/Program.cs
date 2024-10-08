using ConsoleAgeInquisition.Services;

namespace ConsoleAgeInquisition
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine(@"
 ________  ________  ________   ________  ________  ___       _______           ________  ________  _______
|\   ____\|\   __  \|\   ___  \|\   ____\|\   __  \|\  \     |\  ___ \         |\   __  \|\   ____\|\  ___ \
\ \  \___|\ \  \|\  \ \  \\ \  \ \  \___|\ \  \|\  \ \  \    \ \   __/|        \ \  \|\  \ \  \___|\ \   __/|
 \ \  \    \ \  \\\  \ \  \\ \  \ \_____  \ \  \\\  \ \  \    \ \  \_|/__       \ \   __  \ \  \  __\ \  \_|/__
  \ \  \____\ \  \\\  \ \  \\ \  \|____|\  \ \  \\\  \ \  \____\ \  \_|\ \       \ \  \ \  \ \  \|\  \ \  \_|\ \
   \ \_______\ \_______\ \__\\ \__\____\_\  \ \_______\ \_______\ \_______\       \ \__\ \__\ \_______\ \_______\
    \|_______|\|_______|\|__| \|__|\_________\|_______|\|_______|\|_______|        \|__|\|__|\|_______|\|_______|
                                  \|_________|
 ___  ________   ________  ___  ___  ___  ________  ___  _________  ___  ________  ________
|\  \|\   ___  \|\   __  \|\  \|\  \|\  \|\   ____\|\  \|\___   ___\\  \|\   __  \|\   ___  \
\ \  \ \  \\ \  \ \  \|\  \ \  \\\  \ \  \ \  \___|\ \  \|___ \  \_\ \  \ \  \|\  \ \  \\ \  \
 \ \  \ \  \\ \  \ \  \\\  \ \  \\\  \ \  \ \_____  \ \  \   \ \  \ \ \  \ \  \\\  \ \  \\ \  \
  \ \  \ \  \\ \  \ \  \\\  \ \  \\\  \ \  \|____|\  \ \  \   \ \  \ \ \  \ \  \\\  \ \  \\ \  \
   \ \__\ \__\\ \__\ \_____  \ \_______\ \__\____\_\  \ \__\   \ \__\ \ \__\ \_______\ \__\\ \__\
    \|__|\|__| \|__|\|___| \__\|_______|\|__|\_________\|__|    \|__|  \|__|\|_______|\|__| \|__|
                          \|__|             \|_________|
            ");

            ViewsService.HandleMainMenu();

            // Użytkownik zostanie powitany
            // Dostanie możliwość wyboru akcji, które będą wyświetlone w menu
            //// a. bohater (dziedziczy po character + metody odpowiednie), przeciwnicy (dziedziczy po character + metody odpowiednie),
            //// dodatkowe typy (barbarian, knight, golem, ghul itp.))
            //// a. Stworzenie nowej gry
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