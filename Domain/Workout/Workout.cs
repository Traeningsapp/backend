using Domain.Exercise;
using Domain.User;
using Newtonsoft.Json;

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

        private List<IExercise>? _chestExercises;
        private List<IExercise>? _shoulderExercises;
        private List<IExercise>? _tricepsExercises;
        private List<IExercise>? _absExercises;
        private List<IExercise>? _backExercises;
        private List<IExercise>? _bicepsExercises;
        private List<IExercise>? _legsExercises;
        private List<IExercise>? _calvesExercises;


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

            Exercises.AddRange(SelectNonAbsExercisesForPushSplit(exerciseDictionary["nonAbs"], prioFavorites));

            if (includeAbs)
                Exercises.AddRange(SelectAbsExercises(exerciseDictionary["abs"], prioFavorites));

        }

        public void GenerateExercisesForPullSplit(Dictionary<string, List<IExercise>> exerciseDictionary, bool includeAbs, bool prioFavorites)
        {
            Exercises.AddRange(SelectNonAbsExercisesForPullSplit(exerciseDictionary["nonAbs"], prioFavorites));

            if (includeAbs)
                Exercises.AddRange(SelectAbsExercises(exerciseDictionary["abs"], prioFavorites));
        }

        public void GenerateExercisesForLegsSplit(Dictionary<string, List<IExercise>> exerciseDictionary, bool includeAbs, bool prioFavorites)
        {
            Exercises.AddRange(SelectNonAbsExercisesForLegsSplit(exerciseDictionary["nonAbs"], prioFavorites));

            if (includeAbs)
                Exercises.AddRange(SelectAbsExercises(exerciseDictionary["abs"], prioFavorites));
        }

        private List<IExercise> SelectNonAbsExercisesForLegsSplit(List<IExercise> exercises, bool prioFavorites)
        {
            AddLegsExercises(exercises, prioFavorites, 1, 5);
            AddCalvesExercises(exercises, prioFavorites, 1);

            List<IExercise> legsExercises = new();
            legsExercises.AddRange(_legsExercises ??= new List<IExercise>());
            legsExercises.AddRange(_calvesExercises ??= new List<IExercise>());

            return legsExercises;
        }

        private List<IExercise> SelectNonAbsExercisesForPushSplit(List<IExercise> exercises, bool prioFavorites)
        {
            AddChestExercises(exercises, prioFavorites, 2, 1);
            AddShoulderExercises(exercises, prioFavorites, 1, 1);
            AddTricepsExercises(exercises, prioFavorites, 2);

            List<IExercise> pushExercises = new();
            pushExercises.AddRange(_chestExercises ??= new List<IExercise>());
            pushExercises.AddRange(_shoulderExercises ??= new List<IExercise>());
            pushExercises.AddRange(_tricepsExercises ??= new List<IExercise>());

            return pushExercises;
        }

        private List<IExercise> SelectNonAbsExercisesForPullSplit(List<IExercise> exercises, bool prioFavorites)
        {
            AddBackExercises(exercises, prioFavorites, 2, 2);
            AddRearDeltsExercises(exercises, prioFavorites, 1);
            AddBicepsExercises(exercises, prioFavorites, 2);

            List<IExercise> pullExercises = new();
            pullExercises.AddRange(_backExercises ??= new List<IExercise>());
            pullExercises.AddRange(_shoulderExercises ??= new List<IExercise>());
            pullExercises.AddRange(_bicepsExercises ??= new List<IExercise>());

            return pullExercises;
        }

        private List<IExercise> SelectAbsExercises(List<IExercise> exercises, bool priorFavorites)
        {
            AddAbsExercises(exercises, priorFavorites, 2);

            List<IExercise> absExercises = new();
            absExercises.AddRange(_absExercises ??= new List<IExercise>());

            return absExercises;
        }

        private void AddCalvesExercises(List<IExercise> exercises, bool prioFavorites, int amountToAdd)
        {
            var random = new Random();
            _calvesExercises = new();
            int calvesMuscleId = 17;

            var calvesExercises = exercises
                .Where(exercise => exercise.Muscles.Any(muscle => muscle.Id == calvesMuscleId && muscle.IsPrimary))
                .OrderBy(x => random.Next())
                .ToList();

            if (prioFavorites)
                calvesExercises = calvesExercises
                    .OrderByDescending(exercise => exercise.IsFavorite)
                    .ToList();

            _calvesExercises = calvesExercises
                .Take(amountToAdd)
                .ToList();
        }

        private void AddRearDeltsExercises(List<IExercise> exercises, bool prioFavorites, int amountToAdd)
        {
            var random = new Random();
            _shoulderExercises = new();
            int readDeltsMuscleId = 6;

            var rearDeltsExercises = exercises
                .Where(exercise => exercise.Muscles.Any(muscle => muscle.Id == readDeltsMuscleId && muscle.IsPrimary))
                .OrderBy(x => random.Next())
                .ToList();

            if (prioFavorites)
                rearDeltsExercises = rearDeltsExercises
                    .OrderByDescending(exercise => exercise.IsFavorite)
                    .ToList();

            _shoulderExercises = rearDeltsExercises
                .Take(amountToAdd)
                .ToList();
        }

        private void AddBicepsExercises(List<IExercise> exercises, bool prioFavorites, int amountToAdd)
        {
            var random = new Random();
            _bicepsExercises = new();
            int bicepsMuscleId = 13;

            var rearDeltsExercises = exercises
                .Where(exercise => exercise.Muscles.Any(muscle => muscle.Id == bicepsMuscleId && muscle.IsPrimary))
                .OrderBy(x => random.Next())
                .ToList();

            if (prioFavorites)
                rearDeltsExercises = rearDeltsExercises
                    .OrderByDescending(exercise => exercise.IsFavorite)
                    .ToList();

            _bicepsExercises = rearDeltsExercises
                .Take(amountToAdd)
                .ToList();
        }

        private void AddLegsExercises(List<IExercise> exercises, bool prioFavorites, int amountOfCompoundToAdd, int amountOfNonCompoundToAdd)
        {
            _legsExercises = new();
            int legsMusclegroupId = 5;

            var compoundLegsExercises = FilterCompoundExercises(exercises, legsMusclegroupId);
            var nonCompoundLegsExercises = FilterNonCompoundExercises(exercises, legsMusclegroupId);
            var favoriteCompoundLegsExercises = FilterFavoriteCompoundExercises(compoundLegsExercises);
            var nonFavoriteCompoundLegsExercises = FilterNonFavoriteCompoundExercises(compoundLegsExercises);

            // adding starting compound exercises
            while (_legsExercises.Count < amountOfCompoundToAdd)
            {
                if (prioFavorites)
                {
                    _legsExercises = AddFavoriteExercises(favoriteCompoundLegsExercises, amountOfCompoundToAdd);
                    _legsExercises = AddNonFavoriteExercises(nonFavoriteCompoundLegsExercises, _legsExercises, amountOfCompoundToAdd);
                }
                else
                {
                    _legsExercises = AddExercises(compoundLegsExercises, amountOfCompoundToAdd);
                }

                int quadsMuscleId = 14;
                int glutesMuscleId = 15;
                int hamstringsMuscleId = 16;


                Dictionary<int, int> legsExerciseamounts = new()
                {
                    { quadsMuscleId, 2 },
                    { glutesMuscleId, 2 },
                    { hamstringsMuscleId, 2 }
                };

                int currentMuscleIdInList = _legsExercises
                    .SelectMany(exercise => exercise.Muscles)
                    .Where(muscle => muscle.IsPrimary)
                    .Select(muscle => muscle.Id)
                    .First();

                // adding non-starting compound exercises
                foreach (var kvp in legsExerciseamounts)
                {
                    List<IExercise> exercisesToAdd = new();
                    int amountToAdd = kvp.Value;
                    if (currentMuscleIdInList == kvp.Key)
                        amountToAdd--;

                    exercisesToAdd.AddRange(AddNonStartingCompoundExercises(nonCompoundLegsExercises, exercisesToAdd, prioFavorites, amountToAdd, isSamePrimary: true, isLegsExercises: true, muscleId: kvp.Key));
                    _legsExercises.AddRange(exercisesToAdd);
                }
            }
        }

        private void AddBackExercises(List<IExercise> exercises, bool prioFavorites, int amountOfCompoundToAdd, int amountOfNonCompoundToAdd)
        {
            _backExercises = new();
            int backMusclegroupId = 3;
            int teresMajorAndMinorMuscleId = 10;

            var compoundBackExercises = FilterCompoundExercises(exercises, backMusclegroupId, teresMajorAndMinorMuscleId);
            var nonCompoundBackExercises = FilterNonCompoundExercises(exercises, backMusclegroupId);
            var favoriteCompoundBackExercises = FilterFavoriteCompoundExercises(compoundBackExercises);
            var nonFavoriteCompoundBackExercises = FilterNonFavoriteCompoundExercises(compoundBackExercises);

            // adding starting compound exercises
            while (_backExercises.Count < amountOfCompoundToAdd)
            {
                if (prioFavorites)
                {
                    _backExercises = AddFavoriteExercises(favoriteCompoundBackExercises, amountOfCompoundToAdd);
                    _backExercises = AddNonFavoriteExercises(nonFavoriteCompoundBackExercises, _backExercises, amountOfCompoundToAdd);
                }
                else
                {
                    _backExercises = AddExercises(compoundBackExercises, amountOfCompoundToAdd);
                }
            }
            // adding non-starting compound exercises
            _backExercises.AddRange(AddNonStartingCompoundExercises(nonCompoundBackExercises, _backExercises, prioFavorites, amountOfNonCompoundToAdd, isSamePrimary: false));
        }

        private void AddChestExercises(List<IExercise> exercises, bool prioFavorites, int amountOfCompoundToAdd, int amountOfNonCompoundToAdd)
        {
            _chestExercises = new();
            int chestMusclegroupId = 1;

            var compoundChestExercises = FilterCompoundExercises(exercises, chestMusclegroupId);
            var nonCompoundChestExercises = FilterNonCompoundExercises(exercises, chestMusclegroupId);
            var favoriteCompoundChestExercises = FilterFavoriteCompoundExercises(compoundChestExercises);
            var nonFavoriteCompoundChestExercises = FilterNonFavoriteCompoundExercises(compoundChestExercises);

            // adding starting compound exercises
            while (_chestExercises.Count < amountOfCompoundToAdd)
            {
                if (prioFavorites)
                {
                    _chestExercises = AddFavoriteExercises(favoriteCompoundChestExercises, amountOfCompoundToAdd);
                    _chestExercises = AddNonFavoriteExercises(nonFavoriteCompoundChestExercises, _chestExercises, amountOfCompoundToAdd);
                }
                else
                {
                    _chestExercises = AddExercises(compoundChestExercises, amountOfCompoundToAdd);
                }
            }
            // adding non-starting compound exercises
            _chestExercises.AddRange(AddNonStartingCompoundExercises(nonCompoundChestExercises, _chestExercises, prioFavorites, amountOfNonCompoundToAdd));
        }

        private void AddShoulderExercises(List<IExercise> exercises, bool prioFavorites, int amountOfCompoundToAdd, int amountOfNonCompoundToAdd)
        {
            _shoulderExercises = new();
            int shouldersMusclegroupId = 2;
            int tricepsMuscleId = 12;

            var compoundShoulderExercises = FilterCompoundExercises(exercises, shouldersMusclegroupId, tricepsMuscleId);
            var nonCompoundShoulderExercises = FilterNonCompoundExercises(exercises, shouldersMusclegroupId, tricepsMuscleId);
            var favoriteCompoundShoulderExercises = FilterFavoriteCompoundExercises(compoundShoulderExercises);
            var nonFavoriteCompoundShoulderExercise = FilterNonFavoriteCompoundExercises(compoundShoulderExercises);

            // adding starting compound exercises
            while (_shoulderExercises.Count < amountOfCompoundToAdd)
            {
                if (prioFavorites)
                {
                    _shoulderExercises = AddFavoriteExercises(favoriteCompoundShoulderExercises, amountOfCompoundToAdd);
                    _shoulderExercises = AddNonFavoriteExercises(nonFavoriteCompoundShoulderExercise, _shoulderExercises, amountOfCompoundToAdd);
                }
                else
                {
                    _shoulderExercises = AddExercises(compoundShoulderExercises, amountOfCompoundToAdd);
                }
            }

            // adding non-starting compound exercises
            _shoulderExercises.AddRange(AddNonStartingCompoundExercises(nonCompoundShoulderExercises, _shoulderExercises, prioFavorites, amountOfNonCompoundToAdd));
        }

        private void AddTricepsExercises(List<IExercise> exercises, bool prioFavorites, int AmountToAdd)
        {
            var random = new Random();
            _tricepsExercises = new();
            int tricepsMuscleId = 12;

            var tricepsExercises = exercises
                .Where(exercise => exercise.Muscles.Any(muscle => muscle.Id == tricepsMuscleId && muscle.IsPrimary))
                .OrderBy(x => random.Next())
                .ToList();

            if (prioFavorites)
                tricepsExercises = tricepsExercises
                    .OrderByDescending(exercise => exercise.IsFavorite)
                    .ToList();

            _tricepsExercises = tricepsExercises
                .Take(AmountToAdd)
                .ToList();
        }

        private void AddAbsExercises(List<IExercise> exercises, bool prioFavorites, int amountToAdd)
        {
            var random = new Random();
            _absExercises = new();

            exercises = exercises
                .OrderBy(x => random.Next()) // Shuffle the exercises
                .ToList();

            _absExercises = AddNonStartingCompoundExercises(exercises, _absExercises, prioFavorites, amountToAdd);
        }
        private static List<IExercise> AddFavoriteExercises(List<IExercise> favoriteExercises, int amountToAdd)
        {
            List<IExercise> exercises = new();
            // Check if we can add favorite exercises
            while (favoriteExercises.Any() && exercises.Count < amountToAdd)
            {
                var favoriteToAdd = favoriteExercises[0];
                favoriteExercises.RemoveAt(0);

                // Check if the favoriteToAdd has a unique Muscle.Name
                if (!exercises.Any(ce =>
                    ce.Muscles.Any(ceMuscle =>
                        favoriteToAdd.Muscles.Any(favoriteMuscle =>
                            favoriteMuscle.Name == ceMuscle.Name && favoriteMuscle.IsPrimary == ceMuscle.IsPrimary))))
                {
                    exercises.Add(favoriteToAdd);
                }
            }

            return exercises;
        }

        private static List<IExercise> AddNonFavoriteExercises(List<IExercise> nonFavoriteExercises, List<IExercise> currentListOfExercises, int compoundsNeeded)
        {
            List<IExercise> exercises = new();
            exercises.AddRange(currentListOfExercises);
            // Check if we need to add non-favorite exercises
            while (exercises.Count < compoundsNeeded && nonFavoriteExercises.Any())
            {
                var nonFavoriteToAdd = nonFavoriteExercises[0];
                nonFavoriteExercises.RemoveAt(0);

                // Check if the nonFavoriteToAdd has a unique combination of Muscle.Name and IsPrimary
                if (!exercises.Any() || exercises.Any(ce =>
                    ce.Muscles.Any(ceMuscle =>
                        nonFavoriteToAdd.Muscles.Any(nonFavoriteMuscle =>
                            nonFavoriteMuscle.Name != ceMuscle.Name && nonFavoriteMuscle.IsPrimary == ceMuscle.IsPrimary && nonFavoriteMuscle.IsPrimary == true))))
                {
                    exercises.Add(nonFavoriteToAdd);
                }

            }
            return exercises;
        }

        private static List<IExercise> AddExercises(List<IExercise> favoriteAndNonFavoriteExercises, int exercisesToAdd)
        {
            List<IExercise> exercises = new();
            // Check if we need to add non-favorite exercises
            while (exercises.Count < exercisesToAdd && favoriteAndNonFavoriteExercises.Any())
            {
                var exerciseToAdd = favoriteAndNonFavoriteExercises[0];
                favoriteAndNonFavoriteExercises.RemoveAt(0);

                // Check if the nonFavoriteToAdd has a unique combination of Muscle.Name and IsPrimary
                if (!exercises.Any() || exercises.Any(ce =>
                    ce.Muscles.Any(ceMuscle =>
                        exerciseToAdd.Muscles.Any(nonFavoriteMuscle =>
                            nonFavoriteMuscle.Name != ceMuscle.Name && nonFavoriteMuscle.IsPrimary == ceMuscle.IsPrimary && nonFavoriteMuscle.IsPrimary == true))))
                {
                    exercises.Add(exerciseToAdd);
                }

            }
            return exercises;
        }

        private static List<IExercise> AddNonStartingCompoundExercises(List<IExercise> exercises, List<IExercise> currentListOfExercises, bool prioFavorites, int amountToAdd, bool isSamePrimary = false, bool isLegsExercises = false, int muscleId = 0)
        {
            exercises = exercises
                .OrderBy(exercise => Guid.NewGuid())
                .Where(exercise => !exercise.Compound)
                .Where(exercise => exercise.Muscles.Any(m => m.IsPrimary))
                .ToList();

            if (isLegsExercises)
                exercises = exercises
                    .Where(exercise => exercise.Muscles.Any(m => m.Id == muscleId && m.IsPrimary))
                    .ToList();

            if (isSamePrimary)
                exercises = exercises
                    .Where(exercise => !currentListOfExercises.Any(ce => exercise.Muscles.Any(exerciseMuscle => // same type where primary must not exists already
                    ce.Muscles.Any(ceMuscle => exerciseMuscle.IsPrimary && ceMuscle.IsPrimary && exerciseMuscle.Name == ceMuscle.Name))))
                    .ToList();

            if (prioFavorites)
                exercises = exercises
                    .OrderByDescending(exercise => exercise.IsFavorite)
                    .ToList();

            return exercises.Take(amountToAdd).ToList();
        }

        private static List<IExercise> FilterCompoundExercises(List<IExercise> exercises, int includeMusclegroupId)
        {
            var random = new Random();
            return exercises
                .Where(exercise => exercise.Compound)
                .Where(exercise => exercise.Muscles.Any(muscle => muscle.MusclegroupId == includeMusclegroupId && muscle.IsPrimary))
                .ToList();
        }

        private static List<IExercise> FilterCompoundExercises(List<IExercise> exercises, int includeMusclegroupId, int excludeMuscleId)
        {
            var random = new Random();
            return exercises
                .Where(exercise => exercise.Compound)
                .Where(exercise => exercise.Muscles.Any(muscle => muscle.MusclegroupId == includeMusclegroupId && muscle.IsPrimary && muscle.Id != excludeMuscleId))
                .ToList();
        }

        private static List<IExercise> FilterNonCompoundExercises(List<IExercise> exercises, int includeMusclegroupId)
        {
            var random = new Random();
            return exercises
                .Where(exercise => !exercise.Compound)
                .Where(exercise => exercise.Muscles.Any(muscle => muscle.MusclegroupId == includeMusclegroupId && muscle.IsPrimary))
                .OrderBy(x => random.Next()) // Shuffle the exercises
                .ToList();
        }

        private static List<IExercise> FilterNonCompoundExercises(List<IExercise> exercises, int includeMusclegroupId, int excludeMusclegroupId)
        {
            var random = new Random();
            return exercises
                .Where(exercise => !exercise.Compound)
                .Where(exercise => exercise.Muscles.Any(muscle => muscle.MusclegroupId == includeMusclegroupId && muscle.IsPrimary && muscle.Id != excludeMusclegroupId))
                .OrderBy(x => random.Next()) // Shuffle the exercises
                .ToList();
        }

        private static List<IExercise> FilterFavoriteCompoundExercises(List<IExercise> compoundExercises)
        {
            var random = new Random();
            return compoundExercises
                .Where(exercise => exercise.IsFavorite)
                .OrderBy(x => random.Next()) // Shuffle the exercises
                .ToList();
        }

        private static List<IExercise> FilterNonFavoriteCompoundExercises(List<IExercise> compoundExercises)
        {
            var random = new Random();
            return compoundExercises
                .Where(exercise => !exercise.IsFavorite)
                .OrderBy(x => random.Next()) // Shuffle the exercises
                .ToList();
        }
    }
}
