

namespace ContactManager.Core;

// =======================
// REPOSITORY (DATA LAYER)
// =======================
// Responsible ONLY for storing and retrieving data
public class InMemoryContactRepository : IContactRepository
{
    // This list acts like a "fake database"
    // It stores all contacts in memory (RAM)
    private readonly List<Contact> _contacts = new();

    // This keeps track of the next ID to assign
    // Every new contact gets a unique number
    private int _nextId = 1;

    // Add new contact
    public void Add(Contact contact)
    {
        contact.Id = _nextId++;
        _contacts.Add(contact);
    }

    // Get all contacts (read-only protection)
    public IReadOnlyList<Contact> GetAll()
    {
        // Return list but make it read-only
        // so outside code cannot modify internal data directly
        return _contacts.AsReadOnly();
    }

    // Find one contact by ID
    public Contact? GetById(int id)
    {
        // Search list for first contact with matching ID
        // Returns null if nothing is found
        return _contacts.FirstOrDefault(c => c.Id == id);
    }

    // Update existing contact
    public void Update(int id, string name, string email, string phone)
    {
        var contact = GetById(id);
        if (contact == null) return;

        contact.Name = name;
        contact.Email = email;
        contact.Phone = phone;
    }

    // Delete contact
    public void Delete(int id)
    {
        // Find contact
        var contact = _contacts.FirstOrDefault(c => c.Id == id);
        // If not found, stop
        if (contact == null)
            throw new KeyNotFoundException($"Contact with ID {id} not found");

        // Remove from list
        _contacts.Remove(contact);
    }

    // Search contacts by name
    public List<Contact> Search(string name)
    {
        // Find all contacts that contain the search text
        // Ignore uppercase/lowercase differences
        return _contacts.Where(c => c.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();

    }

    public void add(Contact contact)
    {
        throw new NotImplementedException();
    }

    public IReadOnlyList<Contact> Getall()
    {
        throw new NotImplementedException();
    }

    public void delte(int id)
    {
        throw new NotImplementedException();
    }
}













