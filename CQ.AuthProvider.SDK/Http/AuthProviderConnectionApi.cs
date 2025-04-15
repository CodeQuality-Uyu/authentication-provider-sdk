using CQ.Utility;
using Microsoft.Extensions.Options;

namespace CQ.AuthProvider.SDK.Http;

internal sealed class AuthProviderConnectionApi(IOptions<AuthProviderSection> _section)
    : ConcreteHttpClient<CqAuthErrorApi>(_section.Value.Server)
{
    protected override Exception? ProcessConcreteError(CqAuthErrorApi error) => new CqAuthException(error);
}
