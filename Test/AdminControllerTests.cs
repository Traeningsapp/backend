using Application.Ports.Incoming;
using Microsoft.AspNetCore.Mvc;
using Moq;
using REST_API.Controllers;
using Xunit;

namespace Test
{
    public class AdminControllerTests
    {
        private readonly AdminController _adminController;
        private readonly Mock<IAdminUseCase> _mockAdminUseCase;

        public AdminControllerTests()
        {
            _mockAdminUseCase = new Mock<IAdminUseCase>();
            _adminController = new AdminController(_mockAdminUseCase.Object);
        }

        [Fact]
        public void PatchActiveValue_ReturnsOkResult_WhenAuthorizedAndSuccessful()
        {
            // Arrange
            var exerciseId = 1;
            var active = true;
            var userId = "sampleUserId";

            // Act
            var result = _adminController.PatchActiveValue(exerciseId, active, userId);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void PatchActiveValue_ReturnsBadRequestObjectResult_WhenAuthorizedAndThrowsException()
        {
            // Arrange
            var exerciseId = 1;
            var active = true;
            var userId = "sampleUserId";
            var exceptionMessage = "Test exception message";

            _mockAdminUseCase.Setup(x => x.UpdateExerciseActiveFlag(exerciseId, active, userId)).Throws(new Exception(exceptionMessage));

            // Act
            var result = _adminController.PatchActiveValue(exerciseId, active, userId);

            // Assert
            var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(exceptionMessage, badRequestObjectResult.Value);
        }


    }
}
