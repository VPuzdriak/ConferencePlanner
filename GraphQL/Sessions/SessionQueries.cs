using ConferencePlanner.GraphQL.Data;
using HotChocolate.Data.Sorting;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.GraphQL.Sessions;

[QueryType]
public static class SessionQueries
{
    [UsePaging]
    [UseFiltering]
    [UseSorting]
    public static IQueryable<Session> GetSessionsAsync(ApplicationDbContext dbContext, ISortingContext sortingContext)
    {
        var query = dbContext.Sessions.AsNoTracking();

        if (sortingContext.IsDefined)
        {
            return query;
        }

        return query.OrderBy(x => x.Title);
    }

    [NodeResolver]
    public static async Task<Session?> GetSessionByIdAsync(
        int id,
        ISessionByIdDataLoader sessionById,
        CancellationToken cancellationToken)
    {
        return await sessionById.LoadAsync(id, cancellationToken);
    }

    public static async Task<IEnumerable<Session>> GetSessionsByIdAsync(
        [ID<Session>] int[] ids,
        ISessionByIdDataLoader sessionById,
        CancellationToken cancellationToken)
    {
        return await sessionById.LoadRequiredAsync(ids, cancellationToken);
    }
}