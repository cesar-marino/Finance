using Finance.Application.UseCases.User.Commons;
using MediatR;

namespace Finance.Application.UseCases.User.UpdateUsername
{
    public interface IUpdateUsernameHandler : IRequestHandler<UpdateUsernameRequest, UserResponse>
    {

    }
}