namespace CleanArchitecture.UserManagement.Contracts;

public interface IEmailSender
{
    Task SendMailAsync(string text, string emailAddress, CancellationToken cancellationToken);
}
