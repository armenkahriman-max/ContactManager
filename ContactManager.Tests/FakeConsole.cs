
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

    public void Write(string message)
    {
        Output.Add(message);
    }

    public string? ReadLine()
    {// return null (no input left
        if (Input.Count == 0)
        return null;

    return Input.Dequeue();
    } //If there is input available, take and return the next item
}