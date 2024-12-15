using Framework.Exceptions;

namespace CleanArchitecture.Ordering.Domain.Exceptions;

public class InvalidOrderStatusException(int orderId) : ProgrammerException
{
    public int OrderId { get; } = orderId;
    public override string Message => "وضعیت سفارش نامشخص است. لطفا با پشتیبانی سیستم تماس بگیرید";
}
