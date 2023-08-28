namespace Domain.Exercise
{
    public interface IExercise
    {
        string Benefits { get; set; }
        string Description { get; set; }
        int Id { get; set; }
        bool IsCompound { get; set; }
        string Name { get; set; }
        List<IMusclegroup> Musclegroups { get; set; }
        List<IMuscle> Muscles { get; set; }
        List<IExerciseStats> ExerciseStats { get; set; }
        void MapStats(List<IExerciseStats> statsList);
    }
}