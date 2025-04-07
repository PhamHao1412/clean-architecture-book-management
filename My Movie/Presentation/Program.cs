using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.RateLimiting;
using AutoMapper;
using Carter;
using FluentValidation;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using My_Movie;
using My_Movie.Application.Behavior;
using My_Movie.Application.Exceptions;
using My_Movie.Infrastructure.Repositiory;
using My_Movie.IRepository;
using My_Movie.IRepository.Repositiory;
using My_Movie.Mapper;
using My_Movie.Middlewares;
using My_Movie.Presentation.Middlewares;
using Polly;
using Polly.Extensions.Http;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

var builder = WebApplication.CreateBuilder(args);

var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbPort = Environment.GetEnvironmentVariable("DB_PORT");
var dbName = Environment.GetEnvironmentVariable("DB_NAME");
var dbUser = Environment.GetEnvironmentVariable("DB_USER");
var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
builder.Configuration["ConnectionStrings:DefaultConnection"] = $"Host={dbHost};Port={dbPort};Database={dbName};Username={dbUser};Password={dbPassword}";
builder.Services.AddControllers();
builder.Services.AddCarter();
builder.Services.AddMvc();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddLogging();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();

#region HangFire
// builder.Services.AddHangfire(config =>
// {
//     config.UsePostgreSqlStorage(builder.Configuration.GetConnectionString("DefaultConnection"));
// });
// builder.Services.AddScoped<IJobTestService, JobTestService>();
// builder.Services.AddHangfireServer();

#endregion

#region Retry
builder.Services.AddScoped<IAsyncPolicy<HttpResponseMessage>>(serviceProvider =>
{
    var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
    return HttpPolicyExtensions
        .HandleTransientHttpError()
        .WaitAndRetryAsync(
            3,
            retryAttempt => TimeSpan.FromSeconds(15),
            async (outcome, timespan, retryAttempt, context) =>
            {
                logger.LogInformation(
                    $"Retry attempt {retryAttempt} after {timespan.TotalSeconds} seconds due to: {outcome.Exception?.Message}");
            });
});
#endregion

#region MediatR & FluentValidation
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
    // cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
#endregion

#region Logging

builder.Host.UseSerilog((hostContext, services, configuration) =>
{
    configuration
        .WriteTo.Async(writeTo => writeTo.Console(theme: AnsiConsoleTheme.Sixteen,
            outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"))
        .WriteTo.Async(writeTo => writeTo.File("Infrastructure/Logs/log-.txt", rollingInterval: RollingInterval.Day));
});
#endregion

#region Rate Limit

builder.Services.AddRateLimiter(options =>
{
    options.AddPolicy("fixed", httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            httpContext.Request.Headers["X-Forwarded-For"].ToString(),
            _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 10,
                Window = TimeSpan.FromMinutes(1)
            }));
    options.OnRejected = async (context, token) =>
    {
        context.HttpContext.Response.StatusCode = 429;
        context.HttpContext.Response.ContentType = "text/plain";
        await context.HttpContext.Response.WriteAsync("Too many requests. Please try later again... ", token);
    };
});

#endregion

#region AutoMapper

builder.Services.AddAutoMapper(typeof(Program).Assembly);
var mapperConfig = new MapperConfiguration(mc => { mc.AddProfile(new Mapping()); });

#endregion

#region Database Configure

builder.Services.AddDbContext<DBContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

#endregion

#region SwaggerConfigure

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Project Books API",
        Description = "An API to manage books in the Project Books application",
        Contact = new OpenApiContact
        {
            Name = "Hao Pham",
            Email = "hao.pham@kyanon.digitial"
        },
        License = new OpenApiLicense
        {
            Name = "Use under KYANON"
        }
    });
    options.TagActionsBy(api => api.HttpMethod);
    options.OrderActionsBy(apiDesc =>
    {
        var httpMethodOrder = new Dictionary<string, int>
        {
            { "GET", 1 },
            { "POST", 2 },
            { "PUT", 3 },
            { "DELETE", 4 }
        };

        var httpMethod = apiDesc.HttpMethod;
        if (httpMethod == null)
        {
            throw new ArgumentNullException(nameof(httpMethod), "HTTP method cannot be null.");
        }

        var order = httpMethodOrder.ContainsKey(httpMethod) ? httpMethodOrder[httpMethod] : 5;

        return order.ToString("D2");
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: {token}\"",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header
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
            new List<string>()
        }
    });
    options.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Description = "API Key needed to access the endpoints. Example: \"X-Api-Key: {api_key}\"",
        Type = SecuritySchemeType.ApiKey,
        Name = "X-Api-Key",
        In = ParameterLocation.Header,
        Scheme = "ApiKeyScheme"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "ApiKey"
                }
            },
            new List<string>()
        }
    });
});
#endregion

#region Authorization and Authentication

var jwtSecretKey = builder.Configuration["ApplicationSettings:JWT_Secret"];
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey))
        };
        options.Events = new JwtBearerEvents
        {
            OnChallenge = async context =>
            {
                context.HandleResponse();
                await UnauthorizeCustomResponse.HandleUnauthorizedAsync(context.HttpContext);
            },
            OnForbidden = async context =>
            {
                await ForbiddenCustomReponse.HandleForbiddenAsync(context.HttpContext);
            }
        };
    });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Administrator", policy => policy.RequireRole("Administrator"));
    options.AddPolicy("User", policy => policy.RequireRole("User"));
    options.AddPolicy("Finance", policy => policy.RequireRole("Finance"));
    options.AddPolicy("Admin_HR", policy => policy.RequireRole("Administrator", "Human Resources"));
      
});

#endregion

#region CORS

// Add CORS services
// builder.Services.AddCors(options =>
// {
//     options.AddPolicy("AllowSpecificOrigin", builder =>
//     {
//         builder.WithOrigins("https://localhost:7220") // Specify the allowed origin(s)
//                .AllowAnyHeader() // Allow any header
//                .AllowAnyMethod(); // Allow any method (GET, POST, etc.)
//     });
// });
#endregion

#region API KEY
// builder.Services.AddTransient<ApiKeyMiddleware>();
#endregion

#region GlobalException


#endregion

#region InMemoryCache
builder.Services.AddMemoryCache();
#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{

    app.UseSwagger();
    app.UseSwaggerUI(op =>
    {
        op.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        op.EnableFilter();
    });
}
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DBContext>();
    dbContext.Database.Migrate();
}

#region Middlwares
app.UseMiddleware<SwaggerBasicAuthMiddleware>();
#endregion
app.UseExceptionHandler();
app.UseRouting();
app.UseRateLimiter();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapCarter();

app.Run();