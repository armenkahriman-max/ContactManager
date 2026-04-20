using System.Data.Common;
using System.Dynamic;

namespace ContactManager.Core;

public class Contact
{
    public int Id { get; private set; }
    public string? Name { get; private set; }

    public Contact(string name,int id)
    {
        Name = name;
        Id = id;
    }
    

}
public class InMemoryContactRepository
{
    public void Add(Contact contact) { throw new NotImplementedException(); }
    public IReadOnlyList<Contact> GetAll() { throw new NotImplementedException(); }

    private List<Contact> contact = new List<Contact>();

    public void AddContact(string name, int id)
    {
        if (contact.Any(c => c.Name == name))
        {
            throw new ArgumentException("Id exsists");
        }

        contact.Add(new Contact(name, id));

    }
    public List<Contact> GetID()
    {
        return contact;
    }

}
