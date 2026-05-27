using Microsoft.AspNetCore.Mvc;
using ContactManager.Core;
using System.ComponentModel.DataAnnotations;



namespace ContactManager.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ContactController : ControllerBase
{
    private readonly ContactService _contactService;

    public ContactController(ContactService contactService)
    {
        _contactService = contactService;
    }
    public class ContactResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";

    }

    [HttpGet]
    public IEnumerable<ContactResponse> Get()
    {
        return _contactService.GetContacts()
         .Select(contact => new ContactResponse
         {
             Id = contact.Id,
             Name = contact.Name,
             Email = contact.Email,
             Phone = contact.Phone
         });

    }

    [HttpGet("Search")]
    public IEnumerable<ContactResponse> Search(string name)
    {
        return _contactService.SearchContacts(name)
        .Select(contact => new ContactResponse
        {
            Id = contact.Id,
            Name = contact.Name
        });

    }

    [HttpPost]
    public IActionResult Create(CreateContactRequest request)
    {
        try
        {
            var contact = _contactService.AddContact(
                request.Name,
                request.Email,
                request.Phone);

            return Ok(new ContactResponse
            {
                Id = contact.Id,
                Name = contact.Name,
                Email = contact.Email,
                Phone = contact.Phone
            });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }

    }
    [HttpPut("{id}")]
    public IActionResult Edit(int id, UpdateContactRequest request)
    {
        _contactService.UpdateContact(
        id,
         request.Name,
          request.Email,
           request.Phone);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _contactService.DeleteContact(id);


        return NoContent();
    }
}



