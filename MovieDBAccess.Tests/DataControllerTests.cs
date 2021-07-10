using System;
using Xunit;
using Moq;
using MovieDbAccess.Domain.Interfaces;
using MovieDbAccess.Controllers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MovieDbAccess.Tests
{
    public class DataControllerTests
    {
        [Fact]
        public async Task GetUserAsync_Should_Return_Ok()
        {
        /*
        * Tests the DataController's GetUserAsync and ensures the controller returns a
        * 200 OK Response with the user returned by the Data Processor as the object value.
        */
            //Arrange
            var mockDataProcessor = new Mock<IDataProcessor>();
            User expectedUser = new User {UID = "1"};
            mockDataProcessor.Setup(x => x.GetUserAsync(It.IsAny<string>())).Returns(Task.FromResult(expectedUser));
            var dataController = new DataController(mockDataProcessor.Object);
            
            //Act
            var actionResult = await dataController.GetUserAsync("0x456");
            var result = actionResult as OkObjectResult;
            Console.WriteLine(actionResult.GetType());

            //Assert
            Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal(expectedUser, result.Value);
        }

        [Fact]
        public async Task GetMovieAsync_Should_Return_Ok()
        {
        /*
        * Tests the controller's GetMovieAsync method.
        * Expects a 200 Ok Response, ignoring whether or not the movie exists.
        * As that is handled at the application level.
        */
            //Arrange
            var mockDataProcessor = new Mock<IDataProcessor>();
            Movie movie = new Movie {imdb_id = "123456"};
            mockDataProcessor.Setup(x => x.GetMovieAsync(It.IsAny<int>())).Returns(Task.FromResult(movie));
            var dataController = new DataController(mockDataProcessor.Object);
            
            //Act
            var actionResult = await dataController.GetMovieAsync(123);
            var result = actionResult as OkObjectResult;

            //Assert
            Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal(movie, result.Value);
        }

        [Fact]
        public async Task Null_GetMovieAsync_Should_Return_Ok()
        {
        /*
        * Tests the controller's GetMovieAsync method.
        * Expects a 200 Ok Response, ignoring whether or not the movie exists.
        * As that is handled at the application level.
        */
            //Arrange
            var mockDataProcessor = new Mock<IDataProcessor>();
            var dataController = new DataController(mockDataProcessor.Object);
            
            //Act
            var actionResult = await dataController.GetMovieAsync(123);
            var result = actionResult as OkObjectResult;
            //Assert
            Assert.IsType<OkObjectResult>(actionResult);
        }
        
        [Fact]
        public async Task AddMovieAsync_ShouldReturn_Accepted()
        {
        /*
        * Tests the controller's AddMovieAsync Method.
        * Sends a random mocked movie title that successfully is added and is expected
        * to return a Status 202 Accepted Response.
        */
            //Arrange
            var mockDataProcessor = new Mock<IDataProcessor>();
            mockDataProcessor.Setup(x => x.StoreMovieAsync(It.IsAny<int>(), It.IsAny<string>())).Returns(Task.FromResult(1));
            var dataController = new DataController(mockDataProcessor.Object);
            
            //Act
            var actionResult = await dataController.AddMovieAsync(1,"Title");

            //Assert
            Assert.IsType<AcceptedResult>(actionResult);
        }

        [Fact]
        public async Task AddMovieAsync_ShouldReturn_BadRequest()
        {
        /*
        * Tests the controller's AddMovieAsync Method.
        * Sends a random mocked movie title that cannot be added to the database and is expected
        * to return a Status 400 Bad Request Response.
        */
            //Arrange
            var mockDataProcessor = new Mock<IDataProcessor>();
            mockDataProcessor.Setup(x => x.StoreMovieAsync(It.IsAny<int>(), It.IsAny<string>())).Returns(Task.FromResult(0));
            var dataController = new DataController(mockDataProcessor.Object);
            
            //Act
            var actionResult = await dataController.AddMovieAsync(1,"Title");

            //Assert
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async Task DeleteMovieAsync_ShouldReturn_Ok()
        {
        /*
        * Tests the controller's DeleteMovieAsync Method.
        * Sends a random mocked movie title to be deleted from database and is expected
        * to return a Status 200 Ok Response.
        */
            //Arrange
            var mockDataProcessor = new Mock<IDataProcessor>();
            mockDataProcessor.Setup(x => x.DeleteMovieAsync(It.IsAny<int>(), It.IsAny<string>())).Returns(Task.FromResult(1));
            var dataController = new DataController(mockDataProcessor.Object);
            
            //Act
            var actionResult = await dataController.DeleteMovieAsync(1,"Title");

            //Assert
            Assert.IsType<OkResult>(actionResult);
        }

        [Fact]
        public async Task DeleteMovieAsync_ShouldReturn_NotFound()
        {
        /*
        * Tests the controller's DeleteMovieAsync Method.
        * Sends a random mocked movie title to be deleted from database that does not
        * exist and is expected to return a Status 404 Not Found Response.
        */
            //Arrange
            var mockDataProcessor = new Mock<IDataProcessor>();
            mockDataProcessor.Setup(x => x.DeleteMovieAsync(It.IsAny<int>(), It.IsAny<string>())).Returns(Task.FromResult(0));
            var dataController = new DataController(mockDataProcessor.Object);
            
            //Act
            var actionResult = await dataController.DeleteMovieAsync(1,"Title");

            //Assert
            Assert.IsType<NotFoundResult>(actionResult);
        }
    }
}
