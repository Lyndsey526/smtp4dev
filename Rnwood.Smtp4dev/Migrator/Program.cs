using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Rnwood.Smtp4dev.Data;
using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Running migrations...");

        var host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                services.AddDbContext<Smtp4devDbContext>(options =>
                    options.UseSqlServer("Server=tt.oscamerica.com;Database=smtp4dev;User Id=smtpuser;Password=DLgR!7t^mN2#Lk90pBv@;TrustServerCertificate=True"));
            })
            .Build();

        using var scope = host.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<Smtp4devDbContext>();
        dbContext.Database.Migrate();

        Console.WriteLine("Migration complete.");
    }
}
