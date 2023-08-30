using Application.Ports.Outgoing;
using DataMapper.Converter;
using Domain.Exercise;
using Newtonsoft.Json;


namespace DataMapper
{
    public class ExerciseDataMapper : IDataMapper<IExercise>
    {
        public IExercise? FromJson(string json)
        {
            var serializerSettings = new JsonSerializerSettings
            {
                Converters = { new ExerciseJsonConverter() }
            };

            return JsonConvert.DeserializeObject<IExercise>(json, serializerSettings);
        }
    }
}
