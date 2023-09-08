using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exercise
{
    public class HowTo : IHowTo
    {
        public HowTo()
        {
        }

        public HowTo(int exerciseId, int step, string step_text)
        {
            _exerciseId = exerciseId;
            _step = step;
            _step_text = step_text;
        }

        private int _exerciseId;
        private int _step;
        private string? _step_text;

        public int exerciseId
        {
            get => _exerciseId;
            set => _exerciseId = value;
        }

        public int step
        {
            get => _step;
            set => _step = value;
        }

        public string step_text
        {
            get => _step_text ??= "";
            set => _step_text = value;
        }
    }
}