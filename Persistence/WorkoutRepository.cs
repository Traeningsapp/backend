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

        private void SaveExercisesInWorkout(IWorkout workout)
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

        public List<IWorkout> GenerateNewWorkout(int split_id)
        {
            try
            {
                string procedureName = "Workout_GetNew";

                var parameters = new 
                { 
                    split_id = split_id 
                };

                var dbResult = ExecuteStoredProcedure<Workout>(DbConnection(), procedureName, parameters);

                return dbResult.Cast<IWorkout>().ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<IWorkout> GetWorkoutHistoryByUserId(int userId)
        {
            try
            {
                string procedureName = "Workouts_GetByUserId";
                var parameters = new
                {
                    userId
                };

                var dbResult = ExecuteStoredProcedure<Workout>(DbConnection(), procedureName, parameters);

                return dbResult.Cast<IWorkout>().ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IWorkout StartWorkoutFromHistory(int userId, int workoutId)
        {
            throw new NotImplementedException();
        }
    }
}
