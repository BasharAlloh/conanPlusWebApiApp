using conanPlusWebApiApp.Dal;
using conanPlusWebApiApp.JWT;
using conanPlusWebApiApp.Models;
using conanPlusWebApiApp.Profiles;
using GridSender;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// CORS Configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Read configuration
var configuration = builder.Configuration;

// JWT Authentication Configuration
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
        ValidIssuer = configuration["JWT:Issuer"],
        ValidAudience = configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"])),
        ClockSkew = TimeSpan.Zero
    };

    // Custom event to validate token version
    options.Events = new JwtBearerEvents
    {
        OnTokenValidated = async context =>
        {
            var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
            var tokenVersionClaim = claimsIdentity?.FindFirst("TokenVersion")?.Value;

            if (!int.TryParse(tokenVersionClaim, out int currentTokenVersion))
            {
                context.Fail("Token is invalid");
                return;
            }

            var userRepository = context.HttpContext.RequestServices.GetRequiredService<ICommonRepository<User>>();
            var user = await userRepository.GetAll().ContinueWith(task => task.Result.FirstOrDefault(u => u.Username == claimsIdentity.Name));

            if (user == null || user.TokenVersion != currentTokenVersion)
            {
                context.Fail("Token is invalid");
            }
        }
    };
});

// Authorization Policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
    options.AddPolicy("UserPolicy", policy => policy.RequireRole("User", "Admin"));
});

// Database Context Configuration
builder.Services.AddDbContext<conanPlusWebApiAppDbContext>((serviceProvider, options) =>
{
    var config = serviceProvider.GetRequiredService<IConfiguration>();
    options.UseSqlServer(config.GetConnectionString("MsSqlConStr"));
});

// Add IHttpContextAccessor for HttpContext support
builder.Services.AddHttpContextAccessor();

// Add Controllers Service
builder.Services.AddControllers();

// Add Repositories and Managers
builder.Services.AddScoped(typeof(ICommonRepository<>), typeof(CommonRepository<>));
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddTransient<ICommonRepository<User>, CommonRepository<User>>();

// Register TokenManager as Scoped instead of Singleton
builder.Services.AddScoped<ITokenManager, TokenManager>();

// AutoMapper Profiles Registration
builder.Services.AddAutoMapper(
    typeof(FilterProfile),
    typeof(ProjectProfile),
    typeof(ServiceProfile),
    typeof(VisionProfile)
);

// Register Repositories for all Models
builder.Services.AddTransient<ICommonRepository<Employee>, CommonRepository<Employee>>();
builder.Services.AddTransient<ICommonRepository<AboutUs>, CommonRepository<AboutUs>>();
builder.Services.AddTransient<ICommonRepository<CustomServiceBtnLink>, CommonRepository<CustomServiceBtnLink>>();
builder.Services.AddTransient<ICommonRepository<FAQ>, CommonRepository<FAQ>>();
builder.Services.AddTransient<ICommonRepository<Feature>, CommonRepository<Feature>>();
builder.Services.AddTransient<ICommonRepository<Filter>, CommonRepository<Filter>>();
builder.Services.AddTransient<ICommonRepository<Goal>, CommonRepository<Goal>>();
builder.Services.AddTransient<ICommonRepository<Package>, CommonRepository<Package>>();
builder.Services.AddTransient<ICommonRepository<Partner>, CommonRepository<Partner>>();
builder.Services.AddTransient<ICommonRepository<Project>, CommonRepository<Project>>();
builder.Services.AddTransient<ICommonRepository<Service>, CommonRepository<Service>>();
builder.Services.AddTransient<ICommonRepository<Vision>, CommonRepository<Vision>>();
builder.Services.AddTransient<ICommonRepository<User>, CommonRepository<User>>();

// Configure SendGrid service
// Configure SendGrid service
builder.Services.AddTransient<IEmailService, SendGridEmailService>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var sendGridApiKey = configuration["SendGrid:ApiKey"];
    return new SendGridEmailService(sendGridApiKey);
});



// Swagger Configuration for API Documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' followed by your token in the text input below. Example: `Bearer 12345abcdef`"
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
                }
            },
            new string[] {}
        }
    });
});

// Build the application
var app = builder.Build();
app.UseStaticFiles();

app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
