using Application.Ports.Incoming;
using Domain.Exercise;

namespace Application.UseCases
{
    public class ExerciseUseCase : IExerciseUseCase
    {
        public IExercise GetExercise(int exerciseId)
        {
            throw new NotImplementedException();
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
