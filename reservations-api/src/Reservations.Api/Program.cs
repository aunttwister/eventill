using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Reservations.Api.Authorization;
using Reservations.Api.Middlewares;
using Reservations.Application;
using Reservations.Persistance;
using Reservations.Security;
using System.Text;

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

builder.Services.AddControllers(options => options.SuppressAsyncSuffixInActionNames = false);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication(builder.Configuration);
builder.Services.AddSecurity(builder.Configuration);
builder.Services.AddPersistance(builder.Configuration);

builder.Services.AddCors();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
            .AddJwtBearer(options =>
            {
                options.Authority = builder.Configuration["JWTAuthentication:Authority"];
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudiences = builder.Configuration.GetSection("JWTAuthentication")?
                    .GetSection("Audiences")?
                    .GetChildren()?.Select(x => x.Value)?
                    .ToList(),
                    ValidIssuer = builder.Configuration.GetSection("JWTAuthentication").GetSection("Issuer").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.ASCII.GetBytes(builder.Configuration.GetSection("JWTAuthentication:Secret").Value))
                };
            });

builder.Services.AddAuthorization(options =>
{
    options.DefaultPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .AddRequirements(new ProfileAuthorizationRequirement())
        .Build();
});

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.InjectStylesheet("https://cdnjs.cloudflare.com/ajax/libs/swagger-ui/4.1.3/swagger-ui.css");
        options.InjectJavascript("https://cdnjs.cloudflare.com/ajax/libs/swagger-ui/4.1.3/swagger-ui-bundle.js", "text/javascript");
        options.InjectJavascript("https://cdnjs.cloudflare.com/ajax/libs/swagger-ui/4.1.3/swagger-ui-standalone-preset.js", "text/javascript");
        options.SwaggerEndpoint($"v1/swagger.json", "Reservations API");
    });
}
else

    app.UseCustomExceptionHandler();

app.UseStaticFiles();

app.UseRouting();

app.UseCors(build =>
    build.WithOrigins(builder.Configuration["CORS:AllowedOrigins"])
         .WithHeaders(builder.Configuration["CORS:AllowedHeaders"])
         .WithMethods(builder.Configuration["CORS:AllowedMethods"])
);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
