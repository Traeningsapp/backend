using Application.Ports.Incoming;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace REST_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutController : ControllerBase
    {
        private readonly IWorkoutUseCase _workoutUseCase;

        public WorkoutController(IWorkoutUseCase workoutUseCase)
        {
            _workoutUseCase = workoutUseCase;
        }

        [Authorize]
        [Route("get/newworkout")]
        [HttpGet]
        public IActionResult GetNewWorkout()
        {
            try
            {
                var apiResponse = _workoutUseCase.GenerateNewWorkout();

                return Ok(apiResponse);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [Route("get/workoutfromhistory")]
        [HttpGet]
        public IActionResult GetWorkoutFromHistory(int userId, int workoutId)
        {
            try
            {
                var apiResponse = _workoutUseCase.StartWorkoutFromHistory(userId, workoutId);

                return Ok(apiResponse);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [Route("get/workouthistory")]
        [HttpGet]
        public IActionResult GetWorkouthistory(int userId)
        {
            try
            {
                var apiResponse = _workoutUseCase.GetWorkoutHistory(userId);

                return Ok(apiResponse);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [Route("save/workout")]
        [HttpPost]
        public IActionResult SaveWorkout(int userId, string workoutAsJson)
        {
            try
            {
                _workoutUseCase.SaveWorkout(userId, workoutAsJson);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
