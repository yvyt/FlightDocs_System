using FlightDocs_System.Data;
using FlightDocs_System.Service.Flights;
using FlightDocs_System.Service.RolePermissions;
using FlightDocs_System.Service.Roles;
using FlightDocs_System.Service.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Flight Docs API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Need a valid token",
        Name = "Authorization Token",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
}); 
builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("FlightDocs_DB"));
});
var secretBytes = Encoding.UTF8.GetBytes(builder.Configuration["AuthSettings:Key"]);
builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(op =>
{
    op.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["AuthSettings:Audience"],
        ValidIssuer = builder.Configuration["AuthSettings:Issuer"],
        RequireExpirationTime = true,
        IssuerSigningKey = new SymmetricSecurityKey(secretBytes)
    };
});
builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
    options.TokenLifespan = TimeSpan.FromHours(1);
});
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<IFlightService,FlightService>();
builder.Services.AddScoped<IFlightDocumentService,FlightDocumentService>();
builder.Services.AddScoped<ITypeDocumentService,TypeDocumentService>();
builder.Services.AddScoped<IDocumentService,DocumentService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IRolePermissionService, RolePermissionService>();
builder.Services.AddIdentity<IdentityUser, IdentityRole>(op =>
{
    op.Password.RequireDigit = false;
    op.Password.RequireLowercase = false;
    op.Password.RequiredLength = 8;
    op.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    op.User.AllowedUserNameCharacters = string.Empty;


}).AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddAuthorization(options =>
{

    List<string> modules = new List<string> { "Flight", "FlightDocument", "TypeDocument", "User", "Role", "Permission" };
    List<string> action = new List<string> { "Edit", "Delete", "Create", "View" };
    foreach (var module in modules)
    {
        foreach (var ac in action)
        {
            string temp = $"{ac}{module}";
            List<string> listPermission = new List<string>
            {
                "CreatePermission",
                "DeletePermission",
                "EditPermission",
                "DeleteRole"
            };
            if (!listPermission.Contains(temp))
            {
                options.AddPolicy($"{ac}{module}", policy => policy.RequireClaim("Permission", $"{ac}:{module}"));
            }
        }
    }
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();

}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
