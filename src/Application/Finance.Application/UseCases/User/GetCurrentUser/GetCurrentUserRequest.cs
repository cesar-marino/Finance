using Finance.Application.UseCases.User.Commons;
using MediatR;

namespace Finance.Application.UseCases.User.GetCurrentUser
{
    public class GetCurrentUserRequest(string accessToken) : IRequest<UserResponse>
    {
        public string AccessToken { get; } = accessToken;
    }
}