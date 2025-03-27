using EmployeeAdminPortal.MappingProfiles;
using EmployeeAdminPortal.Models;
using EmployeeAdminPortal.Repository;
using EmployeeAdminPortal.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddValidatorsFromAssemblyContaining<EmployeeCreateDTOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<EmployeeUpdateDTOValidator>();
builder.Services.AddScoped<IValidator<EmployeeCreateDTO>, EmployeeCreateDTOValidator>();
builder.Services.AddScoped<IValidator<EmployeeUpdateDTO>, EmployeeUpdateDTOValidator>();

builder.Services.AddFluentValidationAutoValidation(); 
builder.Services.AddFluentValidationClientsideAdapters();

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

// builder.Services.AddScoped<TokenService>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);


builder.Services.AddDbContext<ApplicationDbContext>
(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// app.UseAuthorization();

app.MapControllers();
app.Run();