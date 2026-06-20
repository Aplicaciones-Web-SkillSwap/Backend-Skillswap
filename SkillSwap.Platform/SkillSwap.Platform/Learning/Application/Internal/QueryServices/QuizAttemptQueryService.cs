using SkillSwap.Platform.Learning.Application.QueryServices;
using SkillSwap.Platform.Learning.Domain.Model.Aggregates;
using SkillSwap.Platform.Learning.Domain.Model.Queries;
using SkillSwap.Platform.Learning.Domain.Repositories;

namespace SkillSwap.Platform.Learning.Application.Internal.QueryServices;

/// <summary>
///     QuizAttempt query service
/// </summary>
/// <param name="quizAttemptRepository">
///     QuizAttempt repository
/// </param>
public class QuizAttemptQueryService(IQuizAttemptRepository quizAttemptRepository) : IQuizAttemptQueryService
{
    /// <inheritdoc />
    public async Task<QuizAttempt?> Handle(GetQuizAttemptByIdQuery query, CancellationToken cancellationToken)
    {
        return await quizAttemptRepository.FindByIdAsync(query.AttemptId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QuizAttempt>> Handle(GetQuizAttemptsByLearnerIdQuery query, CancellationToken cancellationToken)
    {
        return await quizAttemptRepository.FindByLearnerIdAsync(query.LearnerId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<QuizAttempt>> Handle(GetQuizAttemptsByQuizIdQuery query, CancellationToken cancellationToken)
    {
        return await quizAttemptRepository.FindByQuizIdAsync(query.QuizId, cancellationToken);
    }
}