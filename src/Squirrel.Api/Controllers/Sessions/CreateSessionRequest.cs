namespace Squirrel.Api.Controllers.Sessions;

public sealed record CreateSessionRequest(Guid Id, int Duration, bool IsFinished, int Type, Guid UserId);