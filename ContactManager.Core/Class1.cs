using System.Data.Common;
using System.Diagnostics.Contracts;
using System.Dynamic;

namespace ContactManager.Core;

public class Contact(string name)
{
    public int Id { get; }
    public string Name { get; } = name;

}

public class InMemoryContactRepository
{
private readonly List<Contact> _contacts = new();

    public void Add(Contact contact)
    {
        _contacts.Add(contact);
    }
    
public IReadOnlyList<Contact> GetAll()
    {
        return _contacts.AsReadOnly();
    }
}









