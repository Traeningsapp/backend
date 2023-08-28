using Domain.Workout;

namespace Application.Ports.Incoming
{
    public interface IWorkoutUseCase
    {
        IWorkout GenerateNewWorkout(int split_id, string userId);
        void SaveWorkout(string userId, string workoutAsJson, string exerciseStatsAsJson);
        List<IWorkout> GetWorkoutHistory(string userId);
        IWorkout StartWorkoutFromHistory(string userId, int workoutId);
    }
}
