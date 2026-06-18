using SkillSwap.Platform.Discovery.Application.QueryServices;
using SkillSwap.Platform.Discovery.Domain.Model.Aggregates;
using SkillSwap.Platform.Discovery.Domain.Model.Queries;
using SkillSwap.Platform.Discovery.Domain.Repositories;

namespace SkillSwap.Platform.Discovery.Application.Internal.QueryServices;

/// <summary>
///     Tutor query service
/// </summary>
/// <param name="tutorRepository">
///     Tutor repository
/// </param>
public class TutorQueryService(ITutorRepository tutorRepository) : ITutorQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<Tutor>> Handle(GetAllTutorsQuery query, CancellationToken cancellationToken)
    {
        return await tutorRepository.ListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Tutor?> Handle(GetTutorByIdQuery query, CancellationToken cancellationToken)
    {
        return await tutorRepository.FindByIdAsync(query.TutorId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Tutor?> Handle(GetTutorByUserIdQuery query, CancellationToken cancellationToken)
    {
        return await tutorRepository.FindByUserIdAsync(query.UserId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Tutor>> Handle(GetTutorsBySkillQuery query, CancellationToken cancellationToken)
    {
        return await tutorRepository.FindBySkillAsync(query.Skill, cancellationToken);
    }
}