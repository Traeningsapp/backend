﻿using Application.Ports.Incoming;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace REST_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseController : ControllerBase
    {
        private readonly IExerciseUseCase _exerciseUseCase;

        public ExerciseController(IExerciseUseCase exerciseUseCase)
        {
            _exerciseUseCase = exerciseUseCase;
        }

        [Authorize]
        [Route("get/musclegroups")]
        [HttpGet]
        public IActionResult GetMusclegroups()
        {
            try
            {
                var apiResponse = _exerciseUseCase.GetMusclegroups();

                return Ok(apiResponse);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [Route("get/muscle/{musclegroupId}")]
        [HttpGet]
        public IActionResult GetMuscles(int musclegroupId)
        {
            try
            {
                var apiResponse = _exerciseUseCase.GetMusclesByMusclegroupId(musclegroupId);

                return Ok(apiResponse);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [Route("get/exerciselist/{muscleId}")]
        [HttpGet]
        public IActionResult GetExerciselist(int muscleId)
        {
            try
            {
                var apiResponse = _exerciseUseCase.GetExercisesForMuscle(muscleId);

                return Ok(apiResponse);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [Route("get/exercise/{exerciseId}")]
        [HttpGet]
        public IActionResult GetExercise(int exerciseId)
        {
            try
            {
                var apiResponse = _exerciseUseCase.GetExercise(exerciseId);

                return Ok(apiResponse);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [Route("get/favorites/user/{userId}")]
        [HttpGet]
        public IActionResult GetFavoriteExerciselist(string userId)
        {
            try
            {
                var apiResponse = _exerciseUseCase.GetFavoriteExercises(userId);

                return Ok(apiResponse);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [Route("get/favorite/exercise/{exerciseId}/user/{userId}")]
        [HttpGet]
        public IActionResult GetExerciseFavoriteStatus(int exerciseId, string userId)
        {
            try
            {
                var apiResponse = _exerciseUseCase.GetExerciseFavoriteStatus(exerciseId, userId);

                return Ok(apiResponse);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [Route("post/favorites/user/{userId}/exercise/{exerciseId}")]
        [HttpPost]
        public IActionResult SetFavoriteExerciselist(string userId, int exerciseId)
        {
            try
            {
                _exerciseUseCase.SetFavoriteExercise(userId, exerciseId);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [Route("delete/favorites/user/{userId}/exercise/{exerciseId}")]
        [HttpDelete]
        public IActionResult DeleteFavoriteExercise(string userId, int exerciseId)
        {
            try
            {
                _exerciseUseCase.DeleteFavoriteExercise(userId, exerciseId);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [Route("get/HowTo/exercise/{exerciseId}")]
        [HttpGet]
        public IActionResult GetExerciseHowTolist(int exerciseId)
        {
            try
            {
                var apiResponse = _exerciseUseCase.GetExerciseHowTo(exerciseId);

                return Ok(apiResponse);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [Route("get/exerciseStats/{exerciseId}/user/{userId}")]
        [HttpGet]
        public IActionResult GetExerciseStats(int exerciseId, string userId)
        {
            try
            {
                var apiResponse = _exerciseUseCase.GetExerciseStats(exerciseId, userId);

                return Ok(apiResponse);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


    }
}
