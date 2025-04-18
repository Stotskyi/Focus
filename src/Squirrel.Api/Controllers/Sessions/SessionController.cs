using MediatR;
using Microsoft.AspNetCore.Mvc;
using Squirrel.Application.Sessions.CreateSession;

namespace Squirrel.Api.Controllers.Sessions;

[ApiController]
[Route("api/v1/sessions")]
public class SessionController : ControllerBase
{
   private readonly ISender _sender;

   public SessionController(ISender sender)
   {
      _sender = sender;
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
   
   [HttpGet]
   public IActionResult GetJoinedSessions()
   {
      return Ok(new { message = "hello world i woked in fucking ios app" });
   }

}