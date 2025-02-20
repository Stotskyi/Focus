using Squirrel.Application.Abstractions.Messaging;

namespace Squirrel.Application.Sessions.CreateSession;

public sealed record CreateSessionCommand(Guid Id, int Duration, bool IsFinished, int Type, Guid UserId) : ICommand;