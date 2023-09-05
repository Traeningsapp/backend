using Domain.Workout;
using FluentValidation;

namespace Application.Validators
{
    public class WorkoutValidator : AbstractValidator<IWorkout>
    {
        public WorkoutValidator()
        {
            RuleFor(x => x.Exercises)
                .NotEmpty().WithMessage("A workout should consist of at least 1 Exercise.")
                .ForEach(exercise => exercise.SetValidator(new ExerciseValidator()));

        }
    }
}
