
using ContactManager.Core;

public class MenuTest
{
    // Create the service with a temporary in-memory data store
    private ContactService service = new(new InMemoryContactRepository());
    // Fake console to simulate user input/output
    private FakeConsole console = new();

// Declare the menu object
    private Menu menu;

    public MenuTest()
    { // Create the menu using the fake console and service
        menu = new Menu(console, service);
    }

    [Fact]
    public void Menu_Q_Exits()
    {// Simulate the user typing "q"
        console.Input.Enqueue("q");
        // Run the menu and check it returns 0 (exit)
        Assert.Equal(0, menu.Run());
        // Check that the menu showed the "exit" option
        Assert.Contains("q. Exit", console.Output);
    }
}