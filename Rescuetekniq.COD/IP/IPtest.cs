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

using RescueTekniq.CODE.IPMatching;
using RescueTekniq.CODE;


namespace RescueTekniq.CODE
{
    public sealed class IPtest
    {
        
        public static void IPmain()
        {
            
            RuleSingle myRule1 = new RuleSingle(new IPAddress((byte) 127, (byte) 0, (byte) 0, (byte) 1));
            
            Debug.WriteLine(myRule1.IsMatch(new IPAddress("127.0.0.1"))); // Returns True.
            Debug.WriteLine(myRule1.IsMatch(new IPAddress("192.168.0.1"))); // Returns False.
            
            //Serie med IP-adresser
            RuleRange myRule2 = new RuleRange(new IPAddress("192.168.0.5"), new IPAddress("192.168.255.255"));
            
            Debug.WriteLine(myRule2.IsMatch(new IPAddress("192.168.0.1"))); // Returns False.
            Debug.WriteLine(myRule2.IsMatch(new IPAddress("192.168.40.1"))); // Returns True.
            Debug.WriteLine(myRule2.IsMatch(new IPAddress("192.168.255.255"))); // Returns True.
            
            
            //Kombination av regler:
            RulesCollection myRules3 = new RulesCollection();
            
            // Create rules:
            RuleRange myRuleRange = new RuleRange(new IPAddress("192.168.0.5"), new IPAddress("192.168.255.255"));
            RuleSingle myRuleSingle = new RuleSingle(new IPAddress("127.0.0.1"));
            
            // Add rules to RulesCollection:
            myRules3.Rules.Add(myRuleRange);
            myRules3.Rules.Add(myRuleSingle);
            
            //Debug.WriteLine(myRules3.IsMatch(New IPAddress("192.168.0.1")))	   ' Returns False.
            //Debug.WriteLine(myRules3.IsMatch(New IPAddress("192.168.40.1")))	   ' Returns True.
            //Debug.WriteLine(myRules3.IsMatch(New IPAddress("192.168.255.255")))	' Returns True.
            //Debug.WriteLine(myRules3.IsMatch(New IPAddress("127.0.0.1")))	   ' Returns True.
            //Debug.WriteLine(myRules3.IsMatch(New IPAddress("192.168.0.1")))	   ' Returns False.
            
        }
        
    }
    
    
    
}
