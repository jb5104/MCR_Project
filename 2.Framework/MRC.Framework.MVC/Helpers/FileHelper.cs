using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using MRC.Framework;
namespace MRC.Framework.Mvc.Helpers
{
    public class FileInfo
    {
        public  string FileUpload(HttpPostedFileBase file, string folderName = "")
        {
            string fileExtension = System.IO.Path.GetExtension(file.FileName);
            string fileName = System.IO.Path.GetFileName(file.FileName);
            
            string UploadUrl = System.Configuration.ConfigurationManager.AppSettings["UploadUrl"].ToString() + (string.IsNullOrEmpty(folderName) ? "" : folderName + "/");
            string oriUploadUrl = UploadUrl;
            UploadUrl = HttpContext.Current.Server.MapPath(UploadUrl);
            Global.FileInformation.MakeFolder(UploadUrl);
            string fileLocation = UploadUrl + fileName;
            if (System.IO.File.Exists(fileLocation))
            {
                System.IO.File.Delete(fileLocation);
            }
            file.SaveAs(fileLocation);
            return oriUploadUrl + fileName;
        }

        public  string FileDelete(string fileLocation)
        {
            try
            {
                string directoryUrl = string.Empty;
                string fileName = string.Empty;
                if(string.IsNullOrEmpty(fileLocation))
                {
                    string[] arrUrl = fileLocation.Split('/');
                    fileName = arrUrl[arrUrl.Count() - 1];

                    directoryUrl = fileLocation.Replace(fileName,"");
                    directoryUrl = HttpContext.Current.Server.MapPath(directoryUrl);
                    fileLocation = directoryUrl + fileName;
                    if (System.IO.File.Exists(fileLocation))
                    {
                        System.IO.File.Delete(fileLocation);
                    }
                }
                
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return string.Empty;
        }
    }
}
