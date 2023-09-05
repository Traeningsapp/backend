using Domain.Exercise;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class ExerciseValidator : AbstractValidator<IExercise>
    {
        public ExerciseValidator()
        {
            RuleFor(x => x.ExerciseStats)
                .NotNull().WithMessage("Stats cannot be null.")
                .ForEach(statsItem => statsItem.SetValidator(new ExerciseStatsValidator()));
        }
    }
}
