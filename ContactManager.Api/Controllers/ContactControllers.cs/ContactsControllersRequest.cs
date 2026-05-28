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
    {
        var response = _contactService.GetContacts();
        return Ok(response);
    }

    [HttpGet("Search")]
    public IActionResult Search([FromQuery] string name)
    {
        try
        {

            var response = _contactService.SearchContacts(name);
            return Ok(response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }

    }

    [HttpPost]
    public IActionResult Create(CreateContactRequest request)
    {
        try
        {
            var response = _contactService.AddContact(request);

            return CreatedAtAction(nameof(Get), new {id = response.Id}, response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }

    }
    [HttpPut("{id}")]
    public IActionResult Edit(int id, UpdateContactRequest request)
    {
        try
        {
            _contactService.UpdateContact(id, request);

            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            _contactService.DeleteContact(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }

        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}




