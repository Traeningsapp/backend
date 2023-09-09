using Domain.Exercise;
using Domain.User;
using Newtonsoft.Json;
using System;
using System.Xml;

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

        public void GenerateExercisesForPullSplit(Dictionary<string, List<IExercise>> exerciseDictionary, bool includeAbs, bool priorFavorites)
        {
            throw new NotImplementedException();
        }

        public void GenerateExercisesForLegsSplit(Dictionary<string, List<IExercise>> exerciseDictionary, bool includeAbs, bool priorFavorites)
        {
            throw new NotImplementedException();
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

        private List<IExercise> SelectAbsExercises(List<IExercise> exercises, bool priorFavorites)
        {
            AddAbsExercises(exercises, priorFavorites, 2);

            List<IExercise> absExercises = new();
            absExercises.AddRange(_absExercises ??= new List<IExercise>());

            return absExercises;
        }

        private void AddChestExercises(List<IExercise> exercises, bool prioFavorites, int amountOfCompoundToAdd, int amountOfNonCompoundToAdd)
        {
            _chestExercises = new();

            var compoundChestExercises = FilterCompoundExercises(exercises, 1);
            var nonCompoundChestExercises = FilterNonCompoundExercises(exercises, 1);
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
            // adding non-starting compound exercise
            _chestExercises.AddRange(AddNonStartingCompoundExercises(nonCompoundChestExercises, _chestExercises, prioFavorites, amountOfNonCompoundToAdd));
        }

        private void AddShoulderExercises(List<IExercise> exercises, bool prioFavorites, int amountOfCompoundToAdd, int amountOfNonCompoundToAdd)
        {
            _shoulderExercises = new();

            var compoundShoulderExercises = FilterCompoundExercises(exercises, 2, 12);
            var nonCompoundShoulderExercises = FilterNonCompoundExercises(exercises, 2, 12);
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

            var tricepsExercises = exercises
                .Where(exercise => exercise.Muscles.Any(muscle => muscle.Id == 12 && muscle.IsPrimary))
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

        private static List<IExercise> AddNonStartingCompoundExercises(List<IExercise> exercises, List<IExercise> currentListOfExercises, bool prioFavorites, int amountToAdd)
        {
            exercises = exercises
                .OrderBy(exercise => Guid.NewGuid())
                .Where(exercise => !exercise.Compound)
                .Where(exercise => exercise.Muscles.Any(m => m.IsPrimary))
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

        private static List<IExercise> FilterCompoundExercises(List<IExercise> exercises, int includeMusclegroupId, int excludeMusclegroupId)
        {
            var random = new Random();
            return exercises
                .Where(exercise => exercise.Compound)
                .Where(exercise => exercise.Muscles.Any(muscle => muscle.MusclegroupId == includeMusclegroupId && muscle.IsPrimary && muscle.Id != excludeMusclegroupId))
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
