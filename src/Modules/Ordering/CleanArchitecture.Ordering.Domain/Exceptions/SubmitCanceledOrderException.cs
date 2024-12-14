using Framework.Exceptions;

namespace CleanArchitecture.Ordering.Domain.Exceptions;

public class SubmitCanceledOrderException(int orderId) : DomainException
{
    public int OrderId { get; } = orderId;
    public override string Message => "سفارش لغو شده است. درخواست ارسال سفارش نامعتبر است";
}
