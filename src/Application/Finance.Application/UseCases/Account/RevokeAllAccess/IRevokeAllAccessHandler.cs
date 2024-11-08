using MediatR;

namespace Finance.Application.UseCases.Account.RevokeAllAccess
{
    public interface IRevokeAllAccessHandler : IRequestHandler<RevokeAllAccessRequest> { }
}