namespace ContactManager.Api.Models.Responses;

public class CreateContactResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";

    }