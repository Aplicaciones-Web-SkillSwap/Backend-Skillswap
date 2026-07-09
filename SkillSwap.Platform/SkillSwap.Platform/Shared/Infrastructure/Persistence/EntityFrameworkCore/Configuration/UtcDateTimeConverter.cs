using System;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace SkillSwap.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

/// <summary>
///     MySQL's DATETIME columns store no timezone offset, so EF Core reads every value back with
///     DateTimeKind.Unspecified. Since every DateTime this app writes is already a true UTC instant
///     (DateTime.UtcNow server-side, or a client-submitted ISO string with a 'Z'/offset), this
///     converter re-tags reads as Utc so JSON responses include the offset and clients can convert
///     back to local time correctly instead of double-interpreting an already-local value as local again.
/// </summary>
public class UtcDateTimeConverter : ValueConverter<DateTime, DateTime>
{
    public UtcDateTimeConverter() : base(
        v => v,
        v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
    {
    }
}
