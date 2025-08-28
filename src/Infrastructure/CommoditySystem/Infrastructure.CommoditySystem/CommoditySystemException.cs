using Framework.Exceptions;

namespace Infrastructure.CommoditySystem;

public class CommoditySystemException(Exception innerException) 
    : ExternalException(Resources.Messages.CommoditySystemException, innerException)
{
}
