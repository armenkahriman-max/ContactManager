

namespace ContactManager.Core;

// =======================
// MENU (UI LAYER)
// =======================
// Handles user interaction only (NO business logic here)
public class Menu(IConsole console, ContactService service)
{   // This is the console abstraction (input + output)
    // We use IConsole so we can replace it in tests (FakeConsole)
    private readonly IConsole _console = console;
    // This is the service layer (business logic)
    // It handles contacts (add, update, delete, search)
    private readonly ContactService _service = service;

    // Main program loop
    public int Run()
    {
        while (true)
        {
            ShowMenu();

            var input = _console.ReadLine() ?? "";
            if (string.IsNullOrWhiteSpace(input))
                return 0;

            if (!HandleChoice(input))
                return 0;
        }


    }

    // This method ONLY shows text to the user
    // It does NOT take input or do logic
    private void ShowMenu()
    {


        _console.WriteLine("Make your choice:");
        _console.WriteLine("1. Add new contact:");
        _console.WriteLine("2. Search contact:");
        _console.WriteLine("3. Update contact:");
        _console.WriteLine("4. Delete contact:");
        _console.WriteLine("5. Show all contacts:");
        _console.WriteLine("q. Exit");




    }

    private void HandleAddContact()
    {
        _console.WriteLine("Add Name:");

        // Read user input from console
        // If null → replace with empty string to avoid errors
        var name = _console.ReadLine() ?? "";

        _console.WriteLine("Add Email:");
        var email = _console.ReadLine() ?? "";

        _console.WriteLine("Add PhoneNumber:");
        var phone = _console.ReadLine() ?? "";

        if (!string.IsNullOrWhiteSpace(name) &&
        !string.IsNullOrWhiteSpace(email) &&
        !string.IsNullOrWhiteSpace(phone))
        {
            _service.AddContact(name, email, phone);
            _console.WriteLine($"Contact added: {name}");
            _console.WriteLine($"Email added:{email}");
            _console.WriteLine($"PhoneNumber added:{phone}");
        }
        else
        {
            _console.WriteLine("Invalid Input.");
        }


    }

    // Simple explanation User input → switch checks it → correct method runs → continue or exit
    private bool HandleChoice(string choice) // Decide what happens based on user input. HandleChoice = "decides what button does"
    {
        switch (choice)
        {
            case "1":
                HandleAddContact(); // run "add contact" logic
                return true;      // keep program running

            case "q":
                return false; // stops the while loop in Run()
            case "2":
                SearchContactFlow();
                return true;
            case "3":
                UpdateContactFlow();
                return true;
            case "4":
                DeleteContactFlow();
                return true;
            case "5":
                ShowContactsFlow();
                return true;

            default:
                _console.WriteLine("Invalid option."); // error message
                return true; // keep program running
        }

    }

    // =======================
    // FLOWS (USER ACTIONS)
    // =======================
    private void ShowContactsFlow() //Shows all saved contacts
    {
        var contacts = _service.GetContacts(); //Ask service for all contacts

        foreach (var c in contacts) //Loop through them one by one
        {
            _console.WriteLine(c.ToString()); //Print each contact
        }   //Show everything in the contact list”
    }

    private void UpdateContactFlow() //Changes an existing contact
    {
        _console.WriteLine("Enter ID:"); //Ask for ID (which contact to change)
        var input = _console.ReadLine(); //Read the input

        if (int.TryParse(input, out var id)) //try to Convert input to int
        {
            _console.WriteLine("Enter new name:"); // enter name
            var name = _console.ReadLine(); // save it

            if (!string.IsNullOrWhiteSpace(name)) //If name is valid:
            {
                _console.WriteLine("Enter Email:");
                var email = _console.ReadLine() ?? "";

                _console.WriteLine("Enter PhoneNumber:");
                var phone = _console.ReadLine() ?? "";

                _service.UpdateContact(id, name, email, phone);
                _console.WriteLine("Updated!"); //show “Updated!”
            }    //Find contact → replace its name”
        }
    }

    private void DeleteContactFlow() //Removes a contact
    {
        _console.WriteLine("Enter ID:"); //Ask for ID
        var input = _console.ReadLine();

        if (int.TryParse(input, out var id)) //Convert input to number
        {
            _service.DeleteContact(id); //Delete contact
            _console.WriteLine("Deleted!"); //how “Deleted!”
        }   //Find contact → remove it”
    }

    private void SearchContactFlow() //Finds contacts by name
    {
        _console.WriteLine("Enter name:"); //Ask user for a search term
        var search = _console.ReadLine() ?? ""; //nothing entered → use empty string

        var results = _service.SearchContacts(search); //Ask service to search
        if (!results.Any())
        {
            _console.WriteLine("Name not found");
            return;
        }

        foreach (var c in results)
        {
            _console.WriteLine(c.ToString()); //Show all matching results
        }   //Type name → see matching contacts

    }
}













