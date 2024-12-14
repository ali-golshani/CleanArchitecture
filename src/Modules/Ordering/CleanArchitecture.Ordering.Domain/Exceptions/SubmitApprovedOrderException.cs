using Framework.Exceptions;

namespace CleanArchitecture.Ordering.Domain.Exceptions;

public class SubmitApprovedOrderException(int orderId) : DomainException
{
    public int OrderId { get; } = orderId;
    public override string Message => "سفارش تایید شده است. درخواست ارسال سفارش نامعتبر است";
}
