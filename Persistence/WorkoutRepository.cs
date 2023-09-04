using Application.Ports.Outgoing;
using Dapper;
using Domain.Exercise;
using Domain.Workout;
using System.Data;

namespace Persistence
{
    public class WorkoutRepository : BaseRepository, IWorkoutRepository
    {
        public WorkoutRepository(IConfigurationProvider configurationProvider) : base(configurationProvider)
        {
        }

        public int SaveWorkout(IWorkout workout, string splitType)
        {
            try
            {
                string procedureName = "Workout_Save";
                var inputParameters = new
                {
                    userId = workout.User.Id,
                    workoutName = workout.Name,
                    savedDate = workout.CreatedDate,
                    visibleToUser = workout.VisibleToUser,
                    splitType
                    
                };
                var dynamicParameters = new DynamicParameters(inputParameters);
                dynamicParameters.Add("workoutId", value: workout.Id, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
                dynamicParameters.Add("isNewWorkout", value: workout.Id, dbType: DbType.Boolean, direction: ParameterDirection.Output);

                ExecuteStoredProcedure(DbConnection(), procedureName, dynamicParameters);

                workout.Id = dynamicParameters.Get<int>("workoutId");
                bool isNewWorkout = dynamicParameters.Get<bool>("isNewWorkout");

                if (isNewWorkout)
                {
                    SaveExercisesInWorkout(workout);
                    SaveStatsInExercises(workout);
                }

                return workout.Id;
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
                    foreach (IExerciseStats stats in exercise.ExerciseStats)
                    {
                        var parameters = new
                        {
                            workoutId = workout.Id,
                            userId = workout.User.Id,
                            exerciseId = stats.ExerciseId,
                            setnr = stats.Setnr,
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

        public void deleteWorkout(int workoutId)
        {
            try
            {
                string procedureName = "Workout_removeWithWorkoutId";
                var parameters = new
                {
                    workoutId
                };

                ExecuteStoredProcedure(DbConnection(), procedureName, parameters);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
