namespace Domain.Exercise
{
    public interface IExercise
    {
        string Benefits { get; set; }
        string Description { get; set; }
        int Id { get; set; }
        bool IsCompound { get; set; }
        string Name { get; set; }
    }
}