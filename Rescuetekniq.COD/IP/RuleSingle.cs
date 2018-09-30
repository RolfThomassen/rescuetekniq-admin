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
        
        public class RuleSingle : Rule
        {
            
            // Property variable:
            private IPMatching.IPAddress ipValue;
            
            // Constructor:
            public RuleSingle(IPMatching.IPAddress Ip)
            {
                ipValue = Ip;
            }
            
            // Properties:
            public override EnumRuleType RuleType
            {
                get
                {
                    return (EnumRuleType.RuleSingle);
                }
            }
            
            public IPMatching.IPAddress Value
            {
                get
                {
                    return (ipValue);
                }
            }
            
            // Functions:
            public override bool IsMatch(IPMatching.IPAddress Ip)
            {
                
                // Description:
                // Compare provided IP-address with current (Me).
                
                if (!(Ip.A == this.Value.A))
                {
                    return false;
                }
                if (!(Ip.B == this.Value.B))
                {
                    return false;
                }
                if (!(Ip.C == this.Value.C))
                {
                    return false;
                }
                if (!(Ip.D == this.Value.D))
                {
                    return false;
                }
                
                // The provided IP-address matches with current (Me).
                return true;
                
            }
            
        }
        
    } // IPMatching
    
    
}
