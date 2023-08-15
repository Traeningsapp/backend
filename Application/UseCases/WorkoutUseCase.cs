using Application.Ports.Incoming;
using Domain.Workout;

namespace Application.UseCases
{
    public class WorkoutUseCase : IWorkoutUseCase
    {
        public IWorkout GenerateNewWorkout()
        {
            throw new NotImplementedException();
        }

        public List<IWorkout> GetWorkoutHistory(string userId)
        {
            throw new NotImplementedException();
        }

        public IWorkout SaveWorkout(int userId, string workoutAsJson)
        {
            throw new NotImplementedException();
        }

        public IWorkout StartWorkoutFromHistory(int userId, int workoutId)
        {
            throw new NotImplementedException();
        }
    }
}
