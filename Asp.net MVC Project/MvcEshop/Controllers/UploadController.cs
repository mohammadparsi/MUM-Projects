using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcEshop.Controllers
{
    public partial class UploadController : Infrastructure.BaseControllerWithDatabaseContext
    {
        [HttpPost]
        public virtual ActionResult Save(IEnumerable<HttpPostedFileBase> files)
        {

            // The Name of the Upload component is "files"
            if (files != null)
            {
                foreach (var file in files)
                {
                    List<MvcEshop.Models.FileNameFileNamesInServer> lstFinalChosenFiles =
               System.Web.HttpContext.Current.Session["ListOfFinalChosenFiles"] as List<MvcEshop.Models.FileNameFileNamesInServer>;


                    // Some browsers send file names with full path.
                    // We are only interested in the file name.
                    string strFileNameInServer = Guid.NewGuid().ToString() +
                           System.IO.Path.GetExtension(file.FileName);


                    file.SaveAs(string.Format
                                (@"{0}", DFEntities.Settings.FirstOrDefault().AssignmentFilesPath)
                                + strFileNameInServer);

                    string strText = MVC.Assignment.GetTextByFileNameInServer(strFileNameInServer);
                    int intTextContentSize = System.Text.ASCIIEncoding.Unicode.GetByteCount(strText);
                    //int? intPaymentAmount =
                    //     System.Text.ASCIIEncoding.Unicode.GetByteCount(strText) / 1000 *
                    //     (intIrandoc * this.IrandocTariff + intInternet * this.InternetTariff);
                    lstFinalChosenFiles.Add
                        (new MvcEshop.Models.FileNameFileNamesInServer
                            (file.FileName, strFileNameInServer, intTextContentSize));


                    // The files are not actually saved in this demo
                    // file.SaveAs(physicalPath);
                }
            }

            // Return an empty string to signify success
            return Content("");
        }

        public virtual ActionResult Remove(string[] fileNames)
        {
            // The parameter of the Remove action must be called "fileNames"

            if (fileNames != null)
            {
                List<MvcEshop.Models.FileNameFileNamesInServer> lstFinalChosenFiles =
               System.Web.HttpContext.Current.Session["ListOfFinalChosenFiles"] as List<MvcEshop.Models.FileNameFileNamesInServer>;

                foreach (var fileName in fileNames)
                {
                    foreach (var file in lstFinalChosenFiles)
                    {
                        if (string.Equals(file.FileName, fileName))
                        {
                            lstFinalChosenFiles.Remove(file);
                            break;
                        }
                    }
                }
            }

            // Return an empty string to signify success
            return Content("");
        }

        [HttpPost]
        public virtual ActionResult SaveSlideShowImage(IEnumerable<HttpPostedFileBase> files)
        {

            // The Name of the Upload component is "files"
            if (files != null)
            {
                foreach (var file in files)
                {
                    string strSlideShowImagesFolderPath =
                        System.Web.Hosting.HostingEnvironment.MapPath
                        ("~/Content/Files/Images/SlideShow");

                    bool blnFirstUpload =
                        Boolean.Parse(System.Web.HttpContext.Current.Session["NoImagesHasBeenUploaded"].ToString());

                    System.Web.HttpContext.Current.Session["NoImagesHasBeenUploaded"] = blnFirstUpload;

                    if (blnFirstUpload)
                    {
                        //delete all images in the folder "SlideShow".
                        string[] filePaths = System.IO.Directory.GetFiles(strSlideShowImagesFolderPath);

                        foreach (string filePath in filePaths)
                            System.IO.File.Delete(filePath);

                        //set the session to "false" in order not to delete all files again and just do it for the first time.
                        System.Web.HttpContext.Current.Session["NoImagesHasBeenUploaded"] = false;
                    }


                    string strDestinationPath =
                        System.IO.Path.Combine(strSlideShowImagesFolderPath, file.FileName);
                    //var destinationPath = Path.Combine(Server.MapPath("~/App_Data"), fileName);

                    file.SaveAs(strDestinationPath);


                    // The files are not actually saved in this demo
                    // file.SaveAs(physicalPath);
                }
            }

            // Return an empty string to signify success
            return Content("");
        }

        public virtual ActionResult RemoveSlideShowImage(string[] fileNames)
        {
            // The parameter of the Remove action must be called "fileNames"

            if (fileNames != null)
            {
                
                foreach (var fileName in fileNames)
                {
                    string strDestinationPath =
                        System.IO.Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath
                        ("~/Content/Files/Images/SlideShow"), fileName);
                    //var destinationPath = Path.Combine(Server.MapPath("~/App_Data"), fileName);

                  if (System.IO.File.Exists(strDestinationPath))
                    {
                        System.IO.File.Delete(strDestinationPath);
                    }

 
                }
            }

            // Return an empty string to signify success
            return Content("");
        }

        //public ActionResult RemoveAndPersist(string[] fileNames)
        //{
        //    // The parameter of the Remove action must be called "fileNames"
        //    if (fileNames != null)
        //    {
        //        foreach (var fullName in fileNames)
        //        {
        //            var fileName = Path.GetFileName(fullName);
        //            var physicalPath = Path.Combine(Server.MapPath("~/App_Data"), fileName);

        //            SessionUploadInitialFilesRepository.Remove(fileName);

        //            if (System.IO.File.Exists(physicalPath))
        //            {
        //                // The files are not actually removed in this demo
        //                // System.IO.File.Delete(physicalPath);
        //            }
        //        }
        //    }

        //    // Return an empty string to signify success
        //    return Content("");
        //}


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                DFEntities.Dispose();
            }
            base.Dispose(disposing);
        }



    }
}