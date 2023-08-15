using Domain.Workout;

namespace Application.Ports.Incoming
{
    public interface IWorkoutUseCase
    {
        IWorkout GenerateNewWorkout();
        IWorkout SaveWorkout(int userId, string workoutAsJson);
        List<IWorkout> GetWorkoutHistory(string userId);
        IWorkout StartWorkoutFromHistory(int userId, int workoutId);
    }
}
