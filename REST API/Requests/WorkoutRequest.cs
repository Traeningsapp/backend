namespace REST_API.Requests
{
    public class WorkoutRequest
    {
        public WorkoutRequest(string workoutAsJson)
        {
            _workoutAsJson = workoutAsJson;
        }

        private string _workoutAsJson;

        public string WorkoutAsJson
        {
            get => _workoutAsJson; set => _workoutAsJson = value;
        }
    }
}
