using Domain.Exercise;
using Domain.User;
using Newtonsoft.Json;

namespace Domain.Workout
{
    public class Workout : IWorkout
    {
        public Workout()
        {
        }
        public Workout(IUser user)
        {
            _user = user;
            _exercises = new List<IExercise>();
        }

        private int _id;
        private string? _name;
        private IUser? _user;
        private List<IExercise>? _exercises;

        public int Id
        {
            get => _id; set => _id = value;
        }
        public string Name
        {
            get => _name = string.IsNullOrEmpty(_name) ? DateTime.Now.ToString() : _name;
            set => _name = value;
        }
        public IUser User
        {
            get => _user ??= new User.User(); set => _user = value;
        }
        public List<IExercise> Exercises
        {
            get => _exercises ??= new List<IExercise>(); set => _exercises = value;
        }

        public void FromJson(string workoutAsJson)
        {
            IWorkout? deserializedWorkout = JsonConvert.DeserializeObject<Workout>(workoutAsJson);

            if (deserializedWorkout != null)
            {
                _id = deserializedWorkout.Id;
                _name = deserializedWorkout.Name;
                _user = deserializedWorkout.User;
                _exercises = deserializedWorkout.Exercises;
            }
        }
    }
}
