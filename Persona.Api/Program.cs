using Persona.Api.Extensions;
using PreubaLogics.Configuration;
using PreubaLogics.Extensions.Operations;
using PruebaDataaces.Configuration;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger("PersonaApi");
builder.Services.AddConfigureCollection()
    .AddConfigureDataAcces(builder.Configuration);
WebApplication app = builder.Build();

// Configure the HTTP request pipeline.

    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.UseRouting(); // Agrega el middleware de enrutamiento

app.UseEndpoints(endpoints => endpoints.MapControllers());
ControllerBase.Initialize(app.Services, app.Configuration, app.Environment);
OperationExtensions.Initialize(app.Services);
app.Run();
