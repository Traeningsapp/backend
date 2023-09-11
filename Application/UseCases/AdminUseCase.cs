using Application.Ports.Incoming;
using Application.Ports.Outgoing;

namespace Application.UseCases
{
    public class AdminUseCase : IAdminUseCase
    {
        private readonly IExerciseRepository _exerciseRepository;

        public AdminUseCase(IExerciseRepository exerciseRepository)
        {
            _exerciseRepository = exerciseRepository;
        }
        public void UpdateExerciseActiveFlag(int exerciseId, bool active, string userId)
        {
            try
            {
                DateTime editedAt = DateTime.Now;
                _exerciseRepository.UpdateExerciseActiveFlag(exerciseId, active, userId, editedAt);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
