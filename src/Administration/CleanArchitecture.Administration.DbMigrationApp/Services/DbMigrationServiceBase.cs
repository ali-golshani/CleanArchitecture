﻿using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Administration.DbMigrationApp.Services;

internal abstract class DbMigrationServiceBase(IServiceProvider serviceProvider)
{
    protected readonly IServiceProvider serviceProvider = serviceProvider;

    protected T Service<T>() where T : notnull => serviceProvider.GetRequiredService<T>();

    protected static void PrintConnectionString(string? connectionString)
    {
        Console.WriteLine($"ConnectionString.Contains(Server=.) = {connectionString?.Contains("Server=.")}");
    }
}
