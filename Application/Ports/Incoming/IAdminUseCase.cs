using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Ports.Incoming
{
    public interface IAdminUseCase
    {
        void UpdateExerciseActiveFlag(int exerciseId, bool active, string userId);
    }
}
