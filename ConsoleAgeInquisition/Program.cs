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

            GameManagementService.Start();

            // Użytkownik zostanie powitany
            // Dostanie możliwość wyboru akcji, które będą wyświetlone w menu
            ////// b2. wybór klasy (opisy czym się harakteryzują)
            //// c. Dodanie instrukcji takich jak atak, leczenie, przejście do innego pokoju (jako komendy w konsoli np. "/attack" lub "/move 1" (drzwi numer 1))
            //// d. Wygenerowanie pierwszego dungeonu, w nim pokoi, itemków, wrogów i bossa (na początek hardcodced)
            ////// d3. Przetestować przechodzenie między pokojami
            //// e. Dodanie modelu walki
        }
    }
}