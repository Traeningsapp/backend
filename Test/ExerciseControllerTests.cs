using Application.Ports.Incoming;
using REST_API.Controllers;
using Moq;
using Domain.Workout;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Domain.Exercise;
using Application.UseCases;

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

        [Fact]
        public void GetFavoriteExerciselist_ReturnsOkObjectResult_WhenAuthorizedAndSuccessful()
        {
            // Arrange
            var userId = "sampleUserId";
            var mockExerciseList = new List<IExercise>();

            _mockExerciseUseCase.Setup(x => x.GetFavoriteExercises(userId)).Returns(mockExerciseList);

            // Act
            var result = _controller.GetFavoriteExerciselist(userId);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var exercises = Assert.IsAssignableFrom<IEnumerable<IExercise>>(okObjectResult.Value);
            Assert.Equal(mockExerciseList, exercises);
        }

        [Fact]
        public void GetFavoriteExerciselist_ReturnsBadRequestObjectResult_WhenAuthorizedAndThrowsException()
        {
            // Arrange
            var userId = "sampleUserId";
            var exceptionMessage = "Test exception message";

            _mockExerciseUseCase.Setup(x => x.GetFavoriteExercises(userId)).Throws(new Exception(exceptionMessage));

            // Act
            var result = _controller.GetFavoriteExerciselist(userId);

            // Assert
            var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(exceptionMessage, badRequestObjectResult.Value);
        }

        [Fact]
        public void GetExerciseFavoriteStatus_ReturnsOkObjectResult_WhenAuthorizedAndSuccessful()
        {
            // Arrange
            var exerciseId = 1;
            var userId = "sampleUserId";
            var favoriteStatus = true;

            _mockExerciseUseCase.Setup(x => x.GetExerciseFavoriteStatus(exerciseId, userId)).Returns(favoriteStatus);

            // Act
            var result = _controller.GetExerciseFavoriteStatus(exerciseId, userId);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var status = Assert.IsType<bool>(okObjectResult.Value);
            Assert.Equal(favoriteStatus, status);
        }

        [Fact]
        public void GetExerciseFavoriteStatus_ReturnsBadRequestObjectResult_WhenAuthorizedAndThrowsException()
        {
            // Arrange
            var exerciseId = 123;
            var userId = "sampleUserId";
            var exceptionMessage = "Test exception message";

            _mockExerciseUseCase.Setup(x => x.GetExerciseFavoriteStatus(exerciseId, userId)).Throws(new Exception(exceptionMessage));

            // Act
            var result = _controller.GetExerciseFavoriteStatus(exerciseId, userId);

            // Assert
            var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(exceptionMessage, badRequestObjectResult.Value);
        }

        [Fact]
        public void SetFavoriteExerciselist_ReturnsOkResult_WhenAuthorizedAndSuccessful()
        {
            // Arrange
            var userId = "sampleUserId";
            var exerciseId = 1;

            // Act
            var result = _controller.SetFavoriteExerciselist(userId, exerciseId);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void SetFavoriteExerciselist_ReturnsBadRequestObjectResult_WhenAuthorizedAndThrowsException()
        {
            // Arrange
            var userId = "sampleUserId";
            var exerciseId = 1;
            var exceptionMessage = "Test exception message";

            _mockExerciseUseCase.Setup(x => x.SetFavoriteExercise(userId, exerciseId)).Throws(new Exception(exceptionMessage));

            // Act
            var result = _controller.SetFavoriteExerciselist(userId, exerciseId);

            // Assert
            var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(exceptionMessage, badRequestObjectResult.Value);
        }

        [Fact]
        public void DeleteFavoriteExercise_ReturnsOkResult_WhenAuthorizedAndSuccessful()
        {
            // Arrange
            var userId = "sampleUserId";
            var exerciseId = 1;

            // Act
            var result = _controller.DeleteFavoriteExercise(userId, exerciseId);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void DeleteFavoriteExercise_ReturnsBadRequestObjectResult_WhenAuthorizedAndThrowsException()
        {
            // Arrange
            var userId = "sampleUserId";
            var exerciseId = 1;
            var exceptionMessage = "Test exception message";

            _mockExerciseUseCase.Setup(x => x.DeleteFavoriteExercise(userId, exerciseId)).Throws(new Exception(exceptionMessage));

            // Act
            var result = _controller.DeleteFavoriteExercise(userId, exerciseId);

            // Assert
            var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(exceptionMessage, badRequestObjectResult.Value);
        }

        [Fact]
        public void GetExerciseHowTolist_ReturnsOkObjectResult_WhenAuthorizedAndSuccessful()
        {
            // Arrange
            var exerciseId = 123;
            var mockHowToList = new List<IHowTo>();

            _mockExerciseUseCase.Setup(x => x.GetExerciseHowTo(exerciseId)).Returns(mockHowToList);

            // Act
            var result = _controller.GetExerciseHowTolist(exerciseId);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(mockHowToList, okObjectResult.Value);
        }

        [Fact]
        public void GetExerciseHowTolist_ReturnsBadRequestObjectResult_WhenAuthorizedAndThrowsException()
        {
            // Arrange
            var exerciseId = 1;
            var exceptionMessage = "Test exception message";

            _mockExerciseUseCase.Setup(x => x.GetExerciseHowTo(exerciseId)).Throws(new Exception(exceptionMessage));

            // Act
            var result = _controller.GetExerciseHowTolist(exerciseId);

            // Assert
            var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(exceptionMessage, badRequestObjectResult.Value);
        }

        [Fact]
        public void GetExerciseStats_ReturnsOkObjectResult_WhenAuthorizedAndSuccessful()
        {
            // Arrange
            var exerciseId = 1;
            var userId = "sampleUserId";
            var mockExerciseStats = new List<IExerciseStats>();

            _mockExerciseUseCase.Setup(x => x.GetExerciseStats(exerciseId, userId)).Returns(mockExerciseStats);

            // Act
            var result = _controller.GetExerciseStats(exerciseId, userId);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(mockExerciseStats, okObjectResult.Value);
        }

        [Fact]
        public void GetExerciseStats_ReturnsBadRequestObjectResult_WhenAuthorizedAndThrowsException()
        {
            // Arrange
            var exerciseId = 1;
            var userId = "sampleUserId";
            var exceptionMessage = "Test exception message";

            _mockExerciseUseCase.Setup(x => x.GetExerciseStats(exerciseId, userId)).Throws(new Exception(exceptionMessage));

            // Act
            var result = _controller.GetExerciseStats(exerciseId, userId);

            // Assert
            var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(exceptionMessage, badRequestObjectResult.Value);
        }

    }
}
