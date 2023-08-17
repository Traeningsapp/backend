using Application.Ports.Incoming;
using Application.Ports.Outgoing;
using Domain.User;
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

        public void SaveWorkout(int userId, string workoutAsJson)
        {
            try
            {
                IUser user = new User(userId);
                IWorkout workout = new Workout(user);

                workout.FromJson(workoutAsJson);

                _workoutRepository.SaveWorkout(workout);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IWorkout StartWorkoutFromHistory(int userId, int workoutId)
        {
            throw new NotImplementedException();
        }
    }
}
