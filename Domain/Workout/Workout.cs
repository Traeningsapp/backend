using Domain.Exercise;
using Domain.User;

namespace Domain.Workout
{
    public class Workout : IWorkout
    {
        public Workout(IUser user)
        {
            _user = user;
            _exercises = new List<IExercise>();
        }

        private int? _id;
        private IUser _user;
        private List<IExercise> _exercises;

        public int? Id
        {
            get => _id; set => _id = value;
        }
        public IUser User
        {
            get => _user; set => _user = value;
        }
        public List<IExercise> Exercises
        {
            get => _exercises; set => _exercises = value;
        }
    }
}
