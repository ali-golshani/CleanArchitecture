using Framework.Exceptions;

namespace Infrastructure.CommoditySystem;

public sealed class CommoditySystemException(Exception innerException) 
    : ExternalException(Resources.Messages.CommoditySystemException, innerException)
{
}
