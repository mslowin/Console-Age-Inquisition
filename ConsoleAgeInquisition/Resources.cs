namespace ConsoleAgeInquisition;

public static class Resources
{
    public static string GetGameSavesFolderPath()
    {
        return Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../GameSaves"));
    }
}
