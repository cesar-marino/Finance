using MediatR;

namespace Finance.Application.UseCases.User.RevokeAllAccess
{
    public interface IRevokeAllAccessHandler : IRequestHandler<RevokeAllAccessRequest> { }
}