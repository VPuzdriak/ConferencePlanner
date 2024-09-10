using ConferencePlanner.GraphQL.Data;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.GraphQL.Sessions;

[QueryType]
public static class SessionQueries
{
    public static async Task<IEnumerable<Session>> GetSessionsAsync(
        [Service] ApplicationDbContext dbContext,
        CancellationToken cancellationToken) =>
        await dbContext.Sessions.AsNoTracking().ToListAsync(cancellationToken);

    [NodeResolver]
    public static async Task<Session?> GetSessionByIdAsync(
        int id,
        ISessionByIdDataLoader sessionById,
        CancellationToken cancellationToken)
    {
        return await sessionById.LoadAsync(id, cancellationToken);
    }
}