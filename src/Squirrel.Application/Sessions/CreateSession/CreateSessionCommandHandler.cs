using Squirrel.Application.Abstractions.Messaging;
using Squirrel.Domain.Abstractions;
using Squirrel.Domain.Session;
using Type = Squirrel.Domain.Session.Type;

namespace Squirrel.Application.Sessions.CreateSession;

internal sealed class CreateSessionCommandHandler : ICommandHandler<CreateSessionCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISessionRepository _sessionRepository;

    public CreateSessionCommandHandler(ISessionRepository sessionRepository, IUnitOfWork unitOfWork)
    {
        _sessionRepository = sessionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(CreateSessionCommand request, CancellationToken cancellationToken)
    {
        var session = Session.Create(
            request.UserId,
            new Duration(request.Duration),
            new IsFinished(request.IsFinished),
            new Type(request.Type));
        
         _sessionRepository.Add(session);        
        
        await  _unitOfWork.SaveChangesAsync();
        
        return Result.Success();
    }
}