using BookStore.BLL.Behaviors.Validation;
using BookStore.BLL.Mapping.Authors;
using BookStore.BLL.MediatR.Authors.GetAll;
using BookStore.DAL.Entities;
using BookStore.DAL.Persistence;
using BookStore.DAL.Repositories.Interfaces.RepositoryWrapper;
using BookStore.DAL.Repositories.Realizations.RepositoryWrapper;
using BookStore.WebApi.Extensions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using BookStore.BLL.Services.CookieServices.Realizations;
using BookStore.BLL.Services.CookieServices.Interfaces;
using BookStore.BLL.Services.TokenServices.Interfaces;
using BookStore.BLL.Services.TokenServices.Realizations;
using BookStore.WebApi.MiddleWares;
using BookStore.BLL.Services.AccessTokenCleaner.Interfaces;
using BookStore.BLL.Services.AccessTokenCleaner.Realizations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add DB Context
builder.Services.AddDbContext<BookStoreDbContext>(conf =>
{
    var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

    if (env.Equals("Development"))
    {
        conf.EnableDetailedErrors();
    }

    var conStr = builder.Configuration.GetSection(env).GetConnectionString("BookStoreDb");
    
    conf.UseNpgsql(conStr);

});

// Add Identity System
builder.Services.AddIdentity<User, IdentityRole<Guid>>(conf =>
 {
     conf.Password.RequiredLength = 7;
     // Add here more configuration for Identity System

 }).AddEntityFrameworkStores<BookStoreDbContext>()
 .AddDefaultTokenProviders();

// Add Cleaner Service
builder.Services.AddSingleton<ICleaner, AccessTokenCleaner>();

//Add Repository Wrapper as a Service

builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();

//Add MediatR
var currentAssemblies = AppDomain.CurrentDomain.GetAssemblies();

builder.Services.AddMediatR(config =>
{ 
    config.RegisterServicesFromAssemblyContaining(typeof(GetAllAuthorsHandler));
});

//Add Mapper
builder.Services.AddAutoMapper(typeof(AuthorProfile));

//Add Validation Behavior
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

//Add JWTToken Configuration
builder.Services.AddJWTTokenConfiguration(builder.Configuration);

//Add Token Service
builder.Services.AddSingleton<ICookieService, CookieService>();
builder.Services.AddScoped<ITokenService, JWTTokenSevice>();

//Add Validators
builder.Services.AddValidatorsFromAssemblyContaining(typeof(GetAllAuthorsQuery));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add Authorization
builder.Services.AddAuthorization();
builder.Services.AddAuthentication();

var policy = "default";

//Add CORS
builder.Services.EnableCORS(builder.Configuration, policy);

builder.Services.AddHttp_ContextAccessor();

var app = builder.Build();

await app.SeedDatabaseAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(policy);

app.MapControllers();

app.UseMiddleware<JWTMiddleware>();

app.UseAuthorization();// Must be definetly after JWTMiddleware 

app.Run();
