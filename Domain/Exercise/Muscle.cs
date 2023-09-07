namespace Domain.Exercise
{
    public class Muscle : IMuscle
    {
        public Muscle()
        {
            
        }
        public Muscle(int id, int musclegroupId, string name, bool isPrimary)
        {
            _name = name;
            _id = id;
            _musclegroupId = musclegroupId;
            _isPrimary = isPrimary;
        }

        private int _id;
        private int _musclegroupId;
        private string _name = "";
        private bool _isPrimary;

        public int Id
        {
            get => _id; set => _id = value;
        }
        public int MusclegroupId
        {
            get => _musclegroupId; set => _musclegroupId = value;
        }
        public string Name
        {
            get => _name; set => _name = value;
        }
        public bool isPrimary 
        { 
            get => _isPrimary; set => _isPrimary = value; 
        }
    }
}
