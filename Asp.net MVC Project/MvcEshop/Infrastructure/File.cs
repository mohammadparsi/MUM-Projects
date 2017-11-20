using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Office.Interop.Word;
using System.Runtime.InteropServices.ComTypes;
using System.IO;

namespace Infrastructure
{
    public class File
    {

        /// <summary>
        /// Get a doc or docx file-path as an input and return the text data of the file. 
        /// </summary>
        /// <param name="strfilename"></param>
        /// <returns></returns>
        public static string GetText(string strfilename)
        {
            string strRetval = "";
            System.Text.StringBuilder strBuilder = new System.Text.StringBuilder();
            if (System.IO.File.Exists(strfilename))
            {
                try
                {
                    using (System.IO.StreamReader sr = System.IO.File.OpenText(strfilename))
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
                    //Infrastructure.Communication.SendEmail(ex);
                }
                finally
                {
                    if (System.IO.File.Exists(strfilename))
                        System.IO.File.Delete(strfilename);
                }
            }

            if (strBuilder.ToString().Trim() != "")
                strRetval = strBuilder.ToString();
            else
                strRetval = "";

            return strRetval;
        }

        /// <summary>
        /// Gets both the path inwhich files are uploaded and the name of the file in the server as
        /// inputs and write the file on the client to be downloaded by client. This is used to avoid using 
        /// links for download in order to meet security rules.
        /// </summary>
        /// <param name="assignmentfilesPath"></param>
        /// <param name="fileNameInServer"></param>
        public static void WriteFileOnClientForDownload
            (string assignmentfilesPath, string fileNameInServer, HttpResponseBase Response)
        {
            string strFilePath = GetFilePath(assignmentfilesPath, fileNameInServer);
            //string strFileName=string.Format(@"filename=""Paper.xls""",)
            Response.ContentType = "application/text";
            string strExtension = string.Format("IranDocPlagiarism{0}", Path.GetExtension(fileNameInServer));
            Response.AddHeader("Content-Disposition", @"filename=" + strExtension + "");
            Response.TransmitFile(strFilePath);
        }

        public static string GetFilePath(string folderPath, string fileName)
        {
           return string.Format(@"{0}{1}", folderPath, fileName);
        }

    }
}