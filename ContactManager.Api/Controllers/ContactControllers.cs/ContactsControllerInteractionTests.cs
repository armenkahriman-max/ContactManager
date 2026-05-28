using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using ContactManager.Api.Controllers;
using ContactManager.Api.Models.Requests;
using ContactManager.Api.Models.Responses;
using ContactManager.Core;

namespace ContactManager.Api.Tests;
public class ContactsControllerInteractionTests
{
    [Fact]
    public void Create_passes_contact_name_to_service()
    {
        var service = new Mock<IContactService>();

        service
            .Setup(s => s.AddContact(It.IsAny<CreateContactRequest>()))
            .Returns(new Contact
            {
                Id = 42,
                Name = "Ada Lovelace"
            });

        var controller = new ContactsController(service.Object);

        controller.Create(new CreateContactRequest
        {
            Name = "Ada Lovelace"
        });

        service.Verify(
            s => s.AddContact(
                It.Is<CreateContactRequest>(request => request.Name == "Ada Lovelace")),
                Times.Once);
    }
}