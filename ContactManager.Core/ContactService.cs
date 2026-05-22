using System.ComponentModel.DataAnnotations;

namespace ContactManager.Core;

// =======================
// SERVICE (BUSINESS LOGIC)
// =======================
// Contains rules, validation, and application logic
public class ContactService
{
    // This is the repository (data layer)
    // It is responsible for storing and retrieving contacts
    private readonly InMemoryContactRepository _repository;

    // Dependency Injection (IoC):
    // The repository is passed in from outside instead of created here
    public ContactService(InMemoryContactRepository repository) // pull InMemoryconontactrepository in to ContactService
    {
        // Dependency Injection (IoC) = something that u need to do the work
        _repository = repository;
    }

    public void AddContact(string name, string email, string phone)
    {
        // Validation: prevent empty or invalid names
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty");
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty");
            if(string.IsNullOrEmpty(phone))
            throw new ArgumentException("Phone number cannot be Empty");
           


        Contact contact = new Contact(name, email, phone);
        // Pass data to repository to store it
        _repository.Add(contact);
    }

    public IReadOnlyList<Contact> GetContacts()
    {
        // Ask repository for all stored contacts
        return _repository.GetAll();
    }

    public void UpdateContact(int id, string name, string email, string phone)
    {
        // Send update request to repository
        _repository.Update(id, name, email, phone);
    }

    public void DeleteContact(int id)
    {
        //// Send delete request to repository
        _repository.Delete(id);
    }

    // SEARCH CONTACTS
    public List<Contact> SearchContacts(string name)
    {
        // Ask repository to find matching contacts
        return _repository.Search(name).ToList();

    }
}













