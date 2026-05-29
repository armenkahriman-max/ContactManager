using ContactManager.Api.Models.Requests;
using ContactManager.Core;
using Microsoft.AspNetCore.Mvc;





namespace ContactManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactsController : ControllerBase
{
    private readonly ContactService _contactService;

    public ContactsController(ContactService contactService)
    {
        _contactService = contactService;
    }


    [HttpGet]
    public IActionResult Get()
    => Ok(_contactService.GetContacts());


    [HttpGet("Search")]
    public IActionResult Search([FromQuery] string name)
    // // string? name = Request.Query["name"];
    => Ok(_contactService.SearchContacts(name));




    [HttpPost]
    public IActionResult Create(CreateContactRequest request)
    {
        var response = _contactService.AddContact(request);
        return Created("", response);

    }
    [HttpPut("{id}")]
    public IActionResult Edit(int id, UpdateContactRequest request)
    {
        return _contactService.UpdateContact(id, request) == false ? NotFound("Contact not found") : Ok();

    }


    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        return _contactService.DeleteContact(id) == false ? NotFound("Contact not found") : Ok("Contact deleted");
    }
}




