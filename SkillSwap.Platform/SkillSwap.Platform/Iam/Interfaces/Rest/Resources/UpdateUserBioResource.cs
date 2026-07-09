namespace SkillSwap.Platform.Iam.Interfaces.Rest.Resources;

/// <summary>
///     Resource for updating a user's learner bio
/// </summary>
/// <param name="Bio">The new bio text</param>
public record UpdateUserBioResource(string Bio);
