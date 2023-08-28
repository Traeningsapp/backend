namespace REST_API.Requests
{
    public class WorkoutRequest
    {
        public WorkoutRequest(string workoutAsJson, string exerciseStatsAsJson)
        {
            _workoutAsJson = workoutAsJson;
            _exerciseStatsAsJson = exerciseStatsAsJson;
        }

        private string _workoutAsJson;
        private string _exerciseStatsAsJson;

        public string WorkoutAsJson
        {
            get => _workoutAsJson; set => _workoutAsJson = value;
        }
        public string ExerciseStatsAsJson
        {
            get => _exerciseStatsAsJson; set => _exerciseStatsAsJson = value;
        }
    }
}
