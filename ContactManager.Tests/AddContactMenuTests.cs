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
        console.Input.Enqueue("1");
        console.Input.Enqueue("Elvis");
        console.Input.Enqueue("Elvis@gmail.com");
        console.Input.Enqueue("043238291");
        console.Input.Enqueue("q");
        menu.Run();
        List<string> expected =
            // Initieel menu
            [ "Make your choice:"
            , "1. Add new contact:"
            , "2. Search contact:"
            , "3. Update contact:"
            , "4. Delete contact:"
            , "5. Show all contacts:"
            , "q. Exit"
            // Na keuze '1'
            , "Add Name:"
            , "Add Email:"
            , "Add PhoneNumber:"
            // Na toevoegen          
            , "Contact added: Elvis"
            , "Email added:Elvis@gmail.com"
            , "PhoneNumber added:043238291"   
            // Turtles all the way down
            ,"Make your choice:"
            , "1. Add new contact:"
            , "2. Search contact:"
            , "3. Update contact:"
            , "4. Delete contact:"
            , "5. Show all contacts:"
            , "q. Exit"
            ];
        Assert.Equal(expected, console.Output);
        var contact = repository.GetAll()[0];
        Assert.Equal(1, contact.Id);
        Assert.Contains("Elvis", contact.Name);
    }
}