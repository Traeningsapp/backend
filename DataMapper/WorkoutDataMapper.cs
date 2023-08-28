using DataMapper.Converter;
using Domain.Workout;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
