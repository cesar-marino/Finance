using Finance.Application.UseCases.User.Commons;
using MediatR;

namespace Finance.Application.UseCases.User.RevokeAccess
{
    public interface IRevokeAccessHandler : IRequestHandler<RevokeAccessRequest, UserResponse> { }
}