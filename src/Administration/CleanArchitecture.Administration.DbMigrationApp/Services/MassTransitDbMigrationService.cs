﻿using MassTransit.SqlTransport;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Administration.DbMigrationApp.Services;

internal sealed class MassTransitDbMigrationService(IServiceProvider serviceProvider) : DbMigrationServiceBase(serviceProvider)
{
    public void Migrate()
    {
        Console.WriteLine("MassTransitDbContext ...");
        using (var db = Service<Framework.MassTransit.MassTransitDbContext>())
        {
            var cs = db.Database.GetConnectionString();
            PrintConnectionString(cs);
            db.Database.Migrate();
        }
        Console.WriteLine("Migration Finished .");
    }

    public void MigrateSqlTransport()
    {
        Console.WriteLine("MassTransitSqlTransport ...");
        Service<SqlTransportMigrationHostedService>().StartAsync(default).Wait();
        Console.WriteLine("Migration Finished .");
    }
}
