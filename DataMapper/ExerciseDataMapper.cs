using DataMapper.Converter;
using Domain.Exercise;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
