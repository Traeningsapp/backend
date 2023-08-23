using Domain.Workout;

namespace Domain.User
{
    public class User : IUser
    {
        public User(string id)
        {
            _id = id;
            _workoutHistory = new List<IWorkout>();
        }

        public User()
        {
        }

        private string? _id;
        private List<IWorkout>? _workoutHistory;

        public string Id
        {
            get => string.IsNullOrEmpty(_id) ? "" : _id; set => _id = value;
        }
        public List<IWorkout> WorkoutHistory
        {
            get => _workoutHistory ??= new List<IWorkout>(); set => _workoutHistory = value;
        }
    }
}
