using ConsoleApp;
using ConsoleApp.Context;
using ConsoleApp.Repositories;
using ConsoleApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

var builder = Host.CreateDefaultBuilder().ConfigureServices(services =>
{

    services.AddDbContext<DataContext>(x => x.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\SCHOOL\DATALAGRING\DatabaseCustomerProductProject\ConsoleApp\ConsoleApp\Data\database.mdf;Integrated Security=True;Connect Timeout=30"));

    services.AddScoped<AddressRepository>();
    services.AddScoped<RoleRepository>();
    services.AddScoped<CustomerRepository>();
    services.AddScoped<CategoryRepository>();
    services.AddScoped<ProductRepository>();

    services.AddScoped<AddressService>();
    services.AddScoped<RoleService>();
    services.AddScoped<CustomerService>();
    services.AddScoped<CategoryService>();
    services.AddScoped<ProductService>();

    services.AddSingleton<ConsoleUI>();

}).Build();

var consoleUI = builder.Services.GetRequiredService<ConsoleUI>();

consoleUI.Run();