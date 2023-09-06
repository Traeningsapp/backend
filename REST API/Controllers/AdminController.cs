using Application.Ports.Incoming;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace REST_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminUseCase _adminUseCase;

        public AdminController(IAdminUseCase adminUseCase)
        {
            _adminUseCase = adminUseCase;
        }

        [Route("patch/exercise/{exerciseId}/activevalue/{active}/user/{userId}")]
        [HttpPatch]
        [Authorize(Policy = "UpdateExercise")]
        public IActionResult PatchActiveValue(int exerciseId, bool active, string userId)
        {
            try
            {
                _adminUseCase.UpdateExerciseActiveFlag(exerciseId, active, userId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
