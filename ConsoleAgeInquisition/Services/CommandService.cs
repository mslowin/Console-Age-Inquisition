using ConsoleAgeInquisition.Commands;

namespace ConsoleAgeInquisition.Services;

public class CommandService
{
    /// <summary>
    /// Dictionary of command names, interfaces and descriptions
    /// </summary>
    private readonly Dictionary<string, (ICommand command, string description)> _commands = new();

    public void RegisterCommand(string name, ICommand command, string description)
    {
        _commands[name] = (command, description);
    }

    public void ExecuteCommand(string input)
    {
        string[] parts = input.Split(' ', 2);
        string commandName = parts[0].ToLower();
        string[] args = parts.Length > 1 ? parts[1].Split(' ') : Array.Empty<string>();

        if (_commands.ContainsKey(commandName))
        {
            _commands[commandName].command.Execute(args);
        }
        else
        {
            Console.WriteLine("Unknown command. Type 'help' for a list of commands.");
        }
    }

    public void ListCommands()
    {
        Console.WriteLine("Available commands:");
        foreach (var entry in _commands)
        {
            Console.WriteLine($"- {entry.Key}: {entry.Value.description}");
        }
    }
}