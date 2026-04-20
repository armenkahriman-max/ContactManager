using ContactManager.Core;

namespace ContactManager.Tests;

public class UnitTest1
{
    [Fact]
    public void AddContact_ShouldApearInGetAll()
    { //Arange
        var repo = new InMemoryContactRepository();
        var contact = new Contact("Armen");
     //Act
        repo.Add(contact);
        var result =repo.GetAll();
    //Assert
      Assert.Contains(contact,result);
      Assert.Single(result);
    }
}