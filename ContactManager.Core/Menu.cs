using System;
using System.Collections.Generic;
using System.Linq;

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
        var running = true; // controls when program stops

        while (running) // Loop keeps running until user chooses to exit
        {
            ShowMenu(); // display menu options to use

            // Read user input from console
            // If null → replace with empty string to avoid errors
            var input = _console.ReadLine() ?? "";

            // Decide what to do based on user input
            running = HandleChoice(input);
        }

        return 0;  // Exit code (0 = success)
    }

    // This method ONLY shows text to the user
    // It does NOT take input or do logic
    private void ShowMenu()
    {
        _console.WriteLine("1. Contact Toevoegen"); // Option 1: create a new contact
        _console.WriteLine("q. Exit"); // Option q: Exit
        _console.WriteLine("Maak uw keuze:"); // Option 3: make new choice
    }

    private void HandleAddContact()
    {
        _console.WriteLine("Voer een naam in: ");

        // Read user input from console
        // If null → replace with empty string to avoid errors
        var input = _console.ReadLine() ?? "";

        if (!string.IsNullOrWhiteSpace(input)) //If name is valid:
        {
            _service.AddContact(input); //send it to the service (save it)
            _console.WriteLine("Contact toegevoegd: " + input);
        }
        else //If not valid
        {
            _console.WriteLine("Invalid name."); //show error message
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
        var input = _console.ReadLine(); //Convert input to number

        if (int.TryParse(input, out var id)) //Ask for new name
        {
            _console.WriteLine("Enter new name:");
            var name = _console.ReadLine();

            if (!string.IsNullOrWhiteSpace(name)) //If name is valid:
            {
                _service.UpdateContact(id, name);
                _console.WriteLine("Updated!"); //show “Updated!”
            }    //Find contact → replace its name”
        }
    }

    private void DeleteContactFlow() //Removes a contact
    {
        _console.WriteLine("Enter ID:"); //Ask for ID
        var input = _console.ReadLine(); //Convert input to number

        if (int.TryParse(input, out var id)) //Send ID to service
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

        foreach (var c in results)
        {
            _console.WriteLine(c.ToString()); //Show all matching results
        }   //Type name → see matching contacts
    }
}













