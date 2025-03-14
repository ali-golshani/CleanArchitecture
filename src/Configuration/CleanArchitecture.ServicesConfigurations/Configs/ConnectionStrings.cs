﻿using CleanArchitecture.Configurations;
using Framework.Exceptions;
using Microsoft.Extensions.Configuration;

namespace CleanArchitecture.ServicesConfigurations.Configs;

internal static class ConnectionStrings
{
    public static string CleanArchitectureConnectionString(this IConfiguration configuration)
    {
        return
            configuration.GetConnectionString(ConfigurationSections.ConnectionStrings.CleanArchitectureDb) ??
            throw new ProgrammerException("Connection String is not Set !");
    }
}