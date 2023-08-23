using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exercise
{
    public class ExerciseStats : IExerciseStats
    {
        public ExerciseStats()
        {
        }

        private int _workoutId;
        private string? _userId;
        private int _exerciseId;
        private DateTime _createdDate;
        private int _setNumber;
        private int _reps;
        private int _kilo;

        public int WorkoutId
        {
            get => _workoutId; set => _workoutId = value;
        }
        public string UserId
        {
            get => string.IsNullOrEmpty(_userId) ? "" : _userId; set => _userId = value;
        }
        public int ExerciseId
        {
            get => _exerciseId; set => _exerciseId = value;
        }
        public DateTime CreatedDate
        {
            get => _createdDate; set => _createdDate = value;
        }
        public int SetNumber
        {
            get => _setNumber; set => _setNumber = value;
        }
        public int Reps
        {
            get => _reps; set => _reps = value;
        }
        public int Kilo
        {
            get => _kilo; set => _kilo = value;
        }
    }
}
