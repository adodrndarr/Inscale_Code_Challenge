using Booking.Source.Security;
using Booking.Source.Services.Implementations;
using Booking.Source.Services.Middlewares;
using Microsoft.AspNetCore.Authentication;


const string BASIC_AUTHENTICATION = "BasicAuthentication";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddHttpClient();
builder.Services.AddSingleton<SearchService, SearchService>();
builder.Services.AddAuthentication(BASIC_AUTHENTICATION)
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>(BASIC_AUTHENTICATION, null);


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalErrorHandlingMiddleware>();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
