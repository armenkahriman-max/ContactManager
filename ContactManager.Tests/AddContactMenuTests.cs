using Xunit;
using ContactManager.Core;

public class AddContactMenuTests
{
    // Create a temporary in-memory data store for contacts
    private readonly InMemoryContactRepository repository = new();
    // Declare the service (will handle contact logic)
    private readonly ContactService service;
    // Fake console to simulate input/output
    private readonly FakeConsole console = new();
    // Declare the menu
    private readonly Menu menu;

    public AddContactMenuTests()
    {// Create the service using the repository
        service = new ContactService(repository);
        // Create the menu with the fake console and service
        menu = new Menu(console, service);
    }

    [Fact]
    public void Menu_AddContact_Flow()
    {
        console.Input.Enqueue("1");     // pick option
        console.Input.Enqueue("Elvis"); // input name
        console.Input.Enqueue("q");     // exit the loop.
        menu.Run();
        List<string> expected =
            // Initieel menu
            [ "1. Contact Toevoegen"
            , "q. Exit"
            , "Maak uw keuze:"
            // Na keuze '1'
            , "Voer een naam in: "  
            // Na toevoegen          
            , "Contact toegevoegd: Elvis"     
            // Turtles all the way down
            , "1. Contact Toevoegen"
            , "q. Exit"
            , "Maak uw keuze:"
            ];
        Assert.Equal(expected, console.Output);
        var contact = repository.GetAll()[0];
        Assert.Equal(1, contact.Id);
        Assert.Contains("Elvis", contact.Name);
    }
}