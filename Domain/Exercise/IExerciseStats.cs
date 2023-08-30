namespace Domain.Exercise
{
    public interface IExerciseStats
    {
        DateTime CreatedDate { get; set; }
        int ExerciseId { get; set; }
        int Kilo { get; set; }
        int Reps { get; set; }
        int Setnr { get; set; }
        string UserId { get; set; }
    }
}