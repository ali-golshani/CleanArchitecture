using CleanArchitecture.Audit.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Audit.Persistence;

internal static class RequestParametersConfiguration
{
    public static void Configure(this ComplexPropertyBuilder<RequestParameters> builder)
    {
        builder.Property(x => x.OrderId).HasColumnName(nameof(RequestParameters.OrderId));
    }
}
