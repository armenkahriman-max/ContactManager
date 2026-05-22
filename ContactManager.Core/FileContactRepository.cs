using System.IO;
using System.Runtime.Serialization;
using ContactManager.Core;

public class FileContactRepository : IContactRepository
{

    private readonly string _filePath = "contacts.txt";

    private List<Contact> LoadFromFile()
    {
        if (File.Exists(_filePath))
            return contacts;
            var lines = File.ReadAllLines(_filePath);
            

      

    }
    public List<Contact> GetAll()
    {
        throw new NotImplementedException();
    }

    public Contact GetById(int id)
    {
        throw new NotImplementedException();
    }

    public void Add(Contact? contact)
    {
        throw new NotImplementedException();
    }

    public void Update(Contact? contact)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

}