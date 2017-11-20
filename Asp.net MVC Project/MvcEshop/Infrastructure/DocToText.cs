using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.InteropServices.ComTypes;
using System.IO;

namespace Infrastructure
{
    public class DocToText
    {
  

       public static string GetText(string strfilename)
    {
        string strRetval = "";
        System.Text.StringBuilder strBuilder = new System.Text.StringBuilder();
        if (System.IO.File.Exists(strfilename))
        {
            try
            {
                using (StreamReader sr = System.IO.File.OpenText(strfilename))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        strBuilder.AppendLine(s);
                    }
                }
            }
            catch (Exception ex)
            {
                //SendErrorMail(ex);
            }
            finally
            {
                //if (System.IO.File.Exists(strfilename))
                //    System.IO.File.Delete(strfilename);
            }
        }

        if (strBuilder.ToString().Trim() != "")
            strRetval = strBuilder.ToString();
        else
            strRetval = "";

        return strRetval;
    }

    public static string SaveAsText(string strfilename)
    {
        string fileName = "";
        object miss = System.Reflection.Missing.Value;
        Microsoft.Office.Interop.Word.Document doc = null;
        try
        {
            Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
            fileName = Path.GetDirectoryName(strfilename) + @"\" + Path.GetFileNameWithoutExtension(strfilename) + ".txt";
            doc = wordApp.Documents.Open(strfilename, false);
            doc.SaveAs(fileName, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatDOSText);

        }
        catch (Exception ex)
        {

            //SendErrorMail(ex);
        }
        finally
        {
            if (doc != null)
            {
                doc.Close(ref miss, ref miss, ref miss);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(doc);
                doc = null;
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        return fileName;
    }
    }
}