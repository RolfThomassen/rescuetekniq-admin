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
    public sealed class PasswordModule
    {
        
        //########################################################################################
        //####
        //####	Password
        //####
        //########################################################################################
        
        /// <summary>
        /// Make a new legal password
        /// </summary>
        /// <param name="PasswordLength">Optional lenght of password, default = 8 char</param>
        /// <returns>Legal password</returns>
        /// <remarks></remarks>
        public static string generatePassword(int PasswordLength = 8)
        {
            VBMath.Randomize();
            return CreatePassword(PasswordLength);
        }
        
        //NATO phonetic alphabet translator
        //ALPHA BRAVO CHARLIE DELTA ECHO FOXTROT GOLF HOTEL INDIA JULIET KILO LIMA MIKE NOVEMBER OSCAR PAPA QUEBEC ROMEO SIERRA TANGO UNIFORM VICTOR WHISKEY X-RAY YANKEE ZULU
        //[Dot] Zero One Two Three Four Five Six Seven Eight Niner [Dash]
        //alpha bravo charlie delta echo foxtrot golf hotel india juliet kilo lima mike november oscar papa quebec romeo sierra tango uniform victor whiskey x-ray yankee zulu
        //[space] [plus] [exclamation mark] [dot] [dash] [percent]
        //[Udråbstegn][Exclamation mark]
        
        private const string allLegal = "ABCDEFGHIJKLMNOPQRSTUVWXYZ.0123456789-abcdefghijklmnopqrstuvwxyz";
        private const string legal = "ABCDEFGH+JKLMN!PQRSTUVWXYZ.#123456789-abcdefghijk%mnopqrstuvwxyz";
        
        /// <summary>
        /// Create a random password
        /// </summary>
        /// <param name="iLen">length of the key</param>
        /// <returns>Random Password</returns>
        /// <remarks></remarks>
        public static string CreatePassword(int iLen)
        {
            string Code = "";
            int idx = 0;
            int iTry = 0;
            int iCnt = 0;
            VBMath.Randomize(DateAndTime.Timer);
            iCnt = 0;
            do
            {
                Code = "";
                iTry = 0;
                do
                {
                    idx = CreateRandomNumber(0, legal.Length);
                    try
                    {
                        Code += legal[idx].ToString();
                    }
                    catch (IndexOutOfRangeException) //Exceptions: System.IndexOutOfRangeException: index is greater than or equal to the length of this object or less than zero.
                    {
                    }
                    iTry++;
                } while (!(Code.Length == iLen | iTry > iLen * 2));
                
                iCnt++;
            } while (!(ValidatePassword(Code) || iCnt > 200));
            return Code;
        }
        
        /// <summary>
        /// Validate a Password
        /// </summary>
        /// <param name="Password">Password to validate</param>
        /// <param name="containUpCase">Password must contain UpCase letters</param>
        /// <param name="containLowerCase">Password must contain LowerCase letters</param>
        /// <param name="containNumeric">Password must contain Numeric letters</param>
        /// <returns>True if password is valid</returns>
        /// <remarks></remarks>
        public static bool ValidatePassword(string Password, bool containUpCase = true, bool containLowerCase = true, bool containNumeric = true)
        {
            bool bResult = true;
            bool bUCase = false;
            bool bLCase = false;
            bool bNum = false;
            
            foreach (char c in Password)
            {
                if (Information.IsNumeric(c))
                {
                    bNum = true;
                }
                else if (c == Strings.UCase(c))
                {
                    bUCase = true;
                }
                else if (c == Strings.LCase(c))
                {
                    bLCase = true;
                }
            }
            
            if (containUpCase)
            {
                if (bUCase == false)
                {
                    bResult = false;
                }
            }
            if (containLowerCase)
            {
                if (bLCase == false)
                {
                    bResult = false;
                }
            }
            if (containNumeric)
            {
                if (bNum == false)
                {
                    bResult = false;
                }
            }
            
            return bResult;
        }
        
        /// <summary>
        /// Create a random code
        /// </summary>
        /// <param name="lowerbound">Min number</param>
        /// <param name="upperbound">Max Number</param>
        /// <returns>Random number equal or between Min and Max number</returns>
        /// <remarks></remarks>
        public static int CreateRandomNumber(int lowerbound, int upperbound)
        {
            int result = 0;
            int antal = 0;
            VBMath.Randomize(DateAndTime.Timer);
            antal = 0;
            do
            {
                antal++;
                result = System.Convert.ToInt32(Conversion.Int((upperbound - lowerbound + 1) * VBMath.Rnd() + lowerbound));
            } while (!((result >= lowerbound & result <= upperbound) || antal > 100));
            return result;
        }
        
    }
    
    
    
    
}
