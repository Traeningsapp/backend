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

            var filteredChestExercises = exercises
                .OrderByDescending(exercise => priorFavorites && exercise.IsFavorite)
                .ThenBy(exercise => random.Next()) // Add randomness within the favorite group
                .Where(exercise => exercise.Compound)
                .Where(exercise => exercise.Muscles.Any(muscle => muscle.MusclegroupId == 1 && muscle.isPrimary))
                .ToList();

            var favoriteExercises = filteredChestExercises
                .Where(exercise => priorFavorites && exercise.IsFavorite)
                .OrderBy(x => random.Next()) // Shuffle the favorite exercises
                .ToList();

            var nonFavoriteExercises = filteredChestExercises
                .Where(exercise => !exercise.IsFavorite)
                .OrderBy(x => random.Next()) // Shuffle the non-favorite exercises
                .ToList();

            while (chestExercises.Count < 2)
            {
                // Check if we can add favorite exercises
                while (favoriteExercises.Count >= 1)
                {
                    var favoriteToAdd = favoriteExercises[0];
                    favoriteExercises.RemoveAt(0);

                    // Check if the favoriteToAdd has a unique Muscle.Name
                    if (!chestExercises.Any(ce =>
                        ce.Muscles.Any(ceMuscle =>
                            favoriteToAdd.Muscles.Any(favoriteMuscle =>
                                favoriteMuscle.Name == ceMuscle.Name && favoriteMuscle.isPrimary == ceMuscle.isPrimary))))
                    {
                        chestExercises.Add(favoriteToAdd);
                    }
                }

                // Check if we need to add non-favorite exercises
                if (chestExercises.Count < 2 && nonFavoriteExercises.Count >= 1)
                {
                    var nonFavoriteToAdd = nonFavoriteExercises[0];
                    nonFavoriteExercises.RemoveAt(0);

                    // Check if the nonFavoriteToAdd has a unique combination of Muscle.Name and IsPrimary
                    if (chestExercises.Any(ce =>
                        ce.Muscles.Any(ceMuscle =>
                            nonFavoriteToAdd.Muscles.Any(nonFavoriteMuscle =>
                                nonFavoriteMuscle.Name != ceMuscle.Name && nonFavoriteMuscle.isPrimary == ceMuscle.isPrimary && nonFavoriteMuscle.isPrimary == true))))
                    {
                        chestExercises.Add(nonFavoriteToAdd);
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
                 .Where(exercise => exercise.Muscles.Any(muscle => muscle.MusclegroupId == 2 && muscle.isPrimary && muscle.Id != 12))
                 .GroupBy(exercise => exercise.Muscles
                     .Where(m => m.MusclegroupId == 2 && exercise.Muscles.Any(x => x.Id != 12) && m.isPrimary)
                     .Select(m => m.Name)
                     .Distinct()
                     .Count())
                 .Where(group => group.Any()) // Filter groups with at least 2 distinct muscle names
                 .OrderBy(x => random.Next())
                 .ToList();

            // Flatten the groups within filteredShoulderExercises and convert to a list of exercises
            var flattenedExercises = filteredShoulderExercises.SelectMany(group => group).ToList();

            // Ensure that flattenedExercises is not empty
            if (flattenedExercises.Any())
            {
                // Generate a random index within the range of flattenedExercises
                int randomIndex = random.Next(0, flattenedExercises.Count);

                // Add the randomly selected exercise to shoulderExercises
                shoulderExercises.Add(flattenedExercises[randomIndex]);
            }


            // adding non-starting compound exercise
            shoulderExercises.AddRange(
                exercises
                .OrderBy(exercise => Guid.NewGuid())
                .ThenByDescending(exercise => priorFavorites && exercise.IsFavorite)
                .Where(exercise => !exercise.Compound)
                .Where(exercise => exercise.Muscles.Any(m => m.MusclegroupId == 2 && m.isPrimary && m.Id != 12))
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
