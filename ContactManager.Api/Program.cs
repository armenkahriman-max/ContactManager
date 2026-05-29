using ContactManager.Core;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IContactRepository, InMemoryContactRepository>();
builder.Services.AddScoped<ContactService>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware — catches (ArgumentException) from the Service
// throws back 400 Bad Requests without try/catch in every controller
/*
context ....
Response => StatusConde, ContentType (app.json,,,), Body
Request => Path (api/contacts), Method, Header, Body
*/
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (ArgumentException e)
    {
        context.Response.StatusCode = 400; // BadRequest
        await context.Response.WriteAsync(e.Message);
    }
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program;




