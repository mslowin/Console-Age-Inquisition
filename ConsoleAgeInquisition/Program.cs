using ConsoleAgeInquisition.Services;

namespace ConsoleAgeInquisition;

internal class Program
{
    private static void Main(string[] args)
    {
        GameManagementService.Start();

        //// a. wybór klasy (opisy czym się harakteryzują)
        //// b. błędne komunikaty jeśli wpisze się np > save . (brakuje jakiegośreturna?)
        //// c. wyrzucić hardcodowane drzwi na północ z ostatniego pokoju
        //// d. Jakieśsimple UI (na pewno wyświetlanie statów, może jakaś mapka?, czy coś)
        //// d. ViewService zamiast indeksu savea mogłaby zwracać w sumie już deserializowany obiekt Game
    }
}