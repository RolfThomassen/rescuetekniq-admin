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

using System.IO;
using System.Security.Permissions;
//using System.Runtime.InteropServices.Marshal;
using System.Runtime.InteropServices;
using RescueTekniq.CODE;


namespace RescueTekniq.CODE
{
    public sealed class FileIO
    {
        
        public static string ReadTextFile(string filename)
        {
            string res = "";
            
            if (!System.IO.File.Exists(filename))
            {
                return "";
            }
            
            StreamReader FileStreamReader = default(StreamReader);
            try
            {
                FileStreamReader = File.OpenText(filename); //Server.MapPath(".\Upload\") & "test.txt"
                res = FileStreamReader.ReadToEnd();
                FileStreamReader.Close();
            }
            catch (FileNotFoundException ex)
            {
                throw (new FileNotFoundException("Filen findes ikke : ", ex));
            }
            catch (FileLoadException ex)
            {
                throw (new FileLoadException("Filen kunne ikke læses : ", ex));
            }
            return res;
        }
        
        [DllImport("urlmon.dll", ExactSpelling=true, CharSet=CharSet.Ansi, SetLastError=true)]
        private static extern int FindMimeFromData(IntPtr pBC, [MarshalAs(UnmanagedType.LPWStr)]string pwzUrl, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeParamIndex = 3)]byte[] pBuffer, int cbSize, [MarshalAs(UnmanagedType.LPWStr)]string pwzMimeProposed, int dwMimeFlags, [MarshalAs(UnmanagedType.LPWStr)]ref string ppwzMimeOut, int dwReserved);
        
        public static string getMimeFromFileOld(string file)
        {
            string mimeout = "";
            
            FileStream fs = default(FileStream);
            byte[] buf = null;
            string result;
            if (!System.IO.File.Exists(file))
            {
                throw (new FileNotFoundException(file + " not found"));
            }
            
            int MaxContent = 0;
            MaxContent = System.Convert.ToInt32(new FileInfo(file).Length);
            if (MaxContent > 4096)
            {
                MaxContent = 4096;
            }
            
            fs = new FileStream(file, FileMode.Open);
            
            buf = new byte[MaxContent + 1];
            fs.Read(buf, 0, MaxContent);
            fs.Close();
            
            result = System.Convert.ToString(FindMimeFromData(IntPtr.Zero, file, buf, MaxContent, null, 0, ref mimeout, 0));
            
            return mimeout;
        }
        
        public static string getMimeFromFile(string file)
        {
            //Dim mimeout As IntPtr
            string mimeout = "";
            
            if (!System.IO.File.Exists(file))
            {
                throw (new FileNotFoundException(file + " not found"));
            }
            int MaxContent = System.Convert.ToInt32(new FileInfo(file).Length);
            if (MaxContent > 4096)
            {
                MaxContent = 4096;
            }
            
            FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read);
            byte[] buf = new byte[MaxContent + 1];
            fs.Read(buf, 0, MaxContent);
            fs.Close();
            //fs.Dispose()
            int result = System.Convert.ToInt32(FindMimeFromData(IntPtr.Zero, file, buf, MaxContent, null, 0, ref mimeout, 0));
            
            if (result != 0)
            {
                //Throw Marshal.GetHRForExceptionresult)
            }
            
            //Dim mime As String = Marshal.PtrToStringUni(mimeout)
            //Marshal.FreeCoTaskMem(mimeout)
            //Return mime
            return mimeout;
        } //getMimeFromFile
        
        public static void DeleteFile(string filename)
        {
            if (File.Exists(filename))
            {
                try
                {
                    File.Delete(filename);
                }
                catch (IOException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        
    }
    
}
