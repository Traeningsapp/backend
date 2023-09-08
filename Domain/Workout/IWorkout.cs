using Domain.Exercise;
using Domain.User;

namespace Domain.Workout
{
    public interface IWorkout
    {
        List<IExercise> Exercises { get; set; }
        int Id { get; set; }
        string Name { get; set; }
        IUser User { get; set; }
        bool VisibleToUser { get; set; }
        DateTime CreatedDate { get; set; }
        string SplitType { get; set; }
        public void GenerateExercisesForPushSplit(Dictionary<string, List<IExercise>> exerciseDictionary, bool includeAbs, bool prioFavorites);
        public void GenerateExercisesForPullSplit(Dictionary<string, List<IExercise>> exerciseDictionary, bool includeAbs, bool prioFavorites);
        public void GenerateExercisesForLegsSplit(Dictionary<string, List<IExercise>> exerciseDictionary, bool includeAbs, bool prioFavorites);
    }
}