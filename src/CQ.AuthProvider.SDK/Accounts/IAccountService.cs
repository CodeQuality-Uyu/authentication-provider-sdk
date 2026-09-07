using CQ.AuthProvider.SDK.Sessions;

namespace CQ.AuthProvider.SDK.Accounts;

public interface IAccountService
{
    /// <summary>
    /// Autorregistro público. El email ya se tiene que haber verificado antes (ver
    /// <c>CreateAccountPasswordArgs.VerificationToken</c>/<c>VerificationCode</c>) — con eso,
    /// la cuenta se crea ya verificada y loguea de una.
    /// </summary>
    Task<SessionCreated> CreateAsync(CreateAccountPasswordArgs args);

    Task<AccountCreated> CreateForAsync(CreateAccountForArgs args, AccountLogged accountLogged);

    Task<AccountCreated> CreateForWithSubscriptionAsync(CreateAccountForArgs args);
}