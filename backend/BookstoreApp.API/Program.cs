using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models; // Add this using directive for Swagger  
using System.Text;
using Swashbuckle.AspNetCore;
using BookstoreApp.Application.DI;
using BookstoreApp.Infrastructure;
using BookstoreApp.Application.Interfaces;
using BookstoreApp.Infrastructure.Repository.UnitOfWork;
using BookstoreApp.Application.UseCases.Book;
using BookstoreApp.Application.Setting;
using BookstoreApp.Infrastructure.Payment.Vnpay; // Ensure this using directive is added for AddSwaggerGen extension  

var builder = WebApplication.CreateBuilder(args);

// Lấy JWT Key từ cấu hình  
var jwtKey = builder.Configuration["JwtSettings:Key"];
if (string.IsNullOrEmpty(jwtKey))
{
    throw new InvalidOperationException("JWT Key is not configured properly in appsettings.");
}

// Đăng ký các dịch vụ  
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);


builder.Services.AddSwaggerGen(options => // Ensure Swagger is properly configured  
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Bookstore API",
        Version = "v1"
    });
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // hoặc https nếu frontend dùng https
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // nếu bạn dùng cookie/session auth
    });
});

// Cấu hình Authentication với JWT Bearer  
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

builder.Services.Configure<FastApiSettings>(
    builder.Configuration.GetSection("FastApi")
);

// Nếu bạn dùng RecommendationService:
builder.Services.AddHttpClient<RecommendationService>();


builder.Services.AddAuthorization();
builder.Services.AddAuthorization();
builder.Services.AddScoped<IVnPayService, VnPayService>();




var app = builder.Build();

// Middleware pipeline  
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

