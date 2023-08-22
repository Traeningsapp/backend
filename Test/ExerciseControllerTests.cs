using Application.Ports.Incoming;
using REST_API.Controllers;
using Moq;
using Domain.Workout;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Domain.Exercise;

namespace Test
{
    public class ExerciseControllerTests
    {
        private readonly ExerciseController _controller;
        private readonly Mock<IExerciseUseCase> _mockExerciseUseCase;

        public ExerciseControllerTests()
        {
            _mockExerciseUseCase = new Mock<IExerciseUseCase>();
            _controller = new ExerciseController(_mockExerciseUseCase.Object);
        }

        [Fact]
        public void GetMusclegroups_ReturnsOkObjectResult_WhenAuthorizedAndSuccessful()
        {
            // Arrange
            var mockMusclegroupList = new Mock<List<IMusclegroup>>();
            _mockExerciseUseCase.Setup(x => x.GetMusclegroups()).Returns(mockMusclegroupList.Object);

            // Act
            var result = _controller.GetMusclegroups();

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsAssignableFrom<List<IMusclegroup>>(okObjectResult.Value);
            Assert.Equal(mockMusclegroupList.Object, okObjectResult.Value);
        }

        [Fact]
        public void GetMusclegroups_ReturnsBadRequestObjectResult_WhenAuthorizedAndThrowsException()
        {
            // Arrange
            var exceptionMessage = "Test exception message";
            _mockExerciseUseCase.Setup(x => x.GetMusclegroups()).Throws(new Exception(exceptionMessage));

            // Act
            var result = _controller.GetMusclegroups();

            // Assert
            var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(exceptionMessage, badRequestObjectResult.Value);
        }

        [Fact]
        public void GetMuscles_ReturnsOkObjectResult_WhenAuthorizedAndSuccessful()
        {
            // Arrange
            var mockMuscleList = new Mock<List<IMuscle>>();
            _mockExerciseUseCase.Setup(x => x.GetMusclesByMusclegroupId(It.IsAny<int>())).Returns(mockMuscleList.Object);

            // Act
            var result = _controller.GetMuscles(It.IsAny<int>());

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsAssignableFrom<List<IMuscle>>(okObjectResult.Value);
            Assert.Equal(mockMuscleList.Object, okObjectResult.Value);
        }

        [Fact]
        public void GetMuscles_ReturnsBadRequestObjectResult_WhenAuthorizedAndThrowsException()
        {
            // Arrange
            var exceptionMessage = "Test exception message";
            _mockExerciseUseCase.Setup(x => x.GetMusclesByMusclegroupId(It.IsAny<int>())).Throws(new Exception(exceptionMessage));

            // Act
            var result = _controller.GetMuscles(It.IsAny<int>());

            // Assert
            var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(exceptionMessage, badRequestObjectResult.Value);
        }

        [Fact]
        public void GetExerciselist_ReturnsOkObjectResult_WhenAuthorizedAndSuccessful()
        {
            // Arrange
            var mockExerciseList = new Mock<List<IExercise>>();
            _mockExerciseUseCase.Setup(x => x.GetExercisesForMuscle(It.IsAny<int>())).Returns(mockExerciseList.Object);

            // Act
            var result = _controller.GetExerciselist(It.IsAny<int>());

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsAssignableFrom<List<IExercise>>(okObjectResult.Value);
            Assert.Equal(mockExerciseList.Object, okObjectResult.Value);
        }

        [Fact]
        public void GetExerciselist_ReturnsBadRequestObjectResult_WhenAuthorizedAndThrowsException()
        {
            // Arrange
            var exceptionMessage = "Test exception message";
            _mockExerciseUseCase.Setup(x => x.GetExercisesForMuscle(It.IsAny<int>())).Throws(new Exception(exceptionMessage));

            // Act
            var result = _controller.GetExerciselist(It.IsAny<int>());

            // Assert
            var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(exceptionMessage, badRequestObjectResult.Value);
        }

        [Fact]
        public void GetExercise_ReturnsOkObjectResult_WhenAuthorizedAndSuccessful()
        {
            // Arrange
            var mockExercise = new Mock<IExercise>();
            _mockExerciseUseCase.Setup(x => x.GetExercise(It.IsAny<int>())).Returns(mockExercise.Object);

            // Act
            var result = _controller.GetExercise(It.IsAny<int>());

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsAssignableFrom<IExercise>(okObjectResult.Value);
            Assert.Equal(mockExercise.Object, okObjectResult.Value);
        }

        [Fact]
        public void GetExercise_ReturnsBadRequestObjectResult_WhenAuthorizedAndThrowsException()
        {
            // Arrange
            var exceptionMessage = "Test exception message";
            _mockExerciseUseCase.Setup(x => x.GetExercise(It.IsAny<int>())).Throws(new Exception(exceptionMessage));

            // Act
            var result = _controller.GetExercise(It.IsAny<int>());

            // Assert
            var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(exceptionMessage, badRequestObjectResult.Value);
        }
    }
}
