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

using RescueTekniq.CODE;


namespace RescueTekniq.CODE
{
    public sealed class DateTimeLib
    {
        
        public static DateTime CalcDate(DateTime dato, string Calc, bool OnlyWeekDay = false)
        {
            string res = dato.ToString();
            foreach (string Part in Calc.Split("+".ToCharArray()))
            {
                switch (Part.ToLower())
                {
                    case "[lbmd]":
                        res = System.Convert.ToString(LobendeMdr(DateTime.Parse(res)));
                        break;
                    case "[eom]":
                    case "[endofmonth]":
                        res = System.Convert.ToString(EndOfMonth(DateTime.Parse(res)));
                        break;
                    case "[md]":
                    case "[måned]":
                    case "[month]":
                    case "[m]":
                    case "[mm]":
                        res = System.Convert.ToString(AddMonths(DateTime.Parse(res)));
                        break;
                    case "[day]":
                    case "[d]":
                    case "[dd]":
                        res = System.Convert.ToString(AddDays(DateTime.Parse(res)));
                        break;
                    case "[year]":
                    case "[y]":
                    case "[yy]":
                    case "[yyyy]":
                        res = System.Convert.ToString(AddYears(DateTime.Parse(res)));
                        break;
                    case "[w]":
                    case "[ww]":
                    case "[week]":
                    case "[uge]":
                        res = System.Convert.ToString(AddWeeks(DateTime.Parse(res)));
                        break;
                    case "[nwkd]":
                    case "[wkd]":
                        res = System.Convert.ToString(FindNextWeekDay(DateTime.Parse(res)));
                        break;
                    case "[pwkd]":
                        res = System.Convert.ToString(FindPrevWeekDay(DateTime.Parse(res)));
                        break;
                    default:
                        if (Information.IsNumeric(Part))
                        {
                            res = System.Convert.ToString(AddDays(DateTime.Parse(res), Convert.ToInt32(Part)));
                        }
                        break;
                }
                if (OnlyWeekDay)
                {
                    res = System.Convert.ToString(TestWeekDay(DateTime.Parse(res)));
                }
            }
            return DateTime.Parse( res);
        }
        
        public enum MoveEnum
        {
            @Prev = -1,
            @Next = 1
        }
        public static DateTime FindPrevWeekDay(DateTime dato)
        {
            return FindWeekDay(dato, MoveEnum.Prev);
        }
        public static DateTime FindNextWeekDay(DateTime dato)
        {
            return FindWeekDay(dato, MoveEnum.Next);
        }
        public static DateTime FindWeekDay(DateTime dato, MoveEnum Move = MoveEnum.Next)
        {
            DateTime res = dato;
            //while (!((int) res.DayOfWeek < System.Convert.ToInt32(DayOfWeek.Monday)| (int) res.DayOfWeek > System.Convert.ToInt32(DayOfWeek.Friday)))
            while (!(Convert.ToInt32(res.DayOfWeek) < Convert.ToInt32(DayOfWeek.Monday) || Convert.ToInt32(res.DayOfWeek) > Convert.ToInt32(DayOfWeek.Friday)))
            {
                res = AddDays(res, Convert.ToInt32(Move));
                
            }
            return res;
        }
        public static DateTime TestWeekDay(DateTime dato, MoveEnum Move = MoveEnum.Next)
        {
            DateTime res = dato;
            while (Convert.ToInt32(res.DayOfWeek) < Convert.ToInt32(DayOfWeek.Monday) || Convert.ToInt32(res.DayOfWeek) > Convert.ToInt32(DayOfWeek.Friday))
            {
                res = AddDays(res, Convert.ToInt32(Move));
            }
            return res;
        }
        
        public static DateTime LobendeMdr(DateTime dato)
        {
            DateTime res = dato;
            res = EndOfMonth(res);
            res = TestWeekDay(res);
            return res;
        }
        
        public static DateTime FirstOfMonth(DateTime dato)
        {
            DateTime res = dato;
            res = DateAndTime.DateSerial(dato.Year, dato.Month, 1);
            return res;
        }
        
        public static DateTime EndOfMonth(DateTime dato)
        {
            DateTime res = dato;
            res = AddMonths(res);
            res = FirstOfMonth(res);
            res = AddDays(res, -1);
            return res;
        }
        
        public static DateTime AddYears(DateTime dato, int years = 1)
        {
            return DateAndTime.DateAdd(DateInterval.Year, years, dato);
        }
        
        public static DateTime AddMonths(DateTime dato, int months = 1)
        {
            return DateAndTime.DateAdd(DateInterval.Month, months, dato);
        }
        
        public static DateTime AddWeeks(DateTime dato, int weeks = 1)
        {
            return DateAndTime.DateAdd(DateInterval.WeekOfYear, weeks, dato);
        }
        
        public static DateTime AddDays(DateTime dato, int days = 1)
        {
            return DateAndTime.DateAdd(DateInterval.Day, days, dato);
        }
        
    }
    
}
