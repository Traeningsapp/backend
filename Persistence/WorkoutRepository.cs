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

                var dbResult = ExecuteStoredProcedure<int>(DbConnection(), procedureName, parameters);
                workout.Id = dbResult.Cast<int>().First();

                SaveExercisesInWorkout(workout);
                SaveStatsInExercises(workout);
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

        private void SaveStatsInExercises(IWorkout workout)
        {
            try
            {
                string procedureName = "ExerciseStats_SaveStats";

                foreach (IExercise exercise in workout.Exercises)
                {
                    foreach (IExerciseStats stats in exercise.Stats)
                    {
                        var parameters = new
                        {
                            workoutId = workout.Id,
                            userId = workout.User.Id,
                            exerciseId = stats.ExerciseId,
                            setNumber = stats.SetNumber,
                            reps = stats.Reps,
                            kilo = stats.Kilo,
                            createdDate = stats.CreatedDate,
                        };

                        ExecuteStoredProcedure(DbConnection(), procedureName, parameters);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<IExercise> GenerateExercisesForNewWorkout(int splitId)
        {
            try
            {
                string procedureName = "Workout_GetNew";

                var parameters = new
                {
                    splitId
                };

                var dbResult = ExecuteStoredProcedure<Exercise>(DbConnection(), procedureName, parameters);

                return dbResult.Cast<IExercise>().ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<IWorkout> GetWorkoutHistoryByUserId(string userId)
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

        public IWorkout StartWorkoutFromHistory(string userId, int workoutId)
        {
            throw new NotImplementedException();
        }
    }
}
