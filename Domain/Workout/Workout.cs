using Domain.Exercise;
using Domain.User;
using Newtonsoft.Json;
using System.Globalization;

namespace Domain.Workout
{
    public class Workout : IWorkout
    {
        public Workout()
        {
        }
        public Workout(IUser user)
        {
            _user = user;
            _exercises = new List<IExercise>();
        }

        private int _id;
        private string? _name;
        private IUser? _user;
        private List<IExercise>? _exercises;
        private bool _visibleToUser;
        private DateTime _createdDate;
        private string? _splitType;
        private string? _userId;

        public int Id
        {
            get => _id; set => _id = value;
        }
        [JsonProperty("name")]
        public string Name
        {
            get => _name = string.IsNullOrEmpty(_name) ? DateTime.Now.ToString() : _name;
            set => _name = value;
        }
        public IUser User
        {
            get => _user ??= new User.User(); set => _user = value;
        }
        public List<IExercise> Exercises
        {
            get => _exercises ??= new List<IExercise>(); set => _exercises = value;
        }
        public bool VisibleToUser
        {
            get => _visibleToUser; set => _visibleToUser = value;
        }
        public DateTime CreatedDate
        {
            get => _createdDate; set => _createdDate = value;
        }
        public string SplitType
        {
            get => _splitType ??= ""; set => _splitType = value;
        }
        public string UserId
        {
            get => _userId ??= ""; set => _userId = value;
        }

        public void GenerateExercisesForPushSplit(Dictionary<string, List<IExercise>> exerciseDictionary, bool includeAbs, bool priorFavorites)
        {

            Exercises.AddRange(SelectNonAbsExercises(exerciseDictionary["nonAbs"], 8, priorFavorites));
            
            if(includeAbs)
                Exercises.AddRange(SelectAbsExercises(exerciseDictionary["abs"], priorFavorites));

        }

        static List<IExercise> SelectNonAbsExercises(List<IExercise> exercises, int count, bool priorFavorites)
        {

            List<IExercise> chestExercises = new List<IExercise>();
            // adding starting compound exercises
            chestExercises.AddRange(
                exercises
                .OrderBy(exercise => Guid.NewGuid())
                .ThenByDescending(exercise => priorFavorites && exercise.IsFavorite)
                .Where(exercise => exercise.Compound)
                .Where(exercise => exercise.Muscles.Any(muscle => muscle.MusclegroupId == 1))
                .GroupBy(exercise => exercise.Muscles.SelectMany(muscle => muscle.Name).Distinct().Count())
                .SelectMany(group => group.Take(1))
                .Take(2)
                );

            // adding non-starting compound exercise
            chestExercises.AddRange(
                exercises
                .OrderBy(exercise => Guid.NewGuid())
                .ThenByDescending(exercise => priorFavorites && exercise.IsFavorite)
                .Where(exercise => !exercise.Compound)
                .Where(exercise => exercise.Muscles.Any(m => m.MusclegroupId == 1))
                .Where(exercise => !chestExercises.Any(ce => exercise.Muscles.Any(exerciseMuscle =>
                    ce.Muscles.Any(ceMuscle => exerciseMuscle.isPrimary && ceMuscle.isPrimary && exerciseMuscle.Name == ceMuscle.Name))))
                .Take(1)
                );

            List <IExercise> shoulderExercises = new List<IExercise>();
            // adding starting compound exercises
            shoulderExercises.AddRange(
                exercises
                .OrderBy(exercise => Guid.NewGuid())
                .ThenByDescending(exercise => priorFavorites && exercise.IsFavorite)
                .Where(exercise => exercise.Compound)
                .Where(exercise => exercise.Muscles.Any(muscle => muscle.MusclegroupId == 2 && muscle.isPrimary && muscle.Id != 12))
                .GroupBy(exercise => exercise.Muscles.Select(muscle => muscle.Name).Distinct().Count())
                .SelectMany(group => group.Take(1))
                .Take(1)
                );

            // adding non-starting compound exercise
            shoulderExercises.AddRange(
                exercises
                .OrderBy(exercise => Guid.NewGuid())
                .ThenByDescending(exercise => priorFavorites && exercise.IsFavorite)
                .Where(exercise => !exercise.Compound)
                .Where(exercise => exercise.Muscles.Any(m => m.MusclegroupId == 2 && m.Id != 12))
                .Where(exercise => !shoulderExercises.Any(ce => exercise.Muscles.Any(exerciseMuscle =>
                    ce.Muscles.Any(ceMuscle => exerciseMuscle.isPrimary && ceMuscle.isPrimary && exerciseMuscle.Name == ceMuscle.Name))))
                .Take(1)
                );

            List<IExercise> tricepsExercises = new List<IExercise>();
            // adding non-starting compound exercise
            tricepsExercises.AddRange(
                exercises
                .OrderBy(exercise => Guid.NewGuid())
                .ThenByDescending(exercise => priorFavorites && exercise.IsFavorite)
                .Where(exercise => !exercise.Compound)
                .Where(exercise => exercise.Muscles.Any(m => m.Id == 12 && m.isPrimary))
                //.Where(exercise => !tricepsExercises.Any(ce => exercise.Muscles.Any(exerciseMuscle =>
                //    ce.Muscles.Any(ceMuscle => exerciseMuscle.isPrimary && ceMuscle.isPrimary && exerciseMuscle.Name == ceMuscle.Name))))
                .Take(2)
                );

            List<IExercise> pushExercises = new();
            pushExercises.AddRange(chestExercises);
            pushExercises.AddRange(shoulderExercises);
            pushExercises.AddRange(tricepsExercises);

            return pushExercises;

        }

        static List<IExercise> SelectAbsExercises(List<IExercise> exercises, bool priorFavorites)
        {
            List<IExercise> absExercises = new()
            {
                exercises
                .OrderBy(exercise => Guid.NewGuid())
                .ThenByDescending(exercise => priorFavorites && exercise.IsFavorite)
                .First(x => x.Muscles.Any(muscle => muscle.Name == "Obliques" && muscle.isPrimary)),

                exercises
                .OrderBy(exercise => Guid.NewGuid())
                .ThenByDescending(exercise => priorFavorites && exercise.IsFavorite)
                .First(x => x.Muscles.Any(muscle => muscle.Name == "Abdominals" && muscle.isPrimary))
            };

            return absExercises;
        }

        public void GenerateExercisesForPullSplit(Dictionary<string, List<IExercise>> exerciseDictionary, bool includeAbs, bool priorFavorites)
        {
            throw new NotImplementedException();
        }

        public void GenerateExercisesForLegsSplit(Dictionary<string, List<IExercise>> exerciseDictionary, bool includeAbs, bool priorFavorites)
        {
            throw new NotImplementedException();
        }

    }
}
