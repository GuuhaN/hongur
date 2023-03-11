using System.Security.Claims;
using hongur.Application;
using hongur.Application.Auth0;
using hongur.Responses.Ratings;
using hongur.Responses.Ratings.Interfaces;
using hongur.Responses.Restaurants;
using hongur.Responses.Restaurants.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.TryAddScoped<IRestaurantService, RestaurantService>();
builder.Services.TryAddScoped<IRatingService, RatingService>();
builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

builder.Services.AddControllers().AddNewtonsoftJson(options=>
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);;
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer("name=ConnectionStrings:Hongur"));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["Auth0:Domain"];
        options.Audience = builder.Configuration["Auth0:Audience"];
        // If the access token does not have a `sub` claim, `User.Identity.Name` will be `null`. Map it to a different claim by setting the NameClaimType below.
        options.TokenValidationParameters = new TokenValidationParameters
        {
            NameClaimType = ClaimTypes.NameIdentifier
        };
        options.RequireHttpsMetadata = false;
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("read:messages", policy => policy.Requirements.Add(new HasScopeRequirement("read:messages", builder.Configuration["Auth0:Domain"])));
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "API",
            Version = "v1",
            Description = "A REST API",
            TermsOfService = new Uri("https://lmgtfy.com/?q=i+like+pie")
        });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            Implicit = new OpenApiOAuthFlow
            {
                Scopes = new Dictionary<string, string>
                {
                    { "openid", "Open Id" }
                },
            }
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger(); 
app.UseSwaggerUI();

// Applies migration after one is created
using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dataContext.Database.Migrate();
}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseSession();

app.UseCors(b =>
{
    b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
});

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.MapControllers();

app.Run();