
# CSC492 Senior Design Project - REST API 
## Controller Unit Tests
**Frameworks Used:** xUnit, Moq

The `DataControllerTests.cs` file contains 7 unit tests, checking that the controller returns the expected responses based on input.
* `GetUserAsync_Should_Return_Ok()` checks that the controller returns a 200 Ok response status and that the response body contains the same User object that is returned by the `DataProcessor`.
* `GetMovieAsync_Should_Return_Ok()` checks that the controller returns a 200 Ok response status and that the response body contains the same movie object that is returned by the `DataProcessor`.
* `Null_GetMovieAsync_Should_Return_Ok` checks that the controller returns a Ok 200 response status even when no movie is retrieved by the `DataProcessor`.
* `AddMovieAsync_ShouldReturn_Accepted()` checks that the controller returns a 202 accepted status when successfully storing a movie to the database.
* `AddMovieAsync_ShouldReturn_BadRequest()` checks that the controller returns a 400 bad request status when making an unsuccessful attempt at storing a movie to the database.
* `DeleteMovieAsync_ShouldReturn_Ok()` checks that the controller returns a 200 Ok response status when successfully deleting a movie from the database.
* `DeleteMovieAsync_ShouldReturn_NotFound()` checks that the controller returns a 404 not found response status when making an unsuccessful attempt at deleting a movie from the database.
