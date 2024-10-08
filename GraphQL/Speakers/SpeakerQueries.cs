using ConferencePlanner.GraphQL.Data;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.GraphQL.Speakers;

[QueryType]
public static class SpeakerQueries
{
    [UsePaging]
    public static IQueryable<Speaker> GetSpeakersAsync(ApplicationDbContext dbContext)
    {
        return dbContext.Speakers.AsNoTracking().OrderBy(s => s.Name);
    }

    [NodeResolver]
    public static async Task<Speaker?> GetSpeakerByIdAsync(
        int id,
        ISpeakerByIdDataLoader speakerById,
        CancellationToken cancellationToken)
    {
        return await speakerById.LoadAsync(id, cancellationToken);
    }

    public static async Task<IEnumerable<Speaker>> GetSpeakersByIdAsync(
        [ID<Speaker>] int[] ids,
        ISpeakerByIdDataLoader speakerById,
        CancellationToken cancellationToken)
    {
        return await speakerById.LoadRequiredAsync(ids, cancellationToken);
    }
}