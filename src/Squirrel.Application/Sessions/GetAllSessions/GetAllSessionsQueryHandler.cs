using System.Collections.Immutable;
using Squirrel.Application.Abstractions.Messaging;
using Squirrel.Domain.Abstractions;
using Squirrel.Domain.Session;

namespace Squirrel.Application.Sessions.GetAllSessions;

internal sealed class GetAllSessionsQueryHandler : IQueryHandler<GetAllSessionsQuery, IReadOnlyList<SessionsResponse>>
{
    private readonly ISessionRepository _sessionRepository;

    public GetAllSessionsQueryHandler(ISessionRepository sessionRepository)
    {
        _sessionRepository = sessionRepository;
    }

    public async Task<Result<IReadOnlyList<SessionsResponse>>> Handle(GetAllSessionsQuery request, CancellationToken cancellationToken)
    {
        var sessions =  await _sessionRepository.GetAllSessionsAsync(request.UserId, cancellationToken);

        var sessionResponses = sessions
            .Select(sessions => new SessionsResponse(
                sessions.Duration.Value,
                sessions.IsFinished.Value,
                sessions.Type.Value)).ToList();

        return sessionResponses;
    }
}