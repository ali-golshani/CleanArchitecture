namespace CleanArchitecture.Ordering.Domain.Services;

public interface IRegisterOrderService
{
    Task<Result<Order>> RegisterOrder(RegisterOrderRequest request);
}