// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Linq;
using System.Configuration;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Xml.Linq;
using System.Collections;
using System.Data;
// End of VB project level imports

using System.ComponentModel;
using RescueTekniq.CODE;


namespace RescueTekniq.CODE
{
    public sealed class EnumFunc
    {
        
        public static SortedList<int, string> GetEnumDataSource<T>()
        {
            Type myEnumType = typeof(T);
            SortedList<int, string> returnCollection = new SortedList<int, string>();
            try
            {
                if (ReferenceEquals(myEnumType.BaseType, typeof(Enum)))
                {
                    string[] enumNames = Enum.GetNames(myEnumType);
                    for (int i = 0; i <= (enumNames.Length - 1); i++)
                    {
                        returnCollection.Add(Convert.ToInt32(Enum.Parse(myEnumType, enumNames[i])), enumNames[i]);
                    }
                }
            }
            catch (Exception)
            {
                return default(SortedList<int, string>);
            }
            return returnCollection;
        }
        
        /// <summary>
        /// Get the Description of an enum value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>If there is no description attribute, the name will be used.</remarks>
        public static string GetDescription(System.Enum value)
        {
            return (string) (GetDescription(value.GetType().GetField(value.ToString())));
        }
        
        /// <summary>
        /// Get the description of a field
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        /// <remarks>If the field doesn't have a description attribute, the name is used</remarks>
        public static object GetDescription(System.Reflection.FieldInfo field)
        {
            // Get the array of description attributes applied (there will be 0 or 1)
            object[] descriptions = null;
            descriptions = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            
            // If there was a description, return it
            if (descriptions.Length > 0)
            {
                return ((DescriptionAttribute) (descriptions[0])).Description;
            }
            
            // Otherwise return the field's name
            return field.Name;
        }
        
    }
    
}
