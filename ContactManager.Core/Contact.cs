namespace ContactManager.Core;

// =======================
// MODEL
// =======================
// Represents a single contact (data structure only)
public class Contact
{
    public int Id { get; set; }       // Unique identifier (read-only)
    public string Name { get; set; }  // Contact name (read-only)

    public Contact(string name)
    {
        // Validation: prevent invalid data entering system
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty");

        // Assign values to properties
        Name = name;
    }

    // STRING REPRESENTATION
    public override string ToString()
    {
        // Defines how object looks when printed
        // Example: Console.WriteLine(contact)
        // Output: "1: John"
        return $"{Id}: {Name}";
    }
}













