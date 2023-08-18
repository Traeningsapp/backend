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
        [Route("get/muscle")]
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
        [Route("get/exerciselist")]
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
        [Route("get/exercise")]
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
    }
}
