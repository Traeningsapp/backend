using Application.Ports.Incoming;
using Application.Ports.Outgoing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
