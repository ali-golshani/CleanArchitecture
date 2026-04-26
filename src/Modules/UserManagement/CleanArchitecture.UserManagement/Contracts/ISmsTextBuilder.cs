namespace CleanArchitecture.UserManagement.Contracts;

public interface ISmsTextBuilder
{
    string BuildOtpText(string otp);
}
