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

        //[Authorize]
        [Route("get/newworkout/{split_id}")]
        [HttpGet]
        public IActionResult GetNewWorkout(int split_id)
        {
            try
            {
                var apiResponse = _workoutUseCase.GenerateNewWorkout(split_id);

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
        [Route("get/workouthistory/user/{userId}")]
        [HttpGet]
        public IActionResult GetWorkoutHistory(int userId)
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
        [Route("save/workout/user/{userId}/workout/{workoutAsJson}")]
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
