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

using System.Runtime.CompilerServices;
using System.Text;
using System.Globalization;
using RescueTekniq.CODE;


namespace RescueTekniq.CODE
{
    public static class StringExtensions
    {
        
        /// <summary>
        /// String Extender to print tekst om console output
        /// </summary>
        /// <param name="aString">String to be printet</param>
        /// <remarks></remarks>
        
public static void Print(this string aString)
        {
            Console.WriteLine(aString);
        }
        
        /// <summary>
        /// String Extender to validate of the contents of
        /// a string is an actual guid.
        /// </summary>
        /// <param name="value">String representation of a guid</param>
        /// <returns>True if the string can be converted to a guid, false if not.</returns>
        /// <remarks></remarks>
        
public static bool IsGuid(this string value)
        {
            // Do not allow empty guids and skip empty strings
            if (string.IsNullOrEmpty(value) || value == Guid.Empty.ToString())
            {
                return false;
            }
            
            // try the convertion
            try
            {
                Guid id = new Guid(value);
            }
            catch
            {
                return false;
            }
            // We made it here we have a valid guid
            return true;
        }
        
    }
    
    
    public sealed class Funktioner
    {
        
        /// <summary>
        /// Extention method to convert a string value to an instance of a guid.
        /// </summary>
        /// <param name="value">The string value of the guid</param>
        /// <returns>A new guid or and empty guid.</returns>
        /// <remarks></remarks>
        public static System.Guid ToGuid(string value)
        {
            
            // Empty or null string return nothing (null)
            if (string.IsNullOrEmpty(value))
            {
                return  default(System.Guid);
            }
            else
            {
                if (value.IsGuid())
                {
                    return new Guid(value);
                }
                else
                {
                    return  default(System.Guid);
                }
            }
        }
        
        //Function ToGuid(ByVal Value As Object) As System.Guid
        //    Dim res As Guid = Guid.Empty
        //    If Not (String.IsNullOrEmpty(Value)) Then
        //        res = Value
        //    End If
        //    Return res
        //End Function
        
        public static bool TestBoolean(object sValue)
        {
            bool bResult = false;
            int iNum = 0;
            string str = "";
            bResult = false;
            switch (Information.VarType(sValue))
            {
                case VariantType.Null: //vbNull
                    bResult = false; //NULL
                    break;
                case VariantType.Empty: // vbEmpty
                    bResult = false; //NULL
                    break;
                case VariantType.Boolean: // vbBoolean
                    bResult = System.Convert.ToBoolean(sValue);
                    break;
                default:
                    str = System.Convert.ToString(Convert.ToString(sValue + "").Trim());
                    iNum = System.Convert.ToInt32(ToDouble(str));
                    switch (str.ToLower())
                    {
                        case "1":
                        case "+":
                        case "on":
                        case "til":
                        case "yes":
                        case "ja":
                        case "true":
                        case "sand":
                        case "-1":
                            iNum = 1;
                            break;
                        case "0":
                        case "-":
                        case "off":
                        case "fra":
                        case "no":
                        case "nej":
                        case "false":
                        case "falsk":
                        case "":
                            iNum = 0;
                            break;
                        default:
                            break;
                            //	iNum = 0
                    }
                    if (Information.IsNumeric(iNum))
                    {
                        if (iNum != 0)
                        {
                            bResult = true;
                        }
                        else
                        {
                            bResult = false;
                        }
                    }
                    break;
            }
            //TestBoolean = bResult
            return bResult;
        }
        
        public static string FormatNumWeb(object value)
        {
            string res = "";
            res = System.Convert.ToString(ToDecimal(value));
            if (res == "0")
            {
                res = "";
            }
            return res;
        }
        
        public static bool ToBool(object Value)
        {
            return ToBoolean(Value);
        }
        public static bool ToBoolean(object Value)
        {
            bool res = false;
            if (!(ReferenceEquals(Value, DBNull.Value)))
            {
                if (TestBoolean(Value) == true)
                {
                    res = true;
                }
            }
            return res;
        }
        
        
        public static int ToInt(object Value, int def = 0)
        {
            return ToInteger(Value, def);
        }
        public static int ToInteger(object Value, int def = 0)
        {
            int res = def;
            if (!(ReferenceEquals(Value, DBNull.Value)))
            {
                if (Information.IsNumeric(Value))
                {
                    res = System.Convert.ToInt32(Value);
                }
            }
            return res;
        }
        
        public static long ToLong(object Value, long def = 0)
        {
            long res = def;
            if (!(ReferenceEquals(Value, DBNull.Value)))
            {
                if (Information.IsNumeric(Value))
                {
                    res = System.Convert.ToInt64(Value);
                }
            }
            return res;
        }
        
        public static decimal ToDec(object Value, decimal def = 0)
        {
            return ToDecimal(Value, def);
        }
        public static decimal ToDecimal(object Value, decimal def = 0)
        {
            decimal res = def;
            if (!(ReferenceEquals(Value, DBNull.Value)))
            {
                if (Information.IsNumeric(Value))
                {
                    decimal.TryParse(System.Convert.ToString(Value), out res);
                }
            }
            return res;
        }
        
