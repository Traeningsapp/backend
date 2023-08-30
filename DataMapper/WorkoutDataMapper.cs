using Application.Ports.Outgoing;
using DataMapper.Converter;
using Domain.Workout;
using Newtonsoft.Json;

namespace DataMapper
{
    public class WorkoutDataMapper : IDataMapper<IWorkout>
    {
        public IWorkout? FromJson(string json)
        {
            var serializerSettings = new JsonSerializerSettings
            {
                Converters =
                {
                    new ExerciseJsonConverter(),
                    new ExerciseStatsJsonConverter()
                }
            };

            return JsonConvert.DeserializeObject<Workout>(json, serializerSettings);
        }
    }
}
