using Ardalis.SharedKernel;
using Ardalis.Specification.EntityFrameworkCore;

namespace Bengbenz.Embassy.eServices.Infrastructure.Data;

/// <inheritdoc />
public class EfRepository<T>(EmbassyDbContext dbContext)
    : RepositoryBase<T>(dbContext), IReadRepository<T>, IRepository<T>
    where T : class, IAggregateRoot;