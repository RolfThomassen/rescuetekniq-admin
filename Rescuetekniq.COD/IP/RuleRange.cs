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
        
        public class RuleRange : Rule
        {
            
            // Property variables:
            private IPMatching.IPAddress rangeStart;
            private IPMatching.IPAddress rangeStop;
            
            // Constructor:
            public RuleRange(IPMatching.IPAddress Start, IPMatching.IPAddress Stop)
            {
                
                // Restrict use of this rule for single addresses.
                if (Start.ToString() == Stop.ToString())
                {
                    throw (new System.Exception("Start address can't be the same as Stop address"));
                }
                
                // Swap places of Start and Stop if Start is higher than Stop.
                bool swapIp = false;
                
                if (Start.A > Stop.A)
                {
                    swapIp = true;
                }
                else
                {
                    if (Start.B > Stop.B)
                    {
                        swapIp = true;
                    }
                    else
                    {
                        if (Start.C > Stop.C)
                        {
                            swapIp = true;
                        }
                        else
                        {
                            if (Start.D > Stop.D)
                            {
                                swapIp = true;
                            }
                        }
                    }
                }
                
                // Store IP-addresses in local variables.
                if (swapIp == false)
                {
                    rangeStart = Start;
                    rangeStop = Stop;
                }
                else
                {
                    rangeStart = Stop;
                    rangeStop = Start;
                }
                
            }
            
            // Properties:
            public override EnumRuleType RuleType
            {
                get
                {
                    return (EnumRuleType.RuleRange);
                }
            }
            
            public IPMatching.IPAddress Start
            {
                get
                {
                    return (rangeStart);
                }
            }
            
            public IPMatching.IPAddress Stop
            {
                get
                {
                    return (rangeStop);
                }
            }
            
            // Functions:
            public override bool IsMatch(IPMatching.IPAddress Ip)
            {
                
                // Description:
                // Determine if the provided IP-address is within (including start/stop)
                // this range.
                
                if (Ip.A > rangeStart.A && Ip.A < rangeStop.A)
                {
                    // A-domain is in between start/stop range.
                    return true;
                }
                
                if (Ip.A == rangeStart.A || Ip.A == rangeStop.A)
                {
                    
                    if (Ip.B > rangeStart.B && Ip.B < rangeStop.B)
                    {
                        // B-domain is in between start/stop range.
                        return true;
                    }
                    
                    if (Ip.B == rangeStart.B || Ip.B == rangeStop.B)
                    {
                        
                        if (Ip.C > rangeStart.C && Ip.C < rangeStop.C)
                        {
                            // C-domain is in between start/stop range.
                            return true;
                        }
                        
                        if (Ip.C == rangeStart.C || Ip.C == rangeStop.C)
                        {
                            
                            if (rangeStart.A == rangeStop.A && rangeStart.B == rangeStop.B && rangeStart.C == rangeStop.C)
                            {
                                
                                // A, B, C-domains are the same for start and stop.
                                if (Ip.D > rangeStart.D - 1 && Ip.D < rangeStop.D + 1)
                                {
                                    return true;
                                }
                                
                            }
                            else
                            {
                                
                                // Different C-domains in start/stop.
                                if (Ip.C == rangeStart.C)
                                {
                                    if (Ip.D >= rangeStart.D)
                                    {
                                        return true;
                                    }
                                }
                                else if (Ip.C == rangeStop.C)
                                {
                                    if (Ip.D <= rangeStop.D)
                                    {
                                        return true;
                                    }
                                }
                                
                            }
                            
                        }
                        
                    }
                    
                }
                
                // No match.
                return false;
                
            }
            
        }
        
    } // IPMatching
    
    
}
