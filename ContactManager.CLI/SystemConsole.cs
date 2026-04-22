using ContactManager.Core;

namespace ContactManager.CLI;

// =======================
// REAL CONSOLE IMPLEMENTATION
// =======================
// Uses actual system console (production version)
public class SystemConsole : IConsole
{
    // This method sends text to the real console (screen)
    // It is used for OUTPUT (showing messages to the user)
    public void WriteLine(string message)
    {
        Console.WriteLine(message);
        // Console.WriteLine = built-in C# method that prints text
    }
    // This method reads input from the user (keyboard)
    // It is used for INPUT (what the user types)
    public string? ReadLine()
    {
        // Returns what the user typed, or null if nothing was entered
        return Console.ReadLine();
    }
}













