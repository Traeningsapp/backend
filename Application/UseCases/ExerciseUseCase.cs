using Application.Ports.Incoming;
using Application.Ports.Outgoing;
using Domain.Exercise;

namespace Application.UseCases
{
    public class ExerciseUseCase : IExerciseUseCase
    {
        private readonly IExerciseRepository _exerciseRepository;

        public ExerciseUseCase(IExerciseRepository exerciseRepository)
        {
            _exerciseRepository = exerciseRepository;
        }

        public IExercise GetExercise(int exerciseId)
        {
            try
            {
                IExercise exercise = _exerciseRepository.GetExerciseById(exerciseId);
                exercise.Muscles = _exerciseRepository.GetMusclesInExerciseById(exerciseId);

                return exercise;
            }
            catch (Exception e)
            {
                throw new Exception("Couldn't fetch exercise.");
            }
        }

        public List<IExercise> GetExercisesForMuscle(int musclegroupId)
        {
            throw new NotImplementedException();
        }

        public List<IMuscle> GetExercisesInMusclegroup(int musclegroupId)
        {
            throw new NotImplementedException();
        }

        public List<IMusclegroup> GetMusclegroups()
        {
            throw new NotImplementedException();
        }
    }
}
