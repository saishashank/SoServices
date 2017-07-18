using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoServices.Hepler
{
    public static class ExtensionMethods
    {
        public static T TrimObject<T>(this T obj)
        {
            var stringProperties = obj.GetType().GetProperties()
                                      .Where(p => p.PropertyType == typeof(string));

            foreach (var stringProperty in stringProperties)
            {
                string currentValue = (string)stringProperty.GetValue(obj, null);
                stringProperty.SetValue(obj, currentValue.Trim(), null);
            }

            return obj;
        }
    }
}