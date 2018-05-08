using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class ReflectionHelper
    {
        private static Tuple<object, PropertyInfo> GetProperty(object source, string propertyName)
        {
            PropertyInfo foundProperty = null;
            var propertyPath = propertyName.Split('.');
            if (propertyPath.Length > 0)
            {
                try
                {
                    foundProperty = source.GetType().GetProperties()
                        .FirstOrDefault(
                            x => x.Name.Equals(propertyPath.FirstOrDefault(), StringComparison.OrdinalIgnoreCase));
                }
                catch (ArgumentNullException)
                {
                    return null;
                }
            }

            if (foundProperty == null)
                return new Tuple<object, PropertyInfo>(source, null);

            propertyName = propertyName.Substring(propertyPath.FirstOrDefault().Length);
            if (!propertyName.Contains('.'))
                return new Tuple<object, PropertyInfo>(source, foundProperty);

            var propertyValue = foundProperty.GetValue(source, null);
            return GetProperty(propertyValue, propertyName.Substring(1));
        }

        /// <summary>
        ///     Gets value from a property name and object.
        ///     When nested property, use full name eg: Customer.Email
        /// </summary>
        /// <param name="source"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static object GetPropertyValue(object source, string propertyName)
        {
            var foundProperty = GetProperty(source, propertyName);
            return foundProperty.Item2.GetValue(foundProperty.Item1, null);
        }

        /// <summary>
        /// Sets an object's property value by using reflection 
        /// </summary>
        /// <param name="source"> source object</param>
        /// <param name="propertyName">name of the property to check</param>
        /// <param name="value"></param>
        /// <returns>true if property's value updated otherwise false</returns>
        public static bool SetPropertyValue(object source, string propertyName, object value)
        {
            var foundProperty = GetProperty(source, propertyName);
            if (foundProperty == null)
                return false;

            foundProperty.Item2.SetValue(foundProperty.Item1, value, null);
            return true;
        }

        /// <summary>
        ///     Creates a property selector expression based on a type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static Expression<Func<T, dynamic>> CreateExpression<T>(object propertyName)
        {
            var param = Expression.Parameter(typeof (T), "x");
            var body = propertyName.ToString()
                .Split('.')
                .Aggregate<string, Expression>(param, Expression.PropertyOrField);
            return Expression.Lambda<Func<T, dynamic>>(body, param);
        }
    }
}

