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

        public IWorkout GenerateNewWorkout(int splitId, string userId, bool includeAbs, bool priorFavorites)
        {
            try
            {
                IUser user = new User(userId);
                IWorkout workout = new Workout(user);

                Dictionary<string, List<IExercise>> exerciseDictionary = new Dictionary<string, List<IExercise>>();

                exerciseDictionary["nonAbs"] = _exerciseRepository.GetAllExercisesBySplitIdAndFavoritesByUserId(splitId, userId);
                if (includeAbs)
                    exerciseDictionary["abs"] = _exerciseRepository.GetAbsExercises(userId);


                foreach(var list in exerciseDictionary)
                {
                    foreach(var exercise in list.Value)
                    {
                        exercise.Muscles = _exerciseRepository.GetMusclesInExerciseById(exercise.Id);
                        exercise.IsFavorite = _exerciseRepository.GetFavoriteStatus(exercise.Id, userId);
                    }
                }

                if (splitId == 1)
                    workout.GenerateExercisesForPushSplit(exerciseDictionary, includeAbs, priorFavorites);

                foreach (IExercise exercise in workout.Exercises)
                {
                    exercise.ExerciseStats = _exerciseRepository.GetExerciseStats(exercise.Id, workout.User.Id);
                }

                // no need for the Muscles objects when returning
                workout.Exercises.ForEach(exercise => exercise.Muscles = new List<IMuscle>());
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

        public int SaveWorkout(IWorkout workout)
        {
            try
            {
                return _workoutRepository.SaveWorkout(workout);
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

                foreach(IExercise exercise in workout.Exercises)
                {
                    exercise.ExerciseStats = _exerciseRepository.GetExerciseStatsByWorkoutId(workoutId, exercise.Id);
                    exercise.Muscles = _exerciseRepository.GetMusclesInExerciseById(exercise.Id);

                }


                return workout;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void DeleteWorkout(int workoutId)
        {
            try
            {
                _workoutRepository.DeleteWorkout(workoutId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
