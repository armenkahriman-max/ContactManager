namespace ContactManager.Core;

// =======================
// ABSTRACTION (INTERFACE)
// =======================
// Defines what a console MUST be able to do
public interface IConsole
{
    void WriteLine(string message); // output text
    public void Write(string message);
    string? ReadLine();             // input text
}













