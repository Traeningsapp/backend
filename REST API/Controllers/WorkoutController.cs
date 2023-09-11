using Application.Ports.Incoming;
using Application.Ports.Outgoing;
using Domain.User;
using Domain.Workout;
using FluentValidation;
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
        private readonly IValidator<IWorkout> _workoutValidator;
        private readonly IDataMapper<IWorkout> _dataMapper;

        public WorkoutController(IWorkoutUseCase workoutUseCase, IValidator<IWorkout> workoutValidator, IDataMapper<IWorkout> dataMapper)
        {
            _workoutUseCase = workoutUseCase;
            _workoutValidator = workoutValidator;
            _dataMapper = dataMapper;
        }

        [Authorize]
        [Route("get/newworkout/split/{splitId}/user/{userId}/abs/{includeAbs}/prioritizeFavs/{prioFavorites}")]
        [HttpGet]
        public IActionResult GetNewWorkout(int splitId, string userId, bool includeAbs, bool prioFavorites)
        {
            try
            {
                var apiResponse = _workoutUseCase.GenerateNewWorkout(splitId, userId, includeAbs, prioFavorites);

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
        [Route("post/workout/user/{userId}/split/{splitType}")]
        [HttpPost]
        public IActionResult PostWorkout(string userId, string splitType, [FromBody] WorkoutRequest request)
        {
            try
            {
                IWorkout? workout = _dataMapper.FromJson(request.WorkoutAsJson);
                if (workout == null)
                {
                    return BadRequest("Workout is null.");
                }
                workout.User = new User(userId);
                workout.SplitType = splitType;

                var validationResult = _workoutValidator.Validate(workout);

                if (validationResult.IsValid == false)
                {
                    return BadRequest(validationResult.Errors);
                }

                var apiResponse = _workoutUseCase.SaveWorkout(workout);

                return Ok(apiResponse);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [Route("patch/workout/{workoutId}")]
        [HttpPatch]
        public IActionResult DeleteWorkout(int workoutId)
        {
            try
            {
                _workoutUseCase.DeleteWorkout(workoutId);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
