using Microsoft.EntityFrameworkCore;
using SkillSwap.Platform.Iam.Application.CommandServices;
using SkillSwap.Platform.Iam.Application.Internal.CommandServices;
using SkillSwap.Platform.Iam.Application.Internal.OutboundServices;
using SkillSwap.Platform.Iam.Application.Internal.QueryServices;
using SkillSwap.Platform.Iam.Application.QueryServices;
using SkillSwap.Platform.Iam.Domain.Repositories;
using SkillSwap.Platform.Iam.Infrastructure.Hashing.BCrypt.Services;
using SkillSwap.Platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using SkillSwap.Platform.Iam.Infrastructure.Pipeline.Middleware.Extensions;
using SkillSwap.Platform.Iam.Infrastructure.Tokens.Jwt.Configuration;
using SkillSwap.Platform.Iam.Infrastructure.Tokens.Jwt.Services;
using SkillSwap.Platform.Discovery.Application.CommandServices;
using SkillSwap.Platform.Discovery.Application.Internal.CommandServices;
using SkillSwap.Platform.Discovery.Application.Internal.QueryServices;
using SkillSwap.Platform.Discovery.Application.QueryServices;
using SkillSwap.Platform.Discovery.Domain.Repositories;
using SkillSwap.Platform.Discovery.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using SkillSwap.Platform.Learning.Application.CommandServices;
using SkillSwap.Platform.Learning.Application.Internal.CommandServices;
using SkillSwap.Platform.Learning.Application.Internal.QueryServices;
using SkillSwap.Platform.Learning.Application.QueryServices;
using SkillSwap.Platform.Learning.Domain.Repositories;
using SkillSwap.Platform.Learning.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
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
using SkillSwap.Platform.Reputation.Application.CommandServices;
using SkillSwap.Platform.Reputation.Application.Internal.CommandServices;
using SkillSwap.Platform.Reputation.Application.Internal.QueryServices;
using SkillSwap.Platform.Reputation.Application.QueryServices;
using SkillSwap.Platform.Reputation.Domain.Repositories;
using SkillSwap.Platform.Reputation.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using SkillSwap.Platform.Payments.Application.CommandServices;
using SkillSwap.Platform.Payments.Application.Internal.CommandServices;
using SkillSwap.Platform.Payments.Application.Internal.QueryServices;
using SkillSwap.Platform.Payments.Application.QueryServices;
using SkillSwap.Platform.Payments.Domain.Repositories;
using SkillSwap.Platform.Payments.Infrastructure.Persistence.EntityFrameworkCore.Repositories;


var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddControllers();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

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

// Iam Bounded Context
builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IHashingService, HashingService>();

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

// Workspace Bounded Context
builder.Services.AddScoped<ISessionRepository, SessionRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<ISessionCommandService, SessionCommandService>();
builder.Services.AddScoped<IMessageCommandService, MessageCommandService>();
builder.Services.AddScoped<ISessionQueryService, SessionQueryService>();
builder.Services.AddScoped<IMessageQueryService, MessageQueryService>();

// Reputation Bounded Context
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IReviewCommandService, ReviewCommandService>();
builder.Services.AddScoped<IReviewQueryService, ReviewQueryService>();

// Payments Bounded Context
builder.Services.AddScoped<IWalletRepository, WalletRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IWalletCommandService, WalletCommandService>();
builder.Services.AddScoped<ITransactionCommandService, TransactionCommandService>();
builder.Services.AddScoped<IWalletQueryService, WalletQueryService>();
builder.Services.AddScoped<ITransactionQueryService, TransactionQueryService>();
builder.Services.AddScoped<IDonationCommandService, DonationCommandService>();

// Learning Bounded Context
builder.Services.AddScoped<IQuizCommandService, QuizCommandService>();
builder.Services.AddScoped<IQuizQueryService, QuizQueryService>();
builder.Services.AddScoped<IQuizRepository, QuizRepository>();
builder.Services.AddScoped<IQuizAttemptRepository, QuizAttemptRepository>();
builder.Services.AddScoped<IQuizAttemptCommandService, QuizAttemptCommandService>();
builder.Services.AddScoped<IQuizAttemptQueryService, QuizAttemptQueryService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseRequestAuthorization();

app.UseAuthorization();

app.MapControllers();

app.Run();