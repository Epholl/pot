using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SnakServer
{
    public class SL
    {
        private static Dictionary<String, Object> objects;

        public static void Register(Object registeredObject)
        {
            CheckInit();

            string key = registeredObject.GetType().ToString();
            objects[key] = registeredObject;
        }

        public static Object Get(Type type)
        {
            CheckInit();

            string key = type.ToString();

            if (objects.ContainsKey(key))
            {
                return objects[key];
            }
            else
            {
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
