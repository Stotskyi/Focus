using MediatR;
using Squirrel.Domain.Abstractions;

namespace Squirrel.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}