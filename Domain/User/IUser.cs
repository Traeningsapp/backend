using Domain.Workout;

namespace Domain.User
{
    public interface IUser
    {
        string Id { get; set; }
        List<IWorkout> WorkoutHistory { get; set; }
    }
}