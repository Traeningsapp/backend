using Domain.Exercise;

namespace Application.Ports.Incoming
{
    public interface IExerciseUseCase
    {
        List<IMusclegroup> GetMusclegroups();
        List<IMuscle> GetExercisesInMusclegroup(int musclegroupId);
        IExercise GetExercise(int exerciseId);
        List<IExercise> GetExercisesForMuscle(int musclegroupId);
    }
}
