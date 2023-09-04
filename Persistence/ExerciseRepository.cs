using Application.Ports.Outgoing;
using Domain.Exercise;

namespace Persistence
{
    public class ExerciseRepository : BaseRepository, IExerciseRepository
    {
        public ExerciseRepository(IConfigurationProvider configurationProvider) : base(configurationProvider)
        {
        }

        public IExercise GetExerciseById(int exerciseId)
        {
            try
            {
                string procedureName = "Exercise_GetById";
                var parameters = new
                {
                    id = exerciseId,
                };

                var dbResult = ExecuteStoredProcedure<Exercise>(DbConnection(), procedureName, parameters);
                IExercise exercise = dbResult.First();

                return exercise;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public List<IMuscle> GetMusclesInExerciseById(int exerciseId)
        {
            try
            {
                string procedureName = "Muscles_GetByExerciseId";
                var parameters = new
                {
                    exerciseId,
                };

                var dbResult = ExecuteStoredProcedure<Muscle>(DbConnection(), procedureName, parameters);

                return dbResult.Cast<IMuscle>().ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<IExercise> GetExercisesForMuscle(int muscleId)
        {
            try
            {
                string procedureName = "Exercises_GetByMuscleId";
                var parameters = new
                {
                    muscleId
                };

                var dbResult = ExecuteStoredProcedure<Exercise>(DbConnection(), procedureName, parameters);

                return dbResult.Cast<IExercise>().ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<IMuscle> GetMusclesByMusclegroupId(int musclegroupId)
        {
            try
            {
                string procedureName = "dbo.Muscles_GetByMusclegroupId";
                var parameters = new
                {
                    musclegroupId
                };

                var dbResult = ExecuteStoredProcedure<Muscle>(DbConnection(), procedureName, parameters);

                return dbResult.Cast<IMuscle>().ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<IMusclegroup> GetMuscleGroups()
        {
            try
            {
                string procedureName = "Musclegroup_GetAll";
                var parameters = new { };

                var dbResult = ExecuteStoredProcedure<Musclegroup>(DbConnection(), procedureName, parameters);

                return dbResult.Cast<IMusclegroup>().ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<IExercise> GetExercisesByWorkoutId(int workoutId)
        {
            try
            {
                string procedureName = "Exercises_GetByWorkoutId";
                var parameters = new
                {
                    workoutId
                };

                var dbResult = ExecuteStoredProcedure<Exercise>(DbConnection(), procedureName, parameters);

                return dbResult.Cast<IExercise>().ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<IExerciseStats> GetExerciseStats(int exerciseId, string userId)
        {
            try
            {
                string procedureName = "Exercise_GetExerciseStats";
                var parameters = new
                {
                    exerciseId,
                    userId
                };

                var dbResult = ExecuteStoredProcedure<ExerciseStats>(DbConnection(), procedureName, parameters);

                return dbResult.Cast<IExerciseStats>().ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<IExercise> GetFavoriteExercises(string userId)
        {
            try
            {
                string procedureName = "Exercises_getFavorites";
                var parameters = new
                {
                    userId
                };

                var dbResult = ExecuteStoredProcedure<Exercise>(DbConnection(), procedureName, parameters);

                return dbResult.Cast<IExercise>().ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<IExerciseStats> GetExerciseStatsByWorkoutId(int workoutId, int exerciseId)
        {
            try
            {
                string procedureName = "Exercise_GetExerciseStatsByWorkoutId";
                var parameters = new
                {
                    workoutId,
                    exerciseId
                };

                var dbResult = ExecuteStoredProcedure<ExerciseStats>(DbConnection(), procedureName, parameters);

                return dbResult.Cast<IExerciseStats>().ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void SetFavoriteExercise(string userId, int exerciseId)
        {
            try
            {
                string procedureName = "Exercise_setFavorite";
                var parameters = new
                {
                    userId,
                    exerciseId
                };

                ExecuteStoredProcedure(DbConnection(), procedureName, parameters);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void DeleteFavoriteExercise(string userId, int exerciseId)
        {
            try
            {
                string procedureName = "Exercise_deleteFavorite";
                var parameters = new
                {
                    userId,
                    exerciseId
                };

                ExecuteStoredProcedure(DbConnection(), procedureName, parameters);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<IHowTo> GetExerciseHowToByExerciseId(int exerciseId)
        {
            try
            {
                string procedureName = "Exercise_getHowTo";
                var parameters = new
                {
                    exerciseId
                };

                var dbResult = ExecuteStoredProcedure<HowTo>(DbConnection(), procedureName, parameters);

                return dbResult.Cast<IHowTo>().ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool GetFavoriteStatus(int exerciseId, string userId)
        {
            try
            {
                string procedureName = "ExerciseFavoritestatus_GetByExerciseAndUser";
                var parameters = new
                {
                    exerciseId,
                    userId
                };

                var dbResult = ExecuteStoredProcedure<string>(DbConnection(), procedureName, parameters);

                return dbResult.Any();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void UpdateExerciseActiveFlag(int exerciseId, bool active, string userId, DateTime editedAt)
        {
            try
            {
                string procedureName = "Exercise_SetActiveStatus";
                var parameters = new
                {
                    exerciseId,
                    active,
                    userId,
                    editedAt
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
