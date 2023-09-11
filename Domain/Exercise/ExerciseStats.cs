namespace Domain.Exercise
{
    public class ExerciseStats : IExerciseStats
    {
        public ExerciseStats()
        {
        }

        private string? _userId;
        private int _exerciseId;
        private DateTime _createdDate;
        private int _setnr;
        private int _reps;
        private int _kilo;

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
        public int Setnr
        {
            get => _setnr; set => _setnr = value;
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
