using Application.Ports.Outgoing;
using Domain.Exercise;
using Domain.Workout;

namespace Persistence
{
    public class WorkoutRepository : BaseRepository, IWorkoutRepository
    {
        public WorkoutRepository(IConfigurationProvider configurationProvider) : base(configurationProvider)
        {
        }

        public void SaveWorkout(IWorkout workout)
        {
            try
            {
                string procedureName = "Workout_Save";
                var parameters = new
                {
                    iuserId = workout.User.Id,
                    workoutName = workout.Name
                };

                ExecuteStoredProcedure(DbConnection(), procedureName, parameters);
                SaveExercisesInWorkout(workout);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void SaveExercisesInWorkout(IWorkout workout)
        {
            try
            {
                string procedureName = "WorkoutExercise_SaveExercise";

                foreach (IExercise exercise in workout.Exercises)
                {

                    var parameters = new
                    {
                        workoutId = workout.Id,
                        exerciseId = exercise.Id,
                    };

                    ExecuteStoredProcedure(DbConnection(), procedureName, parameters);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IWorkout GenerateNewWorkout()
        {
            throw new NotImplementedException();
        }

        public List<IWorkout> GetWorkoutHistory(string userId)
        {
            throw new NotImplementedException();
        }

        public IWorkout StartWorkoutFromHistory(int userId, int workoutId)
        {
            throw new NotImplementedException();
        }
    }
}
