using CQ.Utility;
using Microsoft.Extensions.Options;

namespace CQ.AuthProvider.SDK.Http;

internal sealed class AuthProviderClient(IOptions<AuthProviderSection> section)
    : ConcreteHttpClient<CqAuthErrorApi>(section.Value.Server)
{
    protected override Exception? ProcessConcreteError(CqAuthErrorApi error) => new CqAuthException(error);
}
