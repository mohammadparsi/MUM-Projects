using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcEshop.Models
{
    public class FileNameFileNamesInServer
    {
        public FileNameFileNamesInServer(string fileName, string fileNameInServer, int textContentSize)
        {
            FileName = fileName;
            FileNameInServer = fileNameInServer;
            TextContentSize = textContentSize;
        }
        public string FileName { get; set; }
        public string FileNameInServer { get; set; }
        public int TextContentSize { get; set; }
    }
}