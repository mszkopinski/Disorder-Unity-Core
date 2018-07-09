using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace UnityCore
{
    
    /// <summary>
    /// Need to differentiate that Activities from reflection are only needed when you want to use editor window. 
    /// When you click save on editor window you just need to compare some class and rewrite data.
    /// </summary>
    public static class ModifiersProvider
    {
        public static ISerializable ModifiersData;
        public static List<Type> ActivitiesFromAssembly;

        static Assembly _operatingAssembly;

        public static void Initialize(Assembly assembly)
        {
            _operatingAssembly = assembly;
            GetActivitiesFromAssembly();
        }

        static void GetActivitiesFromAssembly()
        {
            ActivitiesFromAssembly = new List<Type>();

            var types = _operatingAssembly.GetTypes();
            ActivitiesFromAssembly = types
                .Where(t => t.IsClass && typeof(Activity).IsAssignableFrom(t) && !t.IsAbstract).ToList();
        }

        public static List<Modifier> GetModifiersFromActivity(Activity activity)
        {
            var modifiers = new List<Modifier>();

            if (ActivitiesFromAssembly.Count != 0)
            {

            }

            return modifiers;
        }

        public static bool CheckIfActivityExists<T>(T activity)
        {
            return ActivitiesFromAssembly.Any(a => a.ToString() == activity.GetType().FullName);
        }
    }
}