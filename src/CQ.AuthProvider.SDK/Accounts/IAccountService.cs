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

    /// <summary>
    /// Saca la cuenta logueada del app con la que se logueo y cierra sus sesiones ahi. Si no le
    /// queda ninguna otra app, el Auth Provider borra la cuenta entera y el email queda libre.
    /// </summary>
    Task DeleteMeAsync(AccountLogged accountLogged);
}