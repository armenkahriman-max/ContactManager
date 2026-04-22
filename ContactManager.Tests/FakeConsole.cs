using System.Collections.Generic;
using ContactManager.Core;

public class FakeConsole : IConsole
{
    // Stores fake user input
    public Queue<string> Input { get; } = new();
    // Stores what would be printed to the console
    public List<string> Output { get; } = new();

    public void WriteLine(string message)
    {
        Output.Add(message);
    } // Instead of printing, save the message to Output

    public string? ReadLine()
    {// If there is input available, take and return the next item
        return Input.Count > 0 ? Input.Dequeue() : null;
        // Otherwise, return null (no input left
    }
}