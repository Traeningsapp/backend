using Domain.Exercise;
using Domain.User;

namespace Domain.Workout
{
    public interface IWorkout
    {
        List<IExercise> Exercises { get; set; }
        int Id { get; set; }
        string Name { get; set; }
        IUser User { get; set; }

        void FromJson(string workoutAsJson);
    }
}