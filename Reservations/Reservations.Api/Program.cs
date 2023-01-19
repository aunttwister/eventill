using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.Extensions.Configuration;
using Reservations.Application;
using Reservations.Persistance;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

/*builder.Services.AddAuthentication(v => {
        v.DefaultAuthenticateScheme = GoogleDefaults.AuthenticationScheme;
        v.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
    })
    .AddGoogle(googleOptions => {
    googleOptions.ClientId = "";//Authentication:Google:ClientId
    googleOptions.ClientSecret = "";//Authentication:Google:ClientSecret
});*/

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPersistance(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
