using Application.Ports.Incoming;
using Domain.Exercise;
using Domain.Workout;
using Microsoft.AspNetCore.Mvc;
using Moq;
using REST_API.Controllers;
using System.Runtime.InteropServices;
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
            _mockWorkoutUseCase.Setup(x => x.GenerateNewWorkout(It.IsAny<int>())).Returns(mockWorkout.Object);

            // Act
            var result = _controller.GetNewWorkout(It.IsAny<int>());

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
            _mockWorkoutUseCase.Setup(x => x.GenerateNewWorkout(It.IsAny<int>())).Throws(new Exception(exceptionMessage));

            // Act
            var result = _controller.GetNewWorkout(It.IsAny<int>());

            // Assert
            var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(exceptionMessage, badRequestObjectResult.Value);
        }

        [Fact]
        public void GetWorkoutFromHistory_ReturnsOkObjectResult_WhenAuthorizedAndSuccessful()
        {
            // Arrange
            var mockWorkout = new Mock<IWorkout>();
            _mockWorkoutUseCase.Setup(x => x.StartWorkoutFromHistory(It.IsAny<int>(), It.IsAny<int>())).Returns(mockWorkout.Object);

            // Act
            var result = _controller.GetWorkoutFromHistory(It.IsAny<int>(), It.IsAny<int>());

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
            _mockWorkoutUseCase.Setup(x => x.StartWorkoutFromHistory(It.IsAny<int>(), It.IsAny<int>())).Throws(new Exception(exceptionMessage));

            // Act
            var result = _controller.GetWorkoutFromHistory(It.IsAny<int>(), It.IsAny<int>());

            // Assert
            var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(exceptionMessage, badRequestObjectResult.Value);
        }

        [Fact]
        public void GetWorkoutHistory_ReturnsOkObjectResult_WhenAuthorizedAndSuccessful()
        {
            // Arrange
            var mockWorkoutHistory = new Mock<List<IWorkout>>();
            _mockWorkoutUseCase.Setup(x => x.GetWorkoutHistory(It.IsAny<int>())).Returns(mockWorkoutHistory.Object);

            // Act
            var result = _controller.GetWorkoutHistory(It.IsAny<int>());

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
            _mockWorkoutUseCase.Setup(x => x.GetWorkoutHistory(It.IsAny<int>())).Throws(new Exception(exceptionMessage));

            // Act
            var result = _controller.GetWorkoutHistory(It.IsAny<int>());

            // Assert
            var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(exceptionMessage, badRequestObjectResult.Value);
        }

        [Fact]
        public void SaveWorkout_ReturnsOkResult_WhenAuthorizedAndSuccessful()
        {
            // Arrange
            _mockWorkoutUseCase.Setup(x => x.SaveWorkout(It.IsAny<int>(), It.IsAny<string>()));

            // Act
            var result = _controller.SaveWorkout(It.IsAny<int>(), It.IsAny<string>());

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void SaveWorkout_ReturnsBadRequestObjectResult_WhenAuthorizedAndThrowsException()
        {
            // Arrange
            var exceptionMessage = "Test exception message";
            _mockWorkoutUseCase.Setup(x => x.SaveWorkout(It.IsAny<int>(), It.IsAny<string>())).Throws(new Exception(exceptionMessage));

            // Act
            var result = _controller.SaveWorkout(It.IsAny<int>(), It.IsAny<string>());

            // Assert
            var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(exceptionMessage, badRequestObjectResult.Value);
        }
    }
}
