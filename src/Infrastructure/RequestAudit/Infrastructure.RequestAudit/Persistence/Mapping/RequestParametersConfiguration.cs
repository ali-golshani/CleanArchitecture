using Infrastructure.RequestAudit.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.RequestAudit.Persistence.Mapping;

internal static class RequestParametersConfiguration
{
    public static void Configure(this ComplexPropertyBuilder<RequestParameters> builder)
    {
        builder.Property(x => x.OrderId).HasColumnName(nameof(RequestParameters.OrderId));
    }
}
