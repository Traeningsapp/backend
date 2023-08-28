using Application.Ports.Incoming;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using REST_API.Requests;

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

        //[Authorize]
        [Route("get/newworkout/split/{splitId}/user/{userId}")]
        [HttpGet]
        public IActionResult GetNewWorkout(int splitId, string userId)
        {
            try
            {
                var apiResponse = _workoutUseCase.GenerateNewWorkout(splitId, userId);

                return Ok(apiResponse);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [Authorize]
        [Route("get/workoutfromhistory/user/{userId}/workout/{workoutId}")]
        [HttpGet]
        public IActionResult GetWorkoutFromHistory(string userId, int workoutId)
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
        [Route("get/workouthistory/user/{userId}")]
        [HttpGet]
        public IActionResult GetWorkoutHistory(string userId)
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
        [Route("post/workout/user/{userId}")]
        [HttpPost]
        public IActionResult PostWorkout(string userId, [FromBody] WorkoutRequest request)
        {
            try
            {
                _workoutUseCase.SaveWorkout(userId, request.WorkoutAsJson);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
