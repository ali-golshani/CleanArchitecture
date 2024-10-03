namespace CleanArchitecture.Ordering.Domain.Services;

public interface IRegisterOrderService
{
    Task<Result<Empty>> RegisterOrder(RegisterOrderRequest request);
}