using AutoMapper;
using CQ.AuthProvider.SDK.Abstractions.Accounts;
using CQ.AuthProvider.SDK.AuthProviderConnections;

namespace CQ.AuthProvider.SDK.Accounts;
internal sealed class AccountService(
    IMapper _mapper,
    IAuthProviderConnection _authProviderConnection) :
    IAccountService
{
    public async Task<Account> CreateForAsync(CreateAccountPassword account)
    {
        var request = _mapper.Map<CreateAccountPasswordRequest>(account);

        var successBody = await _authProviderConnection
            .CreateAccountForAsync(request)
            .ConfigureAwait(false);

        return _mapper.Map<Account>(successBody);
    }

    public async Task<AccountCreated> CreateAsync(CreateAccountPassword account)
    {
        var request = _mapper.Map<CreateAccountPasswordRequest>(account);

        var response = await _authProviderConnection
            .CreateAccountAsync(request)
            .ConfigureAwait(false);

        return _mapper.Map<AccountCreated>(response);
    }

    public async Task<Account> GetByTokenAsync(string token)
    {
        var response = await _authProviderConnection
            .GetByTokenAsync(token)
            .ConfigureAwait(false);

        return _mapper.Map<Account>(response);
    }
}
