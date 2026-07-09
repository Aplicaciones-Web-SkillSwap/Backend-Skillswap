using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SkillSwap.Platform.Iam.Application.CommandServices;
using SkillSwap.Platform.Iam.Application.Internal.OutboundServices;
using SkillSwap.Platform.Iam.Domain.Model;
using SkillSwap.Platform.Iam.Domain.Model.Aggregates;
using SkillSwap.Platform.Iam.Domain.Model.Commands;
using SkillSwap.Platform.Iam.Domain.Repositories;
using SkillSwap.Platform.Shared.Application.Model;
using SkillSwap.Platform.Shared.Domain.Repositories;
using SkillSwap.Platform.Shared.Resources.Errors;

namespace SkillSwap.Platform.Iam.Application.Internal.CommandServices;

/// <summary>
///     User command service
/// </summary>
/// <param name="userRepository">User repository</param>
/// <param name="tokenService">Token service</param>
/// <param name="hashingService">Hashing service</param>
/// <param name="unitOfWork">Unit of work</param>
/// <param name="localizer">String localizer for error messages</param>
public partial class UserCommandService(
    IUserRepository userRepository,
    ITokenService tokenService,
    IHashingService hashingService,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessage> localizer)
    : IUserCommandService
{
    private readonly IStringLocalizer<ErrorMessage> _localizer = localizer;

    /// <inheritdoc />
    public async Task<Result> Handle(SignUpCommand command, CancellationToken cancellationToken)
    {
        if (!InstitutionalEmailRegex().IsMatch(command.Email))
            return Result.Failure(IamError.InvalidInstitutionalEmail,
                _localizer[nameof(IamError.InvalidInstitutionalEmail)]);

        if (await userRepository.ExistsByUsernameAsync(command.Username, cancellationToken))
            return Result.Failure(IamError.UsernameAlreadyTaken, _localizer[nameof(IamError.UsernameAlreadyTaken)]);

        if (await userRepository.ExistsByEmailAsync(command.Email, cancellationToken))
            return Result.Failure(IamError.EmailAlreadyTaken, _localizer[nameof(IamError.EmailAlreadyTaken)]);

        var hashedPassword = hashingService.HashPassword(command.Password);
        var user = new User(command.Username, command.Email, hashedPassword, command.Role);
        try
        {
            await userRepository.AddAsync(user, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result.Success();
        }
        catch (OperationCanceledException)
        {
            return Result.Failure(IamError.OperationCancelled, _localizer[nameof(IamError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result.Failure(IamError.DatabaseError, _localizer[nameof(IamError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result.Failure(IamError.InternalServerError, _localizer[nameof(IamError.InternalServerError)]);
        }
    }

    /// <inheritdoc />
    public async Task<Result<(User User, string Token)>> Handle(SignInCommand command,
        CancellationToken cancellationToken)
    {
        var user = await userRepository.FindByUsernameAsync(command.Username, cancellationToken);

        if (user is null || !hashingService.VerifyPassword(command.Password, user.PasswordHash))
            return Result<(User User, string Token)>.Failure(IamError.InvalidCredentials,
                _localizer[nameof(IamError.InvalidCredentials)]);

        var token = tokenService.GenerateToken(user);
        return Result<(User User, string Token)>.Success((user, token));
    }

    /// <inheritdoc />
    public async Task<Result<User>> Handle(UpdateUserBioCommand command, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindByIdAsync(command.UserId, cancellationToken);
        if (user is null)
            return Result<User>.Failure(IamError.UserNotFound, _localizer[nameof(IamError.UserNotFound)]);

        if (user.Id != command.ActorUserId)
            return Result<User>.Failure(IamError.NotProfileOwner, _localizer[nameof(IamError.NotProfileOwner)]);

        user.UpdateBio(command.Bio);
        try
        {
            userRepository.Update(user);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<User>.Success(user);
        }
        catch (OperationCanceledException)
        {
            return Result<User>.Failure(IamError.OperationCancelled, _localizer[nameof(IamError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<User>.Failure(IamError.DatabaseError, _localizer[nameof(IamError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<User>.Failure(IamError.InternalServerError, _localizer[nameof(IamError.InternalServerError)]);
        }
    }

    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.edu\.pe$", RegexOptions.IgnoreCase)]
    private static partial Regex InstitutionalEmailRegex();
}
