using System.Net;
using System.Net.Http.Json;
using ContactManager.Api.Controllers;
using ContactManager.Api.Models.Requests;
using ContactManager.Api.Models.Responses;
using ContactManager.Core;
using Microsoft.AspNetCore.Mvc;
using Moq;



namespace ContactManager.Api.Tests;

public class ContactApiTests
{
    private readonly CustomWebApplicationFactory factory = new();


    [Fact]
    public async Task Post_contact_creates_a_contact()
    {
        var client = factory.CreateClient();

        var request = new CreateContactRequest
        {
            Name = "Ada Lovelace",
            Email = "ada.lovelance@example.com",
            Phone = "0404-123-456"
        };

        var response = await client.PostAsJsonAsync(
            "/api/contacts",
            request);

        Assert.Equal(
            HttpStatusCode.Created, //201
            response.StatusCode);

        var created = await response.Content
            .ReadFromJsonAsync<CreateContactResponse>();

        Assert.NotNull(created);
        Assert.NotEqual(0, created.Id);
        Assert.Equal("Ada Lovelace", created.Name);
    }

    // Name = "" >>> 400
    [Fact]
    public async Task InvalidInput_NameISNull_Error400()
    {
        var client = factory.CreateClient();
        var response = await client.PostAsJsonAsync("/api/contacts",
        new CreateContactRequest { Name = "" });
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    // GET --2
    [Fact]
    public async Task Get_Allcontacts()
    {
        HttpClient client = factory.CreateClient();

        // Add
        await client.PostAsJsonAsync("/api/contacts", new CreateContactRequest { Name = "Armen" });
        await client.PostAsJsonAsync("/api/contacts", new CreateContactRequest { Name = "Mark" });

        // Get
        HttpResponseMessage response = await client.GetAsync("/api/contacts");
        List<Contact>? contacts = await response.Content.ReadFromJsonAsync<List<Contact>>();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(contacts);
        //  Assert.Contains(contacts, (contact) => contact.Name == "Riyad");
        Assert.True(
            contacts.Any(contact => contact.Name == "Armen") &&
            contacts.Any(contact => contact.Name == "Mark"));

    }

    // Update --3
    [Fact]
    public async Task Update_Contact()
    {
        var client = factory.CreateClient();

        // Add
        var createdResponse = await client.PostAsJsonAsync("/api/contacts",
        new CreateContactRequest { Name = "Armen" });
        var created = await createdResponse.Content.ReadFromJsonAsync<Contact>();
        Assert.NotNull(created);

        // Update
        var updateResponse = await client.PutAsJsonAsync($"/api/contacts/{created.Id}",
        new UpdateContactRequest
        {
            Name = "Armenn",
            Email = "riyad.m.salem.19988@gmail.com",
            Phone = "0472789025"
        });
        Assert.Equal(HttpStatusCode.OK, updateResponse.StatusCode); //200

        // Get
        var response = await client.GetAsync("/api/contacts");
        var contacts = await response.Content.ReadFromJsonAsync<List<Contact>>();

        // Result
        Assert.NotNull(contacts);
        Assert.Contains(contacts, contact => contact.Name == "Armenn");
    }
    // Update(Not Found (ID)) 404 --4
    [Fact]
    public async Task Update_IdIsNotFound_Error404()
    {
        var client = factory.CreateClient();
        var response = await client.PutAsJsonAsync("/api/contacts/1",
        new UpdateContactRequest { Name = "Armen" });
        Console.WriteLine(response.StatusCode);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    // Delete --5
    [Fact]
    public async Task Delete_Contact()
    {
        var client = factory.CreateClient();

        // Add
        var createdResponse = await client.PostAsJsonAsync("/api/contacts",
        new CreateContactRequest { Name = "Armen" });
        var created = await createdResponse.Content.ReadFromJsonAsync<Contact>();
        Assert.NotNull(created);

        // Delete
        var response = await client.DeleteAsync($"/api/contacts/{created.Id}");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

    }

    // Search (by name) --7
    [Fact]
    public async Task Search_contacts_by_name()
    {
        var client = factory.CreateClient();

        // Add
        await client.PostAsJsonAsync("/api/contacts", new CreateContactRequest { Name = "Armen" });
        await client.PostAsJsonAsync("/api/contacts", new CreateContactRequest { Name = "Mark" });

        // Search by name
        var response = await client.GetAsync("/api/contacts/search?name=arm");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var contacts = await response.Content.ReadFromJsonAsync<List<Contact>>();
        Assert.NotNull(contacts);

        Assert.Contains(contacts, (contact) => contact.Name == "Armen");
        Assert.DoesNotContain(contacts, (contact) => contact.Name == "Mark");
    }


    // Interaction tests
    // Verify that ((controller.GetAll())) is calling ((repository.GetAll()))
    [Fact]
    public void GetAllFunInController_Calls_GetAllFunInRepository_OneTime()
    {
        // fake repository
        Mock<IContactRepository> repository = new Mock<IContactRepository>();

        repository.Setup(r => r.GetAll()) // if I GetAll Calling van DB
        .Returns(new List<Contact> { new Contact("Armen", "", "") });

        // Add fake repository in service
        ContactService service = new ContactService(repository.Object);

        // Add service in controller
        ContactsController controller = new ContactsController(service);

        IActionResult result = controller.Get();
        repository.Verify(r => r.GetAll(), Times.Once);
    }


    /*
        // Verify that ((controller.Delete(id))) is ((repository.Remove(contact))) 
        [Fact]
        public void DeleteContactFunById_Calls_RemoveContactFunInRepository()
        {
            Mock<IContactRepository> repository = new Mock<IContactRepository>();
            Contact contact = new Contact("Armen", "", "");
            repository.Setup(r => r.GetById(contact.Id)).Returns(contact);

            ContactService service = new ContactService(repository.Object);
            ContactsController controller = new ContactsController(service);

            controller.Delete(contact.Id);
            repository.Verify(r => r.Delete(contact.Id), Times.Once);
        }
    */


}