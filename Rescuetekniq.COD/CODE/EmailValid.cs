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
    public sealed class EmailValid
    {
        
        public enum EnumEmailError
        {
            EmailValid = -1, //-1 : Address is valid.
            EmailInvalid = 0, // 0 : Invalid character was found.
            EmailNoAT = 1, // 1 : No @-character found.
            EmailToManyAT = 2, // 2 : Too many @-characters was found.
            EmailNoAccountFound = 3, // 3 : No account name found.
            EmailNoDomainPart = 4, // 4 : There were fewer than two domain parts (@wennerberg.nu)
            EmailDomainTooShort = 5, // 5 : One or more of the domain parts are less then one character.
            EmailTopDomainNotFound = 6 // 6 : The top domain provided does not exist.
        }
        
        // Return Values:
        //-1 : Address is valid.
        // 0 : Invalid character was found.
        // 1 : No @-character found.
        // 2 : Too many @-characters was found.
        // 3 : No account name found.
        // 4 : There were fewer than two domain parts (@wennerberg.nu)
        // 5 : One or more of the domain parts are less then one character.
        // 6 : The top domain provided does not exist.
        public static EnumEmailError ValidateEmail(string vAddress)
        {
            EnumEmailError returnValue = default(EnumEmailError);
            
            string strTopDomains = "";
            string[] strAt = null;
            int intIterator = 0;
            string strChar = "";
            string[] strDomains = null;
            
            // Start by searching for invalid characters (<> a-z, A-Z, 0-9, .], [-], [_])
            for (intIterator = 1; intIterator <= vAddress.Length; intIterator++)
            {
                
                strChar = vAddress.Substring(intIterator - 1, 1);
                
                if (!((Strings.Asc(strChar.ToLower()) > 96 && Strings.Asc(strChar.ToLower()) < 123) 
	|| strChar == "@" 
	|| strChar =="." 
	|| strChar == "-" 
	|| strChar == "_" 
	|| Information.IsNumeric(strChar)))
                {
                    
                    returnValue = (EnumEmailError) 0;
                    return returnValue;
                    
                }
            }
            
            // Check for @-character.
            strAt = vAddress.Split('@');
            
            if ((strAt.Length - 1) < 1)
            {
                returnValue = (EnumEmailError) 1;
                return returnValue;
            }
            else if ((strAt.Length - 1) > 1)
            {
                returnValue = (EnumEmailError) 2;
                return returnValue;
            }
            
            
            // At least 1 characted must exist prior to the @.
            if (strAt[0].Length == 0)
            {
                returnValue = (EnumEmailError) 3;
                return returnValue;
            }
            
            
            // Begin validating domains.
            strDomains = (strAt[1]).Split('.');
            
            
            // Fill variable with all top domains.
            strTopDomains = 
	",biz,com,edu,gov,info,int,mil,name,net,org,aero,asia,cat,coop,jobs,mobi,museum,pro,tel,travel,arpa" + 
	"ac,ad,ae,af,ag,ai,al,am,an,ao,aq,ar,as,at,au,aw,ax,az,ba,bb,bd,be,bf,bg,bh,bi,bj,bm,bn,bo,br,bs," + 
	"bt,bw,by,bz,ca,cc,cd,cf,cg,ch,ci,ck,cl,cm,cn,co,cr,cu,cv,cx,cy,cz,de,dj,dk,dm,do,dz,ec,ee,eg,er," + 
	"es,et,eu,fi,fj,fk,fm,fo,fr,ga,gd,ge,gf,gg,gh,gi,gl,gm,gn,gp,gq,gr,gs,gt,gu,gw,gy,hk,hm,hn,hr,ht," + 
	"hu,id,ie,il,im,in,io,iq,ir,is,it,je,jm,jo,jp,ke,kg,kh,ki,km,kn,kp,kr,kw,ky,kz,la,lb,lc,li,lk,lr," + 
	"ls,lt,lu,lv,ly,ma,mc,md,me,mg,mh,mk,ml,mm,mn,mo,mp,mq,mr,ms,mt,mu,mv,mw,mx,my,mz,na,nc,ne,nf,ng," + 
	"ni,nl,no,np,nr,nu,nz,om,pa,pe,pf,pg,ph,pk,pl,pn,pr,ps,pt,pw,py,qa,re,ro,rs,ru,rw,sa,sb,sc,sd,se," + 
	"sg,sh,si,sk,sl,sm,sn,sr,st,su,sv,sy,sz,tc,td,tf,tg,th,tj,tk,tl,tm,tn,to,tr,tt,tv,tw,tz,ua,ug,uk," + 
	"us,uy,uz,va,vc,ve,vg,vi,vn,vu,wf,ws,ye,za,zm,zw,";
            
            // At least two domain "parts" must exist.
            if ((strDomains.Length - 1) < 1)
            {
                returnValue = (EnumEmailError) 4;
                return returnValue;
            }
            
            
            // Every domain part must be at least 1 character.
            for (intIterator = 0; intIterator <= (strDomains.Length - 1); intIterator++)
            {
                if (strDomains[intIterator].Length == 0)
                {
                    returnValue = (EnumEmailError) 5;
                    return returnValue;
                }
            }
            
            
            // Ensure top domain is not fake.
            if (!(strTopDomains.IndexOf("," + strDomains[strDomains.Length - 1].ToLower() + ",") + 1 > 0))
            {
                returnValue = (EnumEmailError) 6;
                return returnValue;
            }
            
            
            // E-mail address is valid.
            returnValue = (EnumEmailError) (-1);
            
            return returnValue;
        }
        
    }
    
    
}
