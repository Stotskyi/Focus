using MediatR;
using Microsoft.AspNetCore.Mvc;
using Squirrel.Application.Sessions.CreateSession;
using Squirrel.Application.Sessions.GetAllSessions;

namespace Squirrel.Api.Controllers.Sessions;

[ApiController]
[Route("api/sessions")]
public class SessionController : ControllerBase
{
   private readonly ISender _sender;

   public SessionController(ISender sender)
   {
      _sender = sender;
   }
   
   [HttpGet("{id}")]
   public async Task<IActionResult> GetSessions(Guid id, CancellationToken cancellationToken)
   {
      var query = new GetAllSessionsQuery(id);
        
      var result = await _sender.Send(query, cancellationToken);

      return result.IsSuccess ? Ok(result.Value) : NotFound();
   }

   [HttpPost]
   public async Task<IActionResult> CreateSession([FromBody] CreateSessionRequest request, CancellationToken cancellationToken)
   {
      var command = new CreateSessionCommand(
         request.Id,
         request.Duration,
         request.IsFinished,
         request.Type,
         request.UserId);
      
      var result = await _sender.Send(command, cancellationToken);
      
      return result.IsSuccess ? Ok() : NotFound();
   }
}