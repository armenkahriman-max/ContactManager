


using System.ComponentModel.DataAnnotations;
using ContactManager.Api.Models.Requests;
using ContactManager.Api.Models.Responses;

namespace ContactManager.Core;

// =======================
// SERVICE (BUSINESS LOGIC)
// =======================
// Contains rules, validation, and application logic
public class ContactService
{
    // This is the repository (data layer)
    // It is responsible for storing and retrieving contacts
    private readonly IContactRepository _repository;

    // Dependency Injection (IoC):
    // The repository is passed in from outside instead of created here
    public ContactService(IContactRepository repository)
    {
        _repository = repository;
    }

    public CreateContactResponse AddContact(CreateContactRequest request)
    { //in future do not put CretqteContact put the veriables inside request.name,email etc for better organisation
      //make dot for backend and frontend in the future
      // Validation: prevent empty or invalid names
        if (string.IsNullOrWhiteSpace(request.Name))
            throw new ArgumentException("Name cannot be empty");
        /*
    if (string.IsNullOrWhiteSpace(request.Email))
        throw new ArgumentException("Email cannot be empty");
    if (string.IsNullOrWhiteSpace(request.Phone))
        throw new ArgumentException("Phone number cannot be Empty");
        */

        var contact = new Contact(request.Name, request.Email, request.Phone);

        _repository.Add(contact);

        return new CreateContactResponse
        {
            Id = contact.Id,
            Name = contact.Name,
            Email = contact.Email,
            Phone = contact.Phone
        };
    }

    public IReadOnlyList<Contact> GetContacts()
    {
        // Ask repository for all stored contacts
        return _repository.GetAll();
    }

    public bool UpdateContact(int id, UpdateContactRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            throw new ArgumentException("Name cannot be empty");
        /*
    if (string.IsNullOrWhiteSpace(request.Email))
        throw new ArgumentException("Email cannot be empty");
        */

        var contact = _repository.GetById(id);
        if (contact == null) return false;

        _repository.Update(id, request.Name, request.Email, request.Phone);
        return true;
    }

    public bool DeleteContact(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid contact ID");
        //// Send delete request to repository
        var contact = _repository.GetById(id);
        if (contact == null) return false;

        _repository.Delete(id);
        return true;
    }

    // SEARCH CONTACTS
    public List<CreateContactResponse> SearchContacts(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty");
        // Ask repository to find matching contacts
        var contacts = _repository.Search(name);

        return contacts.Select(c => new CreateContactResponse
        {
            Id = c.Id,
            Name = c.Name,
            Email = c.Email,
            Phone = c.Phone

        }).ToList();
    }
}













