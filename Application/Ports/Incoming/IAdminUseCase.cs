namespace Application.Ports.Incoming
{
    public interface IAdminUseCase
    {
        void UpdateExerciseActiveFlag(int exerciseId, bool active, string userId);
    }
}
