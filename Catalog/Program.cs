using Catalog.Setting;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMassTransit(d =>
 d.UsingRabbitMq((context, configurator) => {
     var rabbitMQSettings = builder.Configuration.GetSection(nameof(RabbitMQSetting)).Get<RabbitMQSetting>();
     configurator.Host(rabbitMQSettings.Host,"/", h => {
         h.Username("user");
         h.Password("password");
     });
     configurator.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter("Catalog", false));
 })
);
builder.Services.AddMassTransitHostedService();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
