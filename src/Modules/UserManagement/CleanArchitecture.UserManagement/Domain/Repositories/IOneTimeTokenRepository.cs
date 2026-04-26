namespace CleanArchitecture.UserManagement.Domain.Repositories;

internal interface IOneTimeTokenRepository
{
    Task<OneTimeToken?> Find(Guid id);
    void Add(OneTimeToken ott);
}
