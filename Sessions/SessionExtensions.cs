using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OnlineStore.Sessions
{
    public static class SessionExtensions
    {
        //private static IFormatter _serializer = new BinaryFormatter();

        public static T GetOrAdd<T>(this ISession session, string key, Func<T> creator) where T : class
        {
            var data = session.Get<T>(key);
            if (data == null)
            {
                data = creator();
                Set(session, key, data);
            }
            return data;
        }

        public static void Set<T>(this ISession session, string key, T value)
        {
            JsonSerializerOptions options = new()
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            session.SetString(key, JsonSerializer.Serialize<T>(value, options));
            if(session.Keys.Count() == 5)
            {
                //session.Remove(session, Get<T>(session, session.))
            }
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            JsonSerializerOptions options = new()
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };
            return value == null ? default(T) : JsonSerializer.Deserialize<T>(value, options);
        }
    }
}
