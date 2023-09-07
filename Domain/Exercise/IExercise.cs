namespace Domain.Exercise
{
    public interface IExercise
    {
        string Benefits { get; set; }
        string Description { get; set; }
        int Id { get; set; }
        bool Compound { get; set; }
        string Name { get; set; }
        bool Active { get; set; }
        bool IsFavorite { get; set; }
        int MusclegroupId { get; set; }
        List<IMuscle> Muscles { get; set; }
        List<IExerciseStats> ExerciseStats { get; set; }
        void MapStats(List<IExerciseStats> statsList);
    }
}