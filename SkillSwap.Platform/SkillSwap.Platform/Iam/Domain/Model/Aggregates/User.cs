using System.Text.Json.Serialization;
using SkillSwap.Platform.Iam.Domain.Model.ValueObjects;

namespace SkillSwap.Platform.Iam.Domain.Model.Aggregates;

/// <summary>
///     User aggregate root
/// </summary>
/// <remarks>
///     This class represents a registered SkillSwap user. Student accounts are hybrid
///     (act as both learner and tutor depending on context); Coordinator is a separate role.
/// </remarks>
public partial class User(string username, string email, string passwordHash, UserRole role)
{
    public User() : this(string.Empty, string.Empty, string.Empty, UserRole.Student)
    {
    }

    public int Id { get; }
    public string Username { get; private set; } = username;
    public string Email { get; private set; } = email;

    [JsonIgnore] public string PasswordHash { get; private set; } = passwordHash;

    public UserRole Role { get; private set; } = role;
    public bool IsVerified { get; private set; }
    public string Bio { get; private set; } = string.Empty;

    /// <summary>
    ///     Update the password hash
    /// </summary>
    /// <param name="passwordHash">The new password hash</param>
    /// <returns>The updated user</returns>
    public User UpdatePasswordHash(string passwordHash)
    {
        PasswordHash = passwordHash;
        return this;
    }

    /// <summary>
    ///     Update the user's learner bio
    /// </summary>
    /// <param name="bio">The new bio text</param>
    /// <returns>The updated user</returns>
    public User UpdateBio(string bio)
    {
        Bio = bio;
        return this;
    }

    /// <summary>
    ///     Mark the user's institutional email as verified
    /// </summary>
    /// <returns>The updated user</returns>
    public User Verify()
    {
        IsVerified = true;
        return this;
    }
}
