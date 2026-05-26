using ContactManager.Core;

public interface IContactRepository
{
    void Add(Contact contact);

    IReadOnlyList<Contact> GetAll();

    Contact? GetById(int id);

    void Update(int id, string name, string email, string phone);

    void Delete(int id);

    List<Contact> Search(string name);
}