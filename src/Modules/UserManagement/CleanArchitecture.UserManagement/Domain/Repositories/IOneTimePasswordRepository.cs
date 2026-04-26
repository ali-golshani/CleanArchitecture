namespace CleanArchitecture.UserManagement.Domain.Repositories;

internal interface IOneTimePasswordRepository
{
    Task<OneTimePassword?> Find(Guid id);
    void Add(OneTimePassword otp);
}
