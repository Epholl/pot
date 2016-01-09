using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SnakServer
{
    /// <summary>
    /// A special utility class for a very simple dependency injection available throughout the application.
    /// If an instance of a class is required, it is either already present or is created and always returned to the caller.
    /// Requesting an object needs it's type to be sent and an explicit cast to this type after retrieval.
    /// </summary>
    public class SL
    {
        private static Dictionary<String, Object> objects;

        /// <summary>
        /// Add an object to the list of available objects.
        /// </summary>
        /// <param name="registeredObject">The object to be added to the available objects table.</param>
        public static void Register(Object registeredObject)
        {
            CheckInit();

            string key = registeredObject.GetType().ToString();
            objects[key] = registeredObject;
        }

        /// <summary>
        /// The method to get an instance of a specific type. If none exists, reflection is used to create one using the default constructor.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Object Get(Type type)
        {
            CheckInit();

            string key = type.ToString();

            
            if (objects.ContainsKey(key))
            {
                // An object is present - return it
                return objects[key];
            }
            else
            {
                //A new instance is created and returned.
                Object instance = Activator.CreateInstance(type);
                Register(instance);
                return instance;
            }
        }

        private static void CheckInit()
        {
            if (objects == null)
            {
                objects = new Dictionary<string, object>();
            }
        }
    }
}
