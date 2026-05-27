
namespace ContactManager.Api.Controllers;

public class CreateContactRequest
{
    public string Name { get; set; } = "";
    public int Id { get; set; }
    public string Email { get; set; } = "";
    public string Phone { get; set; } = "";
}
