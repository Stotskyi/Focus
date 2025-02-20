using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Squirrel.Domain.Session;
using Type = Squirrel.Domain.Session.Type;

namespace Squirrel.Infrastructure.Configuration;

internal sealed class SessionConfiguration : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        builder.ToTable("sessions");
        
        builder.HasKey(session => session.Id);
        
        builder.Property(session => session.IsFinished)
            .HasConversion(isFinished => isFinished.Value , value => new IsFinished(value));
        
        builder.Property(session => session.Duration)
            .HasConversion(duration => duration.Value , value => new Duration(value));

        builder.Property(session => session.Type)
            .HasConversion(type => type.Value, value => new Type(value));

        builder.Property(session => session.UserId);
    }
}