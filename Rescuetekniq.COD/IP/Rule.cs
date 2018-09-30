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
        
        public enum EnumRuleType
        {
            RuleSingle = 1,
            RuleRange = 2
        }
        
        // Abstract class (sub: RuleSingle, RuleRange)
        public abstract class Rule
        {
            
            public abstract EnumRuleType RuleType {get;}
            
            public abstract bool IsMatch(IPAddress Ip);
            
        }
        
    } // IPMatching
}
