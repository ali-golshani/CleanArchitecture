using Framework.Exceptions;

namespace Infrastructure.CommoditySystem;

public class CommoditySystemException : ExternalException
{
    public CommoditySystemException(Exception innerException) : base(innerException)
    {
    }

    public override string Message => "خطا در ارتباط سیستم کالایی";
}
