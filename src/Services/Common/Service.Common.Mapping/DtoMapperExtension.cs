using System.Reflection.Metadata.Ecma335;
using System.Text.Json;

namespace Service.Common.Mapping
{
    public static class DtoMapperExtension
    {
        public static T MapTo<T>(this object value) => JsonSerializer.Deserialize<T>(
                JsonSerializer.Serialize(value)
               );
    }
}