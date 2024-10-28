using Xunit;
using RazorPagesMovie.Models;  // Correct namespace for the Movie model

namespace RazorPagesMovie.Tests
{
    public class MovieTests
    {
        [Fact]
        public void Movie_Title_NotEmpty()
        {
            // Arrange
            var movie = new Movie { Title = "Inception" };

            // Act & Assert
            Assert.False(string.IsNullOrEmpty(movie.Title));
        }
    }
}