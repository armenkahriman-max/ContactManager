using System.Net;
using System.Net.Http.Json;
using ContactManager.Api.Models.Requests;
using ContactManager.Api.Models.Responses;
using Xunit;

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
            HttpStatusCode.Created,
            response.StatusCode);

        var created = await response.Content
            .ReadFromJsonAsync<CreateContactResponse>();

        Assert.NotNull(created);
        Assert.NotEqual(0, created.Id);
        Assert.Equal("Ada Lovelace", created.Name);
    }
}