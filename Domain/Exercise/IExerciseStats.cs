namespace Domain.Exercise
{
    public interface IExerciseStats
    {
        DateTime CreatedDate { get; set; }
        int ExerciseId { get; set; }
        int Kilo { get; set; }
        int Reps { get; set; }
        int SetNumber { get; set; }
        int UserId { get; set; }
        int WorkoutId { get; set; }
    }
}