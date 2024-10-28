using Xunit;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Models;
using RazorPagesMovie.Data;
using RazorPagesMovie.Services;
using System.Threading.Tasks;
using System.Transactions;

public class IntegrationTest
{
    [Fact]
    public async Task TestMovieCreation()
    {
        var options = new DbContextOptionsBuilder<RazorPagesMovieContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            using (var context = new RazorPagesMovieContext(options))
            {
                context.Database.EnsureCreated();
                var service = new MovieService(context);
                var newMovie = new Movie { Title = "Test Movie", Genre = "Action", Price = 10.00M };
                await service.AddMovie(newMovie);
            }

            using (var context = new RazorPagesMovieContext(options))
            {
                var movieInDb = await context.Movie.FirstAsync(m => m.Title == "Test Movie");
                Assert.NotNull(movieInDb);
                Assert.Equal("Action", movieInDb.Genre);
                Assert.Equal(10.00M, movieInDb.Price);
            }

            // Rollback transaction to not affect the database state
            scope.Dispose();
        }
    }
}
