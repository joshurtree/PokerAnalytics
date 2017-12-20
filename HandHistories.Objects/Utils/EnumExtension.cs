﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HandHistories.Objects.Utils
{
    public static class EnumExtension 
    {
        public static string ToString<T>(this T enumerationValue) where T : struct
        {
            Type type = enumerationValue.GetType();
            if (type.IsEnum)
            {
                //Tries to find a DescriptionAttribute for a potential friendly name
                //for the enum
                MemberInfo[] memberInfo = type.GetMember(enumerationValue.ToString());
                if (memberInfo != null && memberInfo.Length > 0)
                {
                    object[] attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                    if (attrs != null && attrs.Length > 0)
                    {
                        //Pull out the description value
                        return ((DescriptionAttribute)attrs[0]).Description;
                    }
                }
            }
            //If we have no description attribute, just return the ToString of the enum
            return enumerationValue.ToString();
        }
    }
}
