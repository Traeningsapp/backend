using Domain.Exercise;
using FluentValidation;

namespace Application.Validators
{
    public class ExerciseStatsValidator : AbstractValidator<IExerciseStats>
    {
        public ExerciseStatsValidator()
        {
            RuleFor(x => x.ExerciseId)
                .NotNull().WithMessage("Exercise Id should not be null.")
                .GreaterThan(0).WithMessage("Exercise id should be greater than 0.");

            RuleFor(x => x.Setnr)
                .NotNull().WithMessage("Setnr should not be null.")
                .GreaterThan(0).WithMessage("Setnr can't be null and should be greather than 0");

            RuleFor(x => x.Kilo)
                .NotNull().WithMessage("Kilo should not be null.")
                .InclusiveBetween(1, 999).WithMessage("Kilo should be between 1 and 999 (inclusive).");

        }
    }
}
