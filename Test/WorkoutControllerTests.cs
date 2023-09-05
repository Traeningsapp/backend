using Application.Ports.Incoming;
using Application.Ports.Outgoing;
using Domain.Workout;
using FluentValidation;
using FluentValidation.Results;
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
        private readonly Mock<IValidator<IWorkout>> _mockWorkoutValidator;
        private readonly Mock<IDataMapper<IWorkout>> _mockDataMapper;

        public WorkoutControllerTests()
        {
            _mockWorkoutUseCase = new Mock<IWorkoutUseCase>();
            _mockWorkoutValidator = new Mock<IValidator<IWorkout>>();
            _mockDataMapper = new Mock<IDataMapper<IWorkout>>();
            _controller = new WorkoutController(_mockWorkoutUseCase.Object, _mockWorkoutValidator.Object, _mockDataMapper.Object);
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
            var mockSplitType = "sampleSplitType";
            var mockWorkoutRequest = new WorkoutRequest("{}");
            var mockWorkout = new Mock<IWorkout>();

            // Mock the data mapper behavior
            _mockDataMapper
                .Setup(x => x.FromJson(It.IsAny<string>()))
                .Returns(mockWorkout.Object);

            // Mock the workout validator to always return a valid result
            _mockWorkoutValidator
                .Setup(x => x.Validate(It.IsAny<IWorkout>()))
                .Returns(new ValidationResult());

            // Mock the SaveWorkout method to return a sample response
            _mockWorkoutUseCase
                .Setup(x => x.SaveWorkout(It.IsAny<IWorkout>()))
                .Returns(1); // Replace with your actual response type

            // Act
            var result = _controller.PostWorkout(mockUserId, mockSplitType, mockWorkoutRequest);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }


        [Fact]
        public void PostWorkout_ReturnsBadRequestObjectResult_WhenAuthorizedAndThrowsException()
        {
            // Arrange
            var mockUserId = "sampleUserId";
            var mockSplitType = "sampleSplitType";
            var mockWorkoutRequest = new WorkoutRequest("{}");
            var exceptionMessage = "Workout is null.";
            var mockWorkout = new Mock<IWorkout>();

            // Mock the SaveWorkout method to throw an exception
            _mockWorkoutUseCase
                .Setup(x => x.SaveWorkout(It.IsAny<IWorkout>()))
                .Throws(new Exception(exceptionMessage));

            // Act
            var result = _controller.PostWorkout(mockUserId, mockSplitType, mockWorkoutRequest);

            // Assert
            var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(exceptionMessage, badRequestObjectResult.Value);
        }


    }
}
