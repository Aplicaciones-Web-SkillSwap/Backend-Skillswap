using SkillSwap.Platform.Learning.Domain.Model.Aggregates;
using SkillSwap.Platform.Learning.Domain.Model.Commands;
using SkillSwap.Platform.Learning.Domain.Model.Queries;

namespace SkillSwap.Platform.Learning.Application.QueryServices;

public interface IQuizQueryService
{
    Task<IEnumerable<Quiz>> Handle(GetAllQuizzesQuery query, CancellationToken cancellationToken);
    
    Task<Quiz?> Handle(GetQuizByIdQuery query, CancellationToken cancellationToken);
}