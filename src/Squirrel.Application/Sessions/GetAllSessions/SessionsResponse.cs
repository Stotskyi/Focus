namespace Squirrel.Application.Sessions.GetAllSessions;

public record SessionsResponse(int Duration, bool IsFinished, int Type);