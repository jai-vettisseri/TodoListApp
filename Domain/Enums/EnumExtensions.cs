using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Timers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Domain.Enums
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum value)
        {
            var member = value.GetType().GetMember(value.ToString()).FirstOrDefault();
            var displayAttribute = member?.GetCustomAttribute<DisplayAttribute>();
            return displayAttribute?.Name ?? value.ToString();
        }


        public static T? GetEnumFromDisplayName<T>(string displayName)
            where T : struct, Enum =>
             typeof(T)
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .FirstOrDefault(f =>
                    f.GetCustomAttribute<DisplayAttribute>()?.Name == displayName)
                is FieldInfo match
                ? (T)match.GetValue(null)
                : null;
    }
}
