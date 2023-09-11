using Domain.Exercise;

namespace Application.Ports.Outgoing
{
    public interface IExerciseRepository
    {
        IExercise GetExerciseById(int id);
        List<IMuscle> GetMusclesInExerciseById(int exerciseId);
        List<IExercise> GetExercisesForMuscle(int muscleId);
        List<IMuscle> GetMusclesByMusclegroupId(int musclegroupId);
        List<IMusclegroup> GetMuscleGroups();
        List<IExercise> GetExercisesByWorkoutId(int workoutId);
        List<IExerciseStats> GetExerciseStats(int exerciseId, string userId);
        List<IExercise> GetFavoriteExercises(string userId);
        void SetFavoriteExercise(string userId, int exerciseId);
        void DeleteFavoriteExercise(string userId, int exerciseId);
        List<IExerciseStats> GetExerciseStatsByWorkoutId(int workoutId, int exerciseId);
        List<IHowTo> GetExerciseHowToByExerciseId(int exerciseId);
        bool GetFavoriteStatus(int exerciseId, string userId);
        void UpdateExerciseActiveFlag(int exerciseId, bool active, string userId, DateTime editedAt);
        List<IExercise> GetAllExercisesBySplitIdAndFavoritesByUserId(int splitId, string userId);
        List<IExercise> GetAbsExercises(string userId);
    }
}
