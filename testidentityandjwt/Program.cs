using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using testidentityandjwt.DAL.Context;
using testidentityandjwt.DAL.Entities;
using testidentityandjwt.DAL.Startuphelper;

var builder = WebApplication.CreateBuilder(args);

//IN .NET 6 NON VI è PIù CLASSE STARTUP.
//METODO CONFIGURE SERVICES PER CONFIGURARE CONTAINER DEPENDENCY INJECTION SOSTITUITO CON WEBAPPLICATION.SERVICES

//ConfigureServices(builder.Services);
StartupHelper.Configureservices(builder.Services);




builder.Services.AddDbContext<jwtandidentitycontext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentityCore<MyUser>()
    .AddEntityFrameworkStores<jwtandidentitycontext>()
    .AddDefaultTokenProviders();

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

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
