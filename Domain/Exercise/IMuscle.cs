namespace Domain.Exercise
{
    public interface IMuscle
    {
        int Id { get; set; }
        int MusclegroupId { get; set; }
        string Name { get; set; }
        bool IsPrimary { get; set; }
    }
}