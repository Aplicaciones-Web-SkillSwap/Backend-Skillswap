using SkillSwap.Platform.Learning.Domain.Model.Aggregates;
using SkillSwap.Platform.Learning.Domain.Model.Commands;
using SkillSwap.Platform.Learning.Domain.Model.ValueObjects;
using SkillSwap.Platform.Shared.Application.Model;

namespace SkillSwap.Platform.Learning.Application.CommandServices;

public interface IQuizCommandService
{
    Task<Result<Quiz>> Handle(CreateQuizCommand command, CancellationToken cancellationToken);
    Task<Result> Handle(UpdateQuizInfoCommand command, CancellationToken cancellationToken);
    Task<Result> Handle(UpdateQuestionInQuizCommand command, CancellationToken cancellationToken);
    Task<Result> Handle(DeleteQuizCommand command, CancellationToken cancellationToken);
    Task<Result<Question>> Handle(AddQuestionToQuizCommand command, CancellationToken cancellationToken);
    Task<Result> Handle(RemoveQuestionFromQuizCommand command, CancellationToken cancellationToken);
}