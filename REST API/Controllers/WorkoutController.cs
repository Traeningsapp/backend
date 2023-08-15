using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace REST_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutController : ControllerBase
    {
        [Route("get/newworkout")]
        [HttpGet]
        public IActionResult GetNewWorkout()
        {
            try
            {
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("get/workoutfromhistory")]
        [HttpGet]
        public IActionResult GetWorkoutFromHistory(string userId, string workoutId)
        {
            try
            {
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("get/workouthistory")]
        [HttpGet]
        public IActionResult GetWorkouthistory(string userId)
        {
            try
            {
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("save/workout")]
        [HttpPost]
        public IActionResult SaveWorkout(string workoutSomJson)
        {
            try
            {
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


    }
}
