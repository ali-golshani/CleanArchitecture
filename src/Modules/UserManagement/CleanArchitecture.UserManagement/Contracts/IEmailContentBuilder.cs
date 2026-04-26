namespace CleanArchitecture.UserManagement.Contracts;

public interface IEmailContentBuilder
{
    string BuildOtpContent(string otp);
}
