using Microsoft.AspNetCore.Mvc;
using ContactManager.Core;

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

    [HttpGet]
    public IEnumerable<Contact> Get()
    {
        return _contactService.GetContacts();
    }
}