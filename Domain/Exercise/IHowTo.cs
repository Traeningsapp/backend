namespace Domain.Exercise
{
    public interface IHowTo
    {
        int exerciseId { get; set; }
        int step { get; set; }
        string step_text { get; set; }
    }
}