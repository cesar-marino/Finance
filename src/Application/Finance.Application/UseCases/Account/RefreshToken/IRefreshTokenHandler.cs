using Finance.Application.UseCases.Account.Commons;
using MediatR;

namespace Finance.Application.UseCases.Account.RefreshToken
{
    public interface IRefreshTokenHandler : IRequestHandler<RefreshTokenRequest, AccountResponse> { }
}