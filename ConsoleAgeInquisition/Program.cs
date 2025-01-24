using ConsoleAgeInquisition.Services;

namespace ConsoleAgeInquisition;

internal class Program
{
    private static void Main(string[] args)
    {
        GameManagementService.Start();

        //// a. wybór klasy (opisy czym się harakteryzują)
        //// b. Jakieś simple UI (na pewno wyświetlanie statów, może jakaś mapka?, czy coś)
        //// c. Jak wczytywana jest gra, to nie potrzeba ponownie wyświetlać tekstu powitalnego
        ////    (chyba, że to było związane z tym, że użyłem komendy restart - do sparwdzenia) 
    }
}