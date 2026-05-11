using Ṃeenkaran.Application.Interfaces;
using Ṃeenkaran.Application.Interfaces.Admin;
using Ṃeenkaran.Application.Services;
using Ṃeenkaran.Application.Services.Admin;
using Ṃeenkaran.Infrastructure.Data;
using Ṃeenkaran.Presentaion.Middleware;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using Microsoft.OpenApi.Models;

using System.Text;

var builder = WebApplication.CreateBuilder(args);

// -------------------- SERVICES --------------------

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Meenkaran API",
        Version = "v1"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter JWT Token"
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
            Array.Empty<string>()
        }
    });
});

// CORS (IMPORTANT for frontend)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// -------------------- BUSINESS SERVICES --------------------

builder.Services.AddScoped<IFishingSpotService, FishingSpotService>();
builder.Services.AddScoped<IGuideService, GuideService>();
builder.Services.AddScoped<IActiveFishingFeedService, ActiveFishingFeedService>();
builder.Services.AddScoped<IGuideBookingService, GuideBookingService>();
builder.Services.AddScoped<ITripBookingRequestService, TripBookingRequestService>();
builder.Services.AddScoped<ICatchPostService, CatchPostService>();
builder.Services.AddScoped<IPostInteractionService, PostInteractionService>();
builder.Services.AddScoped<ILeaderboardService, LeaderboardService>();
builder.Services.AddScoped<IGuideReviewService, GuideReviewService>();
builder.Services.AddScoped<IGuideAuthService, GuideAuthService>();
builder.Services.AddScoped<IGuideProfileService, GuideProfileService>();
builder.Services.AddScoped<IGuidePackageService, GuidePackageService>();
builder.Services.AddScoped<IGuideReportService, GuideReportService>();
builder.Services.AddScoped<IAdminVerificationService, AdminVerificationService>();


builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<CloudinaryService>();
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<EmailService>();

// -------------------- AUTHENTICATION --------------------

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.Cookie.SameSite = SameSiteMode.Lax; // Needed for some browsers during redirect
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],

        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
        )
    };
});

// -------------------- DATABASE --------------------

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

// -------------------- APP --------------------

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// CORS (must be before auth sometimes depending on frontend)
app.UseCors("AllowAll");
app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();