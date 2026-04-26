namespace CleanArchitecture.UserManagement.Contracts;

public interface ISmsSender
{
    Task SendMessageAsync(string text, string mobileNumber, CancellationToken cancellationToken);
}
