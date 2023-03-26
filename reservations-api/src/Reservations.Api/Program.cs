using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Reservations.Api.Authorization;
using Reservations.Api.Middlewares;
using Reservations.Api.Services;
using Reservations.Application;
using Reservations.Application.Common.Interfaces;
using Reservations.Persistance;
using Reservations.Security;

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
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Artisti Reservations API (v1)",
        Description = "<p>Provides API endpoints for Artisti Reservations application.</p>"
                    + "<p><h3>Authorization</h3>Authorization is maintained through in house identity management with leveraging oauth2 flow."
                    + "<br/>In order to use system level integration, user needs to be created in the database. "
                    + "After setting up system level user, auth token can be retrieved by using Postman Login endpoint.</p>"
    });
    options.AddSecurityDefinition("Bearer",
                new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter the JWT token in format: Bearer {JWT_TOKEN}",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddCors();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
            .AddJwtBearer(options =>
            {
                //options.Authority = builder.Configuration.GetSection("JWTAuthentication:Authority").Value;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = builder.Configuration.GetSection("JWTAuthentication:Issuer").Value,
                    ValidateIssuerSigningKey = true,
                    ValidAudiences = builder.Configuration.GetSection("JWTAuthentication")?
                    .GetSection("Audiences")?
                    .GetChildren()?.Select(x => x.Value)?
                    .ToList(),
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Convert.FromBase64String(builder.Configuration.GetSection("JWTAuthentication:Secret").Value))
                };
            });

builder.Services.AddAuthorization(options =>
{
    options.DefaultPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .AddRequirements(new ProfileAuthorizationRequirement())
        .Build();
});

builder.Services.AddSecurity(builder.Configuration);
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<IAuthorizationHandler, ProfileAuthorizationHandler>();

builder.Services.AddApplication(builder.Configuration);
builder.Services.AddPersistance(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger(options => options.RouteTemplate = "swagger/{documentName}/swagger.json");
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint($"v1/swagger.json", "Reservations API");
});

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

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
