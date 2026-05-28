using ContactManager.Core;

namespace ContactManager.core;

public interface IContactService
{
    Contact Add(string name, string email, string phone);

    IReadOnlyList<Contact>GetContacts();

    void UpdateContact(int id, string name, string email, string phone);
    void DeleteContact();

    List<Contact>SearchContacts(string name);

}