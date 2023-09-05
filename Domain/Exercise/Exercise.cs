namespace Domain.Exercise
{
    public class Exercise : IExercise
    {
        public Exercise()
        {
        }
        public Exercise(int id, string name, string description, string benefits, bool isCompound, bool active)
        {
            _id = id;
            _name = name;
            _description = description;
            _benefits = benefits;
            _isCompound = isCompound;
            _active = active;
            _musclegroups = new List<IMusclegroup>();
            _muscleActivation = new List<IMuscle>();
            _exerciseStats = new List<IExerciseStats>();
        }

        private int _id;
        private string _name = "";
        private string _description = "";
        private string _benefits = "";
        private bool _isCompound;
        private bool _active;
        private string? _assets = "";
        private List<IMusclegroup>? _musclegroups;
        private List<IMuscle>? _muscleActivation;
        private List<IExerciseStats>? _exerciseStats;

        public int Id
        {
            get => _id; set => _id = value;
        }
        public string Name
        {
            get => _name; set => _name = value;
        }
        public string Description
        {
            get => _description; set => _description = value;
        }
        public string Benefits
        {
            get => _benefits; set => _benefits = value;
        }
        public bool IsCompound
        {
            get => _isCompound; set => _isCompound = value;
        }
        public bool Active
        {
            get => _active; set => _active = value;
        }
        public List<IMusclegroup> Musclegroups
        {
            get => _musclegroups ??= new List<IMusclegroup>(); set => _musclegroups = value;
        }
        public List<IMuscle> Muscles
        {
            get => _muscleActivation ??= new List<IMuscle>(); set => _muscleActivation = value;
        }
        public List<IExerciseStats> ExerciseStats
        {
            get => _exerciseStats ??= new List<IExerciseStats>(); set => _exerciseStats = value;
        }

        public void MapStats(List<IExerciseStats> statsList)
        {
            ExerciseStats = statsList.Where(stat => stat.ExerciseId == Id).ToList();
        }
    }
}
