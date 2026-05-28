
using ContactManager.Core;

public class FileContactRepository
{

    private readonly string _filePath = "contacts.txt";

   private List<Contact> LoadFromFile()
{
    if (File.Exists(_filePath))
    {
        var lines = File.ReadAllLines(_filePath);

        // TODO: convert lines to Contact objects
    }

    return new List<Contact>();
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