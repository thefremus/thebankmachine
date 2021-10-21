using MediatR;
using Microsoft.EntityFrameworkCore;
using TheBankMachine.Api.Application;
using TheBankMachine.Domain.AggregatesModel.AccountAggregate;
using TheBankMachine.Infrastructure;
using TheBankMachine.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var optionsBuilder = new DbContextOptionsBuilder<TheBankMachineContext>();
optionsBuilder.UseSqlite(connectionString, sqliteOptionsAction: sqliteOptions =>
{
    sqliteOptions.MigrationsAssembly("TheBankMachine.Api");
});
var ctx = new TheBankMachineContext(optionsBuilder.Options);
builder.Services.AddDbContext<TheBankMachineContext>(x => { 
    x.UseSqlite(connectionString, sqliteOptionsAction: sqliteOptions =>
    {
        sqliteOptions.MigrationsAssembly("TheBankMachine.Api");
    });
});
builder.Services.AddScoped<IAccountRepository>(x => new AccountRepository(ctx));
builder.Services.AddMediatR(typeof(AccountCreateCommandHandler));

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

app.UseRouting();
//app.UseAuthorization();

app.MapControllers();

app.Run();
