using Microsoft.EntityFrameworkCore;
using SkillSwap.Platform.Discovery.Application.CommandServices;
using SkillSwap.Platform.Discovery.Application.Internal.CommandServices;
using SkillSwap.Platform.Discovery.Application.Internal.QueryServices;
using SkillSwap.Platform.Discovery.Application.QueryServices;
using SkillSwap.Platform.Discovery.Domain.Repositories;
using SkillSwap.Platform.Discovery.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using SkillSwap.Platform.Moderation.Application.CommandServices;
using SkillSwap.Platform.Moderation.Application.Internal.CommandServices;
using SkillSwap.Platform.Moderation.Application.Internal.QueryServices;
using SkillSwap.Platform.Moderation.Application.QueryServices;
using SkillSwap.Platform.Moderation.Domain.Repositories;
using SkillSwap.Platform.Moderation.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using SkillSwap.Platform.Shared.Domain.Repositories;
using SkillSwap.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using SkillSwap.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddControllers();

// Localization
builder.Services.AddLocalization();

// OpenAPI / Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
});

// Database (MySQL)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySQL(connectionString!));

// Shared
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Moderation Bounded Context
builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<ISanctionRepository, SanctionRepository>();
builder.Services.AddScoped<IReportCommandService, ReportCommandService>();
builder.Services.AddScoped<IReportQueryService, ReportQueryService>();
builder.Services.AddScoped<ISanctionCommandService, SanctionCommandService>();
builder.Services.AddScoped<ISanctionQueryService, SanctionQueryService>();

// Discovery Bounded Context
builder.Services.AddScoped<ITutorRepository, TutorRepository>();
builder.Services.AddScoped<ITutorCommandService, TutorCommandService>();
builder.Services.AddScoped<ITutorQueryService, TutorQueryService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();