using AppCore.Repositories;
using AppCore.Services;
using AutoMapper;
using Infrastructure;
using Infrastructure.Memory;
using Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddContactsModule(builder.Configuration);

builder.Services.AddSingleton<IPersonRepository, MemoryPersonRepository>();
builder.Services.AddSingleton<ICompanyRepository, MemoryCompanyRepository>();
builder.Services.AddSingleton<IOrganizationRepository, MemoryOrganizationRepository>();

builder.Services.AddSingleton<IContactUnitOfWork, MemoryContactUnitOfWork>();

builder.Services.AddSingleton<IPersonService, MemoryPersonService>();

builder.Services.AddContactsEfModule(builder.Configuration);
builder.Services.AddContactsCoreModule(builder.Configuration); 

var app = builder.Build();

if (app.Environment.IsDevelopment()) app.MapOpenApi();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();