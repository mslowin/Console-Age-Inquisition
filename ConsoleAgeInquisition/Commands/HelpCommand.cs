using ConsoleAgeInquisition.Services;

namespace ConsoleAgeInquisition.Commands;

public class HelpCommand : ICommand
{
    private readonly CommandService _commandService;

    public HelpCommand(CommandService commandService)
    {
        _commandService = commandService;
    }

    public void Execute(string[] args)
    {
        _commandService.ListCommands();
    }
}