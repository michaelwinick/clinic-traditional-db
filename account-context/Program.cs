using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using account_context.Domain.Handlers;
using Dapr;
using MediatR;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var daprHttpPort = Environment.GetEnvironmentVariable("DAPR_HTTP_PORT") ?? "3600";

builder.Services.AddDaprClient(builder =>
    builder
        .UseHttpEndpoint($"http://localhost:{daprHttpPort}")
        .UseJsonSerializationOptions(new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        }));

builder.Services.AddControllers().AddDapr();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

var app = builder.Build();

// for testing only
app.UseCors(cors => cors
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials());

app.UseRouting();
app.UseCloudEvents();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapSubscribeHandler(); // This is the Dapr subscribe handler
    endpoints.MapControllers();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//// Dapr subscription in [Topic] routes orders topic to this route
//app.MapPost("/order", [Topic("pubsub", "order")] (Order order) => {
//    Console.WriteLine("Subscriber received : " + order);
//    return Results.Ok(order);
//});



app.Run();
