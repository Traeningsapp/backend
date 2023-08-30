using Application.Ports.Incoming;
using Domain.Workout;
using Microsoft.AspNetCore.Mvc;
using Moq;
using REST_API.Controllers;
using REST_API.Requests;
using Xunit;

namespace Test
{
    public class WorkoutControllerTests
    {

        private readonly WorkoutController _controller;
        private readonly Mock<IWorkoutUseCase> _mockWorkoutUseCase;

        public WorkoutControllerTests()
        {
            _mockWorkoutUseCase = new Mock<IWorkoutUseCase>();
            _controller = new WorkoutController(_mockWorkoutUseCase.Object);
        }

        [Fact]
        public void GetNewWorkout_ReturnsOkObjectResult_WhenAuthorizedAndSuccessful()
        {
            // Arrange
            var mockWorkout = new Mock<IWorkout>();
            _mockWorkoutUseCase.Setup(x => x.GenerateNewWorkout(It.IsAny<int>(), It.IsAny<string>())).Returns(mockWorkout.Object);

            // Act
            var result = _controller.GetNewWorkout(It.IsAny<int>(), It.IsAny<string>());

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsAssignableFrom<IWorkout>(okObjectResult.Value);
            Assert.Equal(mockWorkout.Object, okObjectResult.Value);
        }

        [Fact]
        public void GetNewWorkout_ReturnsBadRequestObjectResult_WhenAuthorizedAndThrowsException()
        {
            // Arrange
            var exceptionMessage = "Test exception message";
            _mockWorkoutUseCase.Setup(x => x.GenerateNewWorkout(It.IsAny<int>(), It.IsAny<string>())).Throws(new Exception(exceptionMessage));

            // Act
            var result = _controller.GetNewWorkout(It.IsAny<int>(), It.IsAny<string>());

            // Assert
            var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(exceptionMessage, badRequestObjectResult.Value);
        }

        [Fact]
        public void GetWorkoutFromHistory_ReturnsOkObjectResult_WhenAuthorizedAndSuccessful()
        {
            // Arrange
            var mockWorkout = new Mock<IWorkout>();
            _mockWorkoutUseCase.Setup(x => x.StartWorkoutFromHistory(It.IsAny<string>(), It.IsAny<int>())).Returns(mockWorkout.Object);

            // Act
            var result = _controller.GetWorkoutFromHistory(It.IsAny<string>(), It.IsAny<int>());

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsAssignableFrom<IWorkout>(okObjectResult.Value);
            Assert.Equal(mockWorkout.Object, okObjectResult.Value);
        }

        [Fact]
        public void GetWorkoutFromHistory_ReturnsBadRequestObjectResult_WhenAuthorizedAndThrowsException()
        {
            // Arrange
            var exceptionMessage = "Test exception message";
            _mockWorkoutUseCase.Setup(x => x.StartWorkoutFromHistory(It.IsAny<string>(), It.IsAny<int>())).Throws(new Exception(exceptionMessage));

            // Act
            var result = _controller.GetWorkoutFromHistory(It.IsAny<string>(), It.IsAny<int>());

            // Assert
            var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(exceptionMessage, badRequestObjectResult.Value);
        }

        [Fact]
        public void GetWorkoutHistory_ReturnsOkObjectResult_WhenAuthorizedAndSuccessful()
        {
            // Arrange
            var mockWorkoutHistory = new Mock<List<IWorkout>>();
            _mockWorkoutUseCase.Setup(x => x.GetWorkoutHistory(It.IsAny<string>())).Returns(mockWorkoutHistory.Object);

            // Act
            var result = _controller.GetWorkoutHistory(It.IsAny<string>());

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsAssignableFrom<List<IWorkout>>(okObjectResult.Value);
            Assert.Equal(mockWorkoutHistory.Object, okObjectResult.Value);
        }

        [Fact]
        public void GetWorkoutHistory_ReturnsBadRequestObjectResult_WhenAuthorizedAndThrowsException()
        {
            // Arrange
            var exceptionMessage = "Test exception message";
            _mockWorkoutUseCase.Setup(x => x.GetWorkoutHistory(It.IsAny<string>())).Throws(new Exception(exceptionMessage));

            // Act
            var result = _controller.GetWorkoutHistory(It.IsAny<string>());

            // Assert
            var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(exceptionMessage, badRequestObjectResult.Value);
        }

        [Fact]
        public void PostWorkout_ReturnsOkResult_WhenAuthorizedAndSuccessful()
        {
            // Arrange
            var mockUserId = "sampleUserId";
            var mockWorkoutRequest = new WorkoutRequest("{}");

            _mockWorkoutUseCase
                .Setup(x => x.SaveWorkout(mockUserId, mockWorkoutRequest.WorkoutAsJson))
                .Verifiable();

            // Act
            var result = _controller.PostWorkout(mockUserId, mockWorkoutRequest);

            // Assert
            Assert.IsType<OkResult>(result);
            _mockWorkoutUseCase.Verify();
        }


        [Fact]
        public void PostWorkout_ReturnsBadRequestObjectResult_WhenAuthorizedAndThrowsException()
        {
            // Arrange
            var mockUserId = "sampleUserId";
            var mockWorkoutRequest = new WorkoutRequest("{}");
            var exceptionMessage = "Test exception message";

            _mockWorkoutUseCase
                .Setup(x => x.SaveWorkout(mockUserId, mockWorkoutRequest.WorkoutAsJson))
                .Throws(new Exception(exceptionMessage));

            // Act
            var result = _controller.PostWorkout(mockUserId, mockWorkoutRequest);

            // Assert
            var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(exceptionMessage, badRequestObjectResult.Value);
        }

    }
}
