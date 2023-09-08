using Domain.Workout;

namespace Application.Ports.Incoming
{
    public interface IWorkoutUseCase
    {
        IWorkout GenerateNewWorkout(int split_id, string userId, bool includeAbs, bool prioFavorites);
        int SaveWorkout(IWorkout workout);
        List<IWorkout> GetWorkoutHistory(string userId);
        IWorkout StartWorkoutFromHistory(string userId, int workoutId);
        void DeleteWorkout(int workoutId);
    }
}
