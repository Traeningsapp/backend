namespace Domain.Exercise
{
    public class Muscle : IMuscle
    {
        public Muscle()
        {
            
        }
        public Muscle(int id, int musclegroupId, string name)
        {
            _name = name;
            _id = id;
            _musclegroupId = musclegroupId;
        }

        private int _id;
        private int _musclegroupId;
        private string _name = "";

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
    }
}
