using SkillSwap.Platform.Discovery.Domain.Model.Commands;
using SkillSwap.Platform.Discovery.Domain.Model.ValueObjects;

namespace SkillSwap.Platform.Discovery.Domain.Model.Aggregates;

/// <summary>
///     Tutor Aggregate Root
/// </summary>
/// <remarks>
///     This class represents the Tutor aggregate root.
///     It contains the properties and methods to manage a tutor profile.
/// </remarks>
public partial class Tutor
{
    public Tutor()
    {
        TutorUserId = new TutorId();
        Name = string.Empty;
        University = string.Empty;
        Career = string.Empty;
        Bio = string.Empty;
        TutorSkills = new TutorSkills();
        AvatarUrl = string.Empty;
        ExperienceYears = 0;
        MainSubject = string.Empty;
        Rating = 0.0;
        ReviewCount = 0;
        Verified = false;
        Online = false;
        Visible = true;
    }

    public Tutor(CreateTutorCommand command)
    {
        TutorUserId = new TutorId(command.UserId);
        Name = command.Name;
        University = command.University;
        Career = command.Career;
        Bio = command.Bio;
        TutorSkills = new TutorSkills(command.Skills);
        AvatarUrl = command.AvatarUrl;
        ExperienceYears = command.ExperienceYears;
        MainSubject = command.MainSubject;
        Rating = 0.0;
        ReviewCount = 0;
        Verified = false;
        Online = false;
        Visible = true;
    }

    public int Id { get; private set; }
    public TutorId TutorUserId { get; private set; }
    public string Name { get; private set; }
    public string University { get; private set; }
    public string Career { get; private set; }
    public string Bio { get; private set; }
    public TutorSkills TutorSkills { get; private set; }
    public string AvatarUrl { get; private set; }
    public int ExperienceYears { get; private set; }
    public string MainSubject { get; private set; }
    public double Rating { get; private set; }
    public int ReviewCount { get; private set; }
    public bool Verified { get; private set; }
    public bool Online { get; private set; }
    public bool Visible { get; private set; }

    public void Update(UpdateTutorCommand command)
    {
        Bio = command.Bio;
        TutorSkills = new TutorSkills(command.Skills);
        AvatarUrl = command.AvatarUrl;
        MainSubject = command.MainSubject;
        Visible = command.Visible;
    }
}