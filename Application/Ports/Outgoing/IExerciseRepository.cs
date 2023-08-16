using Domain.Exercise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Ports.Outgoing
{
    public interface IExerciseRepository
    {
        IExercise GetExerciseById(int id);
        List<IMuscle> GetMusclesInExerciseById(int exerciseId);
    }
}
