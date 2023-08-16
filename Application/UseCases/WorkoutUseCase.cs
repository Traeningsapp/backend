using Application.Ports.Incoming;
using Application.Ports.Outgoing;
using Domain.Workout;

namespace Application.UseCases
{
    public class WorkoutUseCase : IWorkoutUseCase
    {
        private readonly IWorkoutRepository _workoutRepository;

        public WorkoutUseCase(IWorkoutRepository workoutRepository)
        {
            _workoutRepository = workoutRepository;
        }

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
