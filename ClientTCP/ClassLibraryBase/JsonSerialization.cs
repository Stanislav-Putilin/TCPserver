using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClassLibraryBase
{
    /// <summary>
    /// Class for serializing and deserializing objects in JSON format.
    /// </summary>
    public class JsonSerialization
    {
        /// <summary>
        /// Saves an object to a file in JSON format.
        /// </summary>
        /// <typeparam name="T">Type of the object to save.</typeparam>
        /// <param name="file">The path to the file where the object will be saved.</param>
        /// <param name="obj">The object to be serialized.</param>
        public static void Save<T>(string file, T obj)
        {
            DataContractJsonSerializer formatter = new DataContractJsonSerializer(typeof(T));
            using (var fs = File.Create(file))
            {
                formatter.WriteObject(fs, obj);
            }
            Console.WriteLine("JsonSerializer Serialize is OK");
        }

        /// <summary>
        /// Loads an object from a file in JSON format.
        /// </summary>
        /// <typeparam name="T">Type of the object to load.</typeparam>
        /// <param name="file">The path to the file from which the object will be loaded.</param>
        /// <returns>The loaded object or null if the file is empty.</returns>
        public static T? Load<T>(string file) where T : class
        {
            T? obj;
            DataContractJsonSerializer formatter = new DataContractJsonSerializer(typeof(T));
            using (var fs = File.OpenRead(file))
            {
                if (fs.Length > 0)
                {
                    obj = formatter.ReadObject(fs) as T;
                    return obj;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Converts an object to a JSON string.
        /// </summary>
        /// <typeparam name="T">Type of the object to serialize.</typeparam>
        /// <param name="obj">The object to be serialized.</param>
        /// <returns>A JSON string representing the object.</returns>
        public static string Serialize<T>(T obj)
        {
            return JsonSerializer.Serialize(obj);
        }

        /// <summary>
        /// Converts a JSON string to an object of the specified type.
        /// </summary>
        /// <typeparam name="T">Type of the object to deserialize.</typeparam>
        /// <param name="json">The JSON string to deserialize.</param>
        /// <returns>The deserialized object.</returns>
        public static T Deserialize<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}