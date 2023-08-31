using Domain.Exercise;

namespace Application.Ports.Incoming
{
    public interface IExerciseUseCase
    {
        List<IMusclegroup> GetMusclegroups();
        List<IMuscle> GetMusclesByMusclegroupId(int musclegroupId);
        IExercise? GetExercise(int exerciseId);
        List<IExercise> GetExercisesForMuscle(int musclegroupId);
        List<IExercise> GetFavoriteExercises(string userId);
        List<IExerciseStats> GetExerciseStats(int exerciseId, string userId);
        void SetFavoriteExercise(string userId, int exerciseId);
        void DeleteFavoriteExercise(string userId, int exerciseId);
    }
}
