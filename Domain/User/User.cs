using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private string _id;
        private List<IWorkout> _workoutHistory;

        public string Id
        {
            get => _id; set => _id = value;
        }
        public List<IWorkout> WorkoutHistory
        {
            get => _workoutHistory; set => _workoutHistory = value;
        }
    }
}
