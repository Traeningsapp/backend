using Domain.Workout;

namespace Domain.User
{
    public interface IUser
    {
        int Id { get; set; }
        List<IWorkout> WorkoutHistory { get; set; }
    }
}