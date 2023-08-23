using Application.Ports.Incoming;
using Application.Ports.Outgoing;
using Domain.Exercise;
using Domain.User;
using Domain.Workout;

namespace Application.UseCases
{
    public class WorkoutUseCase : IWorkoutUseCase
    {
        private readonly IWorkoutRepository _workoutRepository;
        private readonly IExerciseRepository _exerciseRepository;

        public WorkoutUseCase(IWorkoutRepository workoutRepository, IExerciseRepository exerciseRepository)
        {
            _workoutRepository = workoutRepository;
            _exerciseRepository = exerciseRepository;
        }

        public IWorkout GenerateNewWorkout(int split_id, string userId)
        {
            try
            {
                IUser user = new User(userId);
                IWorkout workout = new Workout(user)
                {
                    Exercises = _workoutRepository.GenerateExercisesForNewWorkout(split_id)
                };

                foreach (IExercise exercise in workout.Exercises)
                {
                    exercise.Stats = _exerciseRepository.GetExerciseStats(exercise.Id, workout.User.Id);
                }

                return workout;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<IWorkout> GetWorkoutHistory(string userId)
        {
            try
            {
                List<IWorkout> workouts = _workoutRepository.GetWorkoutHistoryByUserId(userId);

                return workouts;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void SaveWorkout(string userId, string workoutAsJson)
        {
            try
            {
                IUser user = new User(userId);
                IWorkout workout = new Workout(user);

                workout.FromJson(workoutAsJson);

                _workoutRepository.SaveWorkout(workout);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IWorkout StartWorkoutFromHistory(string userId, int workoutId)
        {
            try
            {
                IUser user = new User(userId);
                IWorkout workout = new Workout(user)
                {
                    Exercises = _exerciseRepository.GetExercisesByWorkoutId(workoutId)
                };

                return workout;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
