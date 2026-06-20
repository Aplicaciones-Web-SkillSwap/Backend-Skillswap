using SkillSwap.Platform.Learning.Application.QueryServices;
using SkillSwap.Platform.Learning.Domain.Model.Aggregates;
using SkillSwap.Platform.Learning.Domain.Model.Queries;
using SkillSwap.Platform.Learning.Domain.Repositories;

namespace SkillSwap.Platform.Learning.Application.Internal.QueryServices;

public class QuizQueryService(IQuizRepository quizRepository) : IQuizQueryService
{
    public async Task<IEnumerable<Quiz>> Handle(GetAllQuizzesQuery query, CancellationToken cancellationToken)
    {
        return await quizRepository.ListAsync(cancellationToken);
    }

    public async Task<Quiz?> Handle(GetQuizByIdQuery query, CancellationToken cancellationToken)
    {
        return await quizRepository.FindByIdAsync(query.Id, cancellationToken);
    }
}