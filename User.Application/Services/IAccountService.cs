using User.Application.DTOs;

namespace User.Application.Services;

public interface IAccountService
{
    public Task<AuthenticatedAccountInfoDto> AuthenticationAccount(AuthenticationAccountDto authenticationAccount);
    public Task CreateAccount(NewAccountModel user);
}