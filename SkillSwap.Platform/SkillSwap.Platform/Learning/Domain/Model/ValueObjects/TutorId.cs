namespace SkillSwap.Platform.Learning.Domain.Model.ValueObjects;

public record TutorId(int tutorId)
{
    public TutorId() : this(0)
    {
    }
}
