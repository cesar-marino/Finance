using Finance.Application.UseCases.User.Commons;
using MediatR;

namespace Finance.Application.UseCases.User.RefreshToken
{
    public interface IRefreshTokenHandler : IRequestHandler<RefreshTokenRequest, UserResponse> { }
}