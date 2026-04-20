using ContactManager.Core;

namespace ContactManager.Tests;

public class UnitTest1
{
    [Fact]
    public void CreateContact_ID()
    {
        var contact = new Contact("Armen",32);
        Assert.Equal("Armen", contact.Name);
        Assert.Equal(32,contact.Id);

    }
}