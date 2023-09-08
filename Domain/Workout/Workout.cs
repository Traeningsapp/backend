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

        public void GenerateExercisesForPushSplit(Dictionary<string, List<IExercise>> exerciseDictionary, bool includeAbs, bool prioFavorites)
        {

            Exercises.AddRange(SelectNonAbsExercises(exerciseDictionary["nonAbs"], prioFavorites));

            if (includeAbs)
                Exercises.AddRange(SelectAbsExercises(exerciseDictionary["abs"], prioFavorites));

        }

        static List<IExercise> SelectNonAbsExercises(List<IExercise> exercises, bool priorFavorites)
        {
            var random = new Random();

            List<IExercise> chestExercises = new List<IExercise>();
            // adding starting compound exercises
            var filteredChestExercises = exercises
                 .Where(exercise => exercise.Compound)
                 .Where(exercise => exercise.Muscles.Any(muscle => muscle.MusclegroupId == 1 && muscle.isPrimary))
                 .GroupBy(exercise => exercise.Muscles
                     .Where(m => m.MusclegroupId == 1 && m.isPrimary)
                     .Select(m => m.Name)
                     .Distinct()
                     .Count())
                 .Where(group => group.Count() >= 2) // Filter groups with at least 2 distinct muscle names
                 .ToList();

            // Flatten the groups and take 2 exercises
            while (chestExercises.Count < 2)
            {
                foreach (var group in filteredChestExercises)
                {
                    // Shuffle the exercises within each group
                    var shuffledGroup = group.OrderBy(x => random.Next()).ToList();

                    // Check if the first two exercises in the shuffled group have different muscle names
                    if (shuffledGroup.Count >= 2 && shuffledGroup[0].Muscles[0].Name != shuffledGroup[1].Muscles[0].Name)
                    {
                        chestExercises.Add(shuffledGroup[0]);
                        chestExercises.Add(shuffledGroup[1]);
                        break;
                    }
                }
            }

            // adding non-starting compound exercise
            chestExercises.AddRange(
                exercises
                .OrderBy(exercise => Guid.NewGuid())
                .ThenByDescending(exercise => priorFavorites && exercise.IsFavorite)
                .Where(exercise => !exercise.Compound)
                .Where(exercise => exercise.Muscles.Any(m => m.MusclegroupId == 1 && m.isPrimary))
                .Where(exercise => !chestExercises.Any(ce => exercise.Muscles.Any(exerciseMuscle =>
                    ce.Muscles.Any(ceMuscle => exerciseMuscle.isPrimary && ceMuscle.isPrimary && exerciseMuscle.Name == ceMuscle.Name))))
                .Take(1)
                );


            List<IExercise> shoulderExercises = new List<IExercise>();
            // adding starting compound exercises
            var filteredShoulderExercises = exercises
                 .Where(exercise => exercise.Compound)
                 .Where(exercise => exercise.Muscles.Any(muscle => muscle.MusclegroupId == 2 && muscle.isPrimary))
                 .GroupBy(exercise => exercise.Muscles
                     .Where(m => m.MusclegroupId == 2 && m.isPrimary)
                     .Select(m => m.Name)
                     .Distinct()
                     .Count())
                 .Where(group => group.Count() >= 2) // Filter groups with at least 2 distinct muscle names
                 .ToList();

            // Flatten the groups and take 2 exercises
            while (shoulderExercises.Count < 2)
            {
                foreach (var group in filteredShoulderExercises)
                {
                    // Shuffle the exercises within each group
                    var shuffledGroup = group.OrderBy(x => random.Next()).ToList();

                    // Check if the first two exercises in the shuffled group have different muscle names
                    if (shuffledGroup.Count >= 2 && shuffledGroup[0].Muscles[0].Name != shuffledGroup[1].Muscles[0].Name)
                    {
                        shoulderExercises.Add(shuffledGroup[0]);
                        shoulderExercises.Add(shuffledGroup[1]);
                        break;
                    }
                }
            }

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
