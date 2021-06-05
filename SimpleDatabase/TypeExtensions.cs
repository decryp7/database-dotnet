﻿using System;
using GuardLibrary;

namespace SimpleDatabase
{
    public static class TypeExtensions
    {
        public static string GetFriendlyName(this Type type)
        {
            Guard.Ensure(type, nameof(type)).IsNotNull();

            string friendlyName = type.Name;
            if (!type.IsGenericType)
            {
                return friendlyName;
            }

            int iBacktick = friendlyName.IndexOf('`');
            if (iBacktick > 0)
            {
                friendlyName = friendlyName.Remove(iBacktick);
            }

            friendlyName += "<";
            Type[] typeParameters = type.GetGenericArguments();
            for (int i = 0; i < typeParameters.Length; ++i)
            {
                string typeParamName = GetFriendlyName(typeParameters[i]);
                friendlyName += (i == 0 ? typeParamName : "," + typeParamName);
            }
            friendlyName += ">";

            return friendlyName;
        }
    }
}