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
    namespace IPMatching
    {
        
        public class IPAddress
        {
            
            public byte A = (byte) 0;
            public byte B = (byte) 0;
            public byte C = (byte) 0;
            public byte D = (byte) 0;
            
            public IPAddress(byte IP1, byte IP2, byte IP3, byte IP4)
            {
                if (IPAddress.IsValid(IP1, IP2, IP3, IP4))
                {
                    this.A = IP1;
                    this.B = IP2;
                    this.C = IP3;
                    this.D = IP4;
                }
                else
                {
                    // IP-address was not valid, throw exception.
                    throw (new System.Exception("Invalid IP-address (IPMatching.IPAddress!New)"));
                }
            }
            
            public IPAddress(string Ip)
            {
                
                // Description:
                // Create new IPAddress object based on a String.
                
                if (IPAddress.IsValid(Ip) == true)
                {
                    string[] splitIp = Ip.Split(".".ToCharArray());
                    
                    this.A = byte.Parse(splitIp[0]);
                    this.B = byte.Parse(splitIp[1]);
                    this.C = byte.Parse(splitIp[2]);
                    this.D = byte.Parse(splitIp[3]);
                }
                else
                {
                    // IP-address was not valid, throw exception.
                    throw (new System.Exception("Invalid IP-address (IPMatching.IPAddress!New)"));
                }
                
            }
            
            public IPAddress()
            {
                // Default constructor, do nothing.
            }
            
            public static bool IsValid(byte IP1, byte IP2, byte IP3, byte IP4)
            {
                return IsValid(IP1.ToString() +"." + IP2.ToString() +"." + IP3.ToString() +"." + IP4.ToString());
            }
            public static bool IsValid(string Ip)
            {
                
                // Description:
                // A function that uses the System.Net.IPAddress.Parse function to
                // validate an IP-address.
                // Supresses exceptions and returns a Boolean instead.
                
                System.Net.IPAddress testIp;
                
                try
                {
                    testIp = System.Net.IPAddress.Parse(Ip);
                }
                catch
                {
                    // Invalid IP-address.
                    return false;
                }
                
                // No exception accured, IP-address is valid.
                return true;
                
            }
            
            public override string ToString()
            {
                
                // Description:
                // Overrides ToString method of baseclass (Object) to return
                // current IP-address instead of class name.
                
                return (this.A +"." + System.Convert.ToString(this.B) +"." + System.Convert.ToString(this.C) +"." + System.Convert.ToString(this.D));
                
            }
            
        }
        
    } // IPMatching
    
    
}
