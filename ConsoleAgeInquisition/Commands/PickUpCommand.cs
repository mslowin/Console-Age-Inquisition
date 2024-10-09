namespace ConsoleAgeInquisition.Commands;

public class PickUpCommand : ICommand
{
    public void Execute(string[] args)
    {
        string item = args.Length > 0 ? args[0] : "unknown item";
        Console.WriteLine($"Picking up {item}...");

        // TODO: Reszta logiki do podnoszenia
        // trzeba będzie znaleźć item z taką nazwą co jest w item
        // Trzeba będzie też chyba przekazać w ogóle obiekt Room, który w sobie ma wszystko (Hero, Enemies, Items)
        // Trzeba będzie też przekazać obiekt Hero chyba (który może mieć w sobie metodę PickUp)
    }
}