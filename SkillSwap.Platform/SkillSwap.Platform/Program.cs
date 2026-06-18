using Microsoft.EntityFrameworkCore;
using SkillSwap.Platform.Moderation.Application.CommandServices;
using SkillSwap.Platform.Moderation.Application.Internal.CommandServices;
using SkillSwap.Platform.Moderation.Application.Internal.QueryServices;
using SkillSwap.Platform.Moderation.Application.QueryServices;
using SkillSwap.Platform.Moderation.Domain.Repositories;
using SkillSwap.Platform.Moderation.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using SkillSwap.Platform.Shared.Domain.Repositories;
using SkillSwap.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using SkillSwap.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using SkillSwap.Platform.Workspace.Application.CommandServices;
using SkillSwap.Platform.Workspace.Application.Internal.CommandServices;
using SkillSwap.Platform.Workspace.Application.Internal.QueryServices;
using SkillSwap.Platform.Workspace.Application.QueryServices;
using SkillSwap.Platform.Workspace.Domain.Repositories;
using SkillSwap.Platform.Workspace.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

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

// Workspace Bounded Context
builder.Services.AddScoped<ISessionRepository, SessionRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<ISessionCommandService, SessionCommandService>();
builder.Services.AddScoped<IMessageCommandService, MessageCommandService>();
builder.Services.AddScoped<ISessionQueryService, SessionQueryService>();
builder.Services.AddScoped<IMessageQueryService, MessageQueryService>();

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