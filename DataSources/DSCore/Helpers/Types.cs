using System;
using System.Reflection;

namespace DSCore.Helpers
{
    public static class Types
    {
        public static Type GetItemProperty(Type type)
        {
            PropertyInfo propertyInfo = null;
            var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (var property in properties)
            {
                if (property.GetIndexParameters().Length != 0 && property.PropertyType != typeof(object))
                {
                    propertyInfo = property;
                    if (propertyInfo.Name == "Item")
                        return propertyInfo.PropertyType;
                }
            }
            return propertyInfo?.PropertyType;
		}
    }
}
