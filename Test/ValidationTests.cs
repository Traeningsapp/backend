using Application.Validators;
using Domain.Exercise;
using Domain.Workout;
using FluentValidation.TestHelper;
using Moq;
using Xunit;

namespace Test
{
    public class ValidationTests
    {
        [Fact]
        public void WorkoutValidator_ShouldNotHaveValidationErrors_WhenWorkoutIsValid()
        {
            // Arrange
            var exerciseStats = new Mock<IExerciseStats>();
            exerciseStats.Setup(x => x.Setnr).Returns(1);
            exerciseStats.Setup(x => x.Kilo).Returns(50);
            exerciseStats.Setup(x => x.ExerciseId).Returns(1);

            var exercise = new Mock<IExercise>();
            exercise.Setup(x => x.ExerciseStats).Returns(new List<IExerciseStats>());
            exercise.Setup(x => x.Id).Returns(1);

            var workout = new Mock<IWorkout>();
            workout.Setup(x => x.Exercises).Returns(new List<IExercise> { exercise.Object });

            var validator = new WorkoutValidator();

            // Act
            var result = validator.TestValidate(workout.Object);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void WorkoutValidator_ShouldHaveValidationError_WhenNoExercises()
        {
            // Arrange
            var workout = new Mock<IWorkout>();
            workout.Setup(x => x.Exercises).Returns(new List<IExercise>());

            var validator = new WorkoutValidator();

            // Act
            var result = validator.TestValidate(workout.Object);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Exercises);
        }

        [Fact]
        public void ExerciseValidator_ShouldNotHaveValidationErrors_WhenExerciseIsValid()
        {
            // Arrange
            var exerciseStats = new Mock<IExerciseStats>();
            exerciseStats.Setup(x => x.Setnr).Returns(1);
            exerciseStats.Setup(x => x.Kilo).Returns(50);
            exerciseStats.Setup(x => x.ExerciseId).Returns(1);

            var exercise = new Mock<IExercise>();
            exercise.Setup(x => x.ExerciseStats).Returns(new List<IExerciseStats> { exerciseStats.Object });

            var validator = new ExerciseValidator();

            // Act
            var result = validator.TestValidate(exercise.Object);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void ExerciseStatsValidator_ShouldNotHaveValidationErrors_WhenExerciseStatsAreValid()
        {
            // Arrange
            var exerciseStats = new Mock<IExerciseStats>();
            exerciseStats.Setup(x => x.Setnr).Returns(1);
            exerciseStats.Setup(x => x.Kilo).Returns(50);
            exerciseStats.Setup(x => x.ExerciseId).Returns(1);

            var validator = new ExerciseStatsValidator();

            // Act
            var result = validator.TestValidate(exerciseStats.Object);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
