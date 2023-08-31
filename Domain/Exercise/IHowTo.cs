using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exercise
{
    public interface IHowTo
    {
        int exerciseId { get; set; }
        int step { get; set; }
        string step_text { get; set; }
    }
}