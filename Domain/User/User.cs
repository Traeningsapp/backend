using Domain.Workout;

namespace Domain.User
{
    public class User : IUser
    {
        public User(int id)
        {
            _id = id;
            _workoutHistory = new List<IWorkout>();
        }

        public User()
        {
        }

        private int _id;
        private List<IWorkout>? _workoutHistory;

        public int Id
        {
            get => _id; set => _id = value;
        }
        public List<IWorkout> WorkoutHistory
        {
            get => _workoutHistory ??= new List<IWorkout>(); set => _workoutHistory = value;
        }
    }
}
