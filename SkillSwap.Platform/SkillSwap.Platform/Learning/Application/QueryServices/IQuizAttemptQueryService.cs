using SkillSwap.Platform.Learning.Domain.Model.Aggregates;
using SkillSwap.Platform.Learning.Domain.Model.Queries;

namespace SkillSwap.Platform.Learning.Application.QueryServices;

/// <summary>
///     QuizAttempt query service interface
/// </summary>
public interface IQuizAttemptQueryService
{
    /// <summary>
    ///     Handle get quiz attempt by id query
    /// </summary>
    Task<QuizAttempt?> Handle(GetQuizAttemptByIdQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Handle get quiz attempts by learner id query
    /// </summary>
    Task<IEnumerable<QuizAttempt>> Handle(GetQuizAttemptsByLearnerIdQuery query, CancellationToken cancellationToken);

    /// <summary>
    ///     Handle get quiz attempts by quiz id query
    /// </summary>
    Task<IEnumerable<QuizAttempt>> Handle(GetQuizAttemptsByQuizIdQuery query, CancellationToken cancellationToken);
}