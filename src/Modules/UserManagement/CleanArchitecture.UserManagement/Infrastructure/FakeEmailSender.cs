using CleanArchitecture.UserManagement.Contracts;

namespace CleanArchitecture.UserManagement.Infrastructure;

internal sealed class FakeEmailSender : IEmailSender
{
    public Task SendMailAsync(string text, string emailAddress, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}