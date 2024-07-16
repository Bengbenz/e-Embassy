// MIT License.
// Copyright (c) 2024 Caleb BENGUELET, @Bengbenz
// See more info in [LICENSE] file.

using Bengbenz.Embassy.eServices.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Bengbenz.Embassy.eServices.IntegrationTests;

public abstract class BaseEfRepoTestFixture<T> where T : DbContext
{
    protected T _dbContext;

    protected BaseEfRepoTestFixture()
    {
        var options = CreateNewContextOptions();
        _dbContext = (Activator.CreateInstance(typeof(T), options) as T)!;
    }

    protected DbContextOptions<T> CreateNewContextOptions()
    {
        // Create a fresh service provider, and therefore a fresh
        // InMemory database instance.
        var serviceProvider = new ServiceCollection()
            .AddEntityFrameworkInMemoryDatabase()
            .BuildServiceProvider();

        // Create a new options instance telling the context to use an
        // InMemory database and the new service provider.
        var builder = new DbContextOptionsBuilder<T>();
        builder.UseInMemoryDatabase(nameof(T))
            .UseInternalServiceProvider(serviceProvider);

        return builder.Options;
    }
}