        public static decimal ToFloat(object Value, decimal def = 0)
        {
            decimal res = def;
            if (!(ReferenceEquals(Value, DBNull.Value)))
            {
                if (Information.IsNumeric(Value))
                {
                    res = System.Convert.ToDecimal(Value);
                }
            }
            return res;
        }
        
        public static DateTime ToDate(object Value)
        {
            DateTime res = default(DateTime); //= DateSerial(1, 1, 1754) 'Date.MinValue
            if (!(ReferenceEquals(Value, DBNull.Value)))
            {
                if (Information.IsDate(Value))
                {
                    res = System.Convert.ToDateTime(Value);
                }
            }
            return res;
        }
        public static DateTime ToDateDef(object Value, DateTime def = default(DateTime))
        {
            // VBConversions Note: def assigned to default value below, since optional parameter values must be static and C# doesn't support date literals.
            if (def == default(DateTime))
                def = new DateTime(2018, 8, 6);
            
            DateTime res = def; //= DateSerial(1, 1, 1754) 'Date.MinValue
            if (!(ReferenceEquals(Value, DBNull.Value)))
            {
                if (Information.IsDate(Value))
                {
                    res = System.Convert.ToDateTime(Value);
                }
            }
            return res;
        }
        
        public static string ToSqlDateTime(object Value)
        {
            string res = "";
            DateTime dtg = default(DateTime);
            dtg = ToDateDef(Value);
            if (dtg == new DateTime(2018, 8, 6))
            {
                res = "";
            }
            else if (dtg != new DateTime(1754, 1, 1))
            {
                res = dtg.ToString("yyyy-MM-dd");
                if (DateAndTime.DateDiff(DateInterval.Second, dtg, DateAndTime.DateSerial(DateAndTime.Year(dtg), DateAndTime.Month(dtg), DateAndTime.Day(dtg))) != 0)
                {
                    res += " ";
                    res = dtg.ToString("T");
                }
            }
            return res.Trim();
        }
        
        public static double ToDouble(object Value, double def = 0)
        {
            double res = def;
            if (!(ReferenceEquals(Value, DBNull.Value)))
            {
                if (Information.IsNumeric(Value))
                {
                    res = System.Convert.ToDouble(Value);
                }
            }
            return res;
        }
        
        public static SByte ToSByte(object Value)
        {
            SByte res = 0;
            if (!(ReferenceEquals(Value, DBNull.Value)))
            {
                if (Information.IsNumeric(Value))
                {
                    res = (sbyte) Value;
                }
            }
            return res;
        }
        
        public static short ToShort(object Value)
        {
            short res = (short) 0;
            if (!(ReferenceEquals(Value, DBNull.Value)))
            {
                if (Information.IsNumeric(Value))
                {
                    res = System.Convert.ToInt16(Value);
                }
            }
            return res;
        }
        
        public static float ToSingle(object Value)
        {
            float res = 0;
            if (!(ReferenceEquals(Value, DBNull.Value)))
            {
                if (Information.IsNumeric(Value))
                {
                    res = System.Convert.ToSingle(Value);
                }
            }
            return res;
        }
        
        public static uint ToUInteger(object Value)
        {
            uint res = (uint) 0;
            if (!(ReferenceEquals(Value, DBNull.Value)))
            {
                if (Information.IsNumeric(Value))
                {
                    res = System.Convert.ToUInt32(Value);
                }
            }
            return res;
        }
        
        public static ulong ToULong(object Value)
        {
            ulong res = 0;
            if (!(ReferenceEquals(Value, DBNull.Value)))
            {
                if (Information.IsNumeric(Value))
                {
                    res = (ulong) Value;
                }
            }
            return res;
        }
        
        public static ushort ToUShort(object Value)
        {
            ushort res = System.Convert.ToUInt16(0);
            if (!(ReferenceEquals(Value, DBNull.Value)))
            {
                if (Information.IsNumeric(Value))
                {
                    res = System.Convert.ToUInt16(Value);
                }
            }
            return res;
        }
        
        public static string ToDbString(object Value, string def = "")
        {
            string res = def;
            if (!(ReferenceEquals(Value, DBNull.Value) || ReferenceEquals(Value, null)))
            {
                res = Value.ToString();
            }
            return res;
        }
        
        public static string RemoveDiacritics(string stIn)
        {
            string stFormD = stIn.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();
            
            for (int iCH = 0; iCH <= stFormD.Length - 1; iCH++)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(stFormD[iCH]);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(stFormD[iCH]);
                }
            }
            return sb.ToString().Normalize(NormalizationForm.FormC);
        }
        
        public static string MakeAlias(string strAlias)
        {
            string res = "";
            res = RemoveDiacritics(strAlias);
            res = res.Replace(" ", "_");
            
            return res;
        }
        
    }
    
    
    
}
