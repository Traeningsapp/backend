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
                string procedureName = "Exercises_GetByMsucleId";
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
                string procedureName = "Muscles_GetByMusclegroupId";
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
    }
}
