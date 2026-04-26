using CleanArchitecture.UserManagement.Contracts;

namespace CleanArchitecture.UserManagement.Infrastructure;

internal sealed class FakeSmsSender : ISmsSender
{
    public Task SendMessageAsync(string text, string mobileNumber, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
