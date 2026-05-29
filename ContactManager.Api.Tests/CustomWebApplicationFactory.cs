using Microsoft.AspNetCore.Mvc.Testing;

namespace ContactManager.Api.Tests;

public class CustomWebApplicationFactory
    : WebApplicationFactory<Program>
{
    // So it's a box containing all the API code, and when I run a test, it applies to it...
}