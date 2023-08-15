namespace Domain.Exercise
{
    public class Exercise : IExercise
    {
        public Exercise(int id, string name, string description, string benefits, bool isCompound)
        {
            _id = id;
            _name = name;
            _description = description;
            _benefits = benefits;
            _isCompound = isCompound;
            _musclegroups = new List<IMusclegroup>();
        }

        private int _id;
        private string _name;
        private string _description;
        private string _benefits;
        private bool _isCompound;
        private List<IMusclegroup> _musclegroups;

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
        public List<IMusclegroup> Musclegroups
        {
            get => _musclegroups; set => _musclegroups = value;
        }
    }
}
