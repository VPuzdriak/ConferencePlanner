using ConferencePlanner.GraphQL.Data;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.GraphQL.Tracks;

[QueryType]
public static class TrackQueries
{
    public static async Task<IEnumerable<Track>> GetTracksAsync(
        [Service] ApplicationDbContext dbContext,
        CancellationToken cancellationToken)
    {
        return await dbContext.Tracks.AsNoTracking().ToListAsync(cancellationToken);
    }

    [NodeResolver]
    public static async Task<Track?> GetTrackByIdAsync(
        int id,
        ITrackByIdDataLoader trackById,
        CancellationToken cancellationToken)
    {
        return await trackById.LoadAsync(id, cancellationToken);
    }
}