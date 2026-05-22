using Xunit;
using ContactManager.Core;


public class ContactServiceTests
{
    [Fact]
    public void AddContact_ShouldAddContact()
    {
        // Create a fake (in-memory) repository to store contacts during the test
        var repo = new InMemoryContactRepository();
        // Create the service that uses the repository
        var service = new ContactService(repo);
        // Call the method we want to test: add a contact named "John"
        service.AddContact("Frank", "FranktheTank@gmail.com", "0404332393");

        // Get all contacts from the service
        var contacts = service.GetContacts();

        // Check that there is exactly 1 contact in the list
        Assert.Single(contacts);
        // Check that the first contact's name is "John"
        Assert.Equal("Frank", contacts[0].Name);
    }
}
public class RepositoryTests
{
    [Fact]
    public void Add_ShouldGenerateIncrementingIds()
    {
        var repo = new InMemoryContactRepository();

        var contact1 = new Contact("Frank", "FranktheTank@gmail.com", "0403243241");
        var contact2 = new Contact("Bob", "Bobthebuilder@gmail.com", "0402343442");

        repo.Add(contact1);
        repo.Add(contact2);

        Assert.Equal(1, contact1.Id);
        Assert.Equal(2, contact2.Id);
    }
}
