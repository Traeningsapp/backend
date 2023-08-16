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
                string procedureNavn = "Exercise_GetById";
                var parameters = new
                {
                    id = exerciseId,
                };

                var exercises = ExecuteStoredProcedure<Exercise>(DbConnection(), procedureNavn, parameters);
                IExercise exercise = exercises.First();

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
                string procedureNavn = "Muscles_GetByExerciseId";
                var parameters = new
                {
                    exerciseId,
                };

                var muscles = ExecuteStoredProcedure<Muscle>(DbConnection(), procedureNavn, parameters);
                
                return muscles.Cast<IMuscle>().ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
