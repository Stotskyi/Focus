using Squirrel.Application.Abstractions.Messaging;
using Squirrel.Domain.Abstractions;

namespace Squirrel.Application.Sessions.GetAllSessions;

public record GetAllSessionsQuery(Guid UserId) : IQuery<IReadOnlyList<SessionsResponse>>;