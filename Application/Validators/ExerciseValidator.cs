using Domain.Exercise;
using FluentValidation;

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
