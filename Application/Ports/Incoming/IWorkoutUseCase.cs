using Domain.Workout;

namespace Application.Ports.Incoming
{
    public interface IWorkoutUseCase
    {
        IWorkout GenerateNewWorkout(int split_id, string userId);
        int SaveWorkout(string userId, string workoutAsJsonm, string splitType);
        List<IWorkout> GetWorkoutHistory(string userId);
        IWorkout StartWorkoutFromHistory(string userId, int workoutId);
        void deleteWorkout(int workoutId);
    }
}
