using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace REST_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseController : ControllerBase
    {
        [Route("get/musclegroups")]
        [HttpGet]
        public IActionResult GetMusclegroups()
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

        [Route("get/muscle")]
        [HttpGet]
        public IActionResult GetMuscles(string musclegroupId)
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

        [Route("get/exerciselist")]
        [HttpGet]
        public IActionResult GetExerciselist(string muscleId)
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

        [Route("get/exercise")]
        [HttpGet]
        public IActionResult GetExercise(string exerciseId)
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
