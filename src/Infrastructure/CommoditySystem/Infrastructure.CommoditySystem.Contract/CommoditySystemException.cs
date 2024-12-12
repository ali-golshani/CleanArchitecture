using Framework.Exceptions;

namespace Infrastructure.CommoditySystem;

public class CommoditySystemException(Exception innerException) : ExternalException(innerException)
{
    public override string Message => "خطا در ارتباط سیستم کالایی";
}
