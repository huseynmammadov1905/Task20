using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks;
using Serilog.Context;
using Serilog.Core;
using Serilog.Sinks.MSSqlServer;
using TodoWebService;
using TodoWebService.Services.Todo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwagger();


builder.Services.AddAuthenticationAndAuthorization(builder.Configuration);


builder.Services.AddDomainServices();

builder.Services.AddTodoContext(builder.Configuration);

builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

//app.UseOutputCache();
app.UseResponseCaching();

app.UseAuthorization();

app.MapControllers();

app.Run();
