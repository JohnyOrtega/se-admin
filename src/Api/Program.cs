using System.Text;
using Api.Middlewares;
using Core.Extensions;
using Core.Models;
using Core.Models.Interfaces;
using Infrastructure.Extensions;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCore();
builder.Services.AddInfrastructure(builder.Configuration);

var tokenConfiguration = new TokenConfiguration();
builder.Configuration.GetSection("Jwt").Bind(tokenConfiguration);
builder.Services.AddSingleton<ITokenConfiguration>(tokenConfiguration);

builder.Services.AddAuthentication()
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = tokenConfiguration.Issuer,
            ValidAudience = tokenConfiguration.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(tokenConfiguration.SecretKey)),
        };
    });

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
});
builder.Services.AddHealthChecks();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAllOrigins", configurePolicy: policy =>
    {
        policy
            .WithOrigins(
                "https://localhost:5173", 
                "https://localhost:5174", 
                "https://superexpansao-admin.vercel.app", 
                "https://admin.superexpansao.com.br",
                "https://main.d2u07rn7pkfnfy.amplifyapp.com/")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllOrigins");
app.UseMiddleware<TokenValidationMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseHttpsRedirection();
app.MapHealthChecks("/health");
app.MapControllers();
app.Run();