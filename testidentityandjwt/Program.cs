using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using testidentityandjwt.DAL.Context;
using testidentityandjwt.DAL.Entities;
using testidentityandjwt.DAL.Repository;

using testidentityandjwt.Startuphelper;
//using DataProtectionProvider = testidentityandjwt.DataProtectionProvider;

var builder = WebApplication.CreateBuilder(args);

//IN .NET 6 NON VI � PI� CLASSE STARTUP.
//METODO CONFIGURE SERVICES PER CONFIGURARE CONTAINER DEPENDENCY INJECTION SOSTITUITO CON WEBAPPLICATION.SERVICES

//using a static method with 'this' keyword for the first argument allows you to call the method
//directly on the object 'this' refers to
//builder.Services.Configureservices();

//This is a 'fake' implementation for IDataProtectionProvider, it does nothing
//I'm not sure what this thing is supposed to do, but I would research to see
//if you need to implement the methods that throw a NotImplementedException
//or if there is an implementation that you can register
//builder.Services.Configureservices();
//builder.Services.AddTransient<IDataProtectionProvider, DataProtectionProvider>();



builder.Services.AddDbContext<jwtandidentitycontext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<MyUser,IdentityRole>()
    .AddEntityFrameworkStores<jwtandidentitycontext>()
    .AddDefaultTokenProviders();


builder.Services.Configureservices();


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

app.UseHttpsRedirection();

//app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
