using Application.Ports.Outgoing;
using Domain.Exercise;

namespace Persistence
{
    public class ExerciseRepository : BaseRepository, IExerciseRepository
    {
        public ExerciseRepository(IConfigurationProvider configurationProvider) : base(configurationProvider)
        {
        }

        public IExercise? GetExerciseById(int exerciseId)
        {
            try
            {
                string procedureNavn = "Exercise_GetById";
                var parameters = new
                {
                    Id = exerciseId,
                };

                var exercises = ExecuteStoredProcedure<IExercise>(DbConnection(), procedureNavn, parameters);
                IExercise? exercise = exercises.FirstOrDefault();


                return exercise;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
