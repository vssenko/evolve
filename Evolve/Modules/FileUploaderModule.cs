using Nancy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy.Security;
using Evolve.Infrastructure;
namespace Evolve.Modules
{
    public class FileUploaderModule : NancyModule
    {
        public FileUploaderModule(IFileManager fileManager)
        {
            this.RequiresAuthentication();
            Post["/uploadimage",true] = async (x,ct) =>
            {
                var file = this.Request.Files.First();
                var ckeditorFuncNum = this.Request.Query["CKEditorFuncNum"];
                var hostAddress = this.Request.Url.SiteBase;
                var fileUrl = fileManager.SaveImage(hostAddress,file.Value);
                StringBuilder sb = new StringBuilder();
                var message = "Success";
                sb.AppendFormat("<script>window.parent.CKEDITOR.tools.callFunction('{0}', '{1}','{2}')", ckeditorFuncNum, fileUrl, message);
                sb.Append("</script>");
                return sb.ToString();
            };
        }

    }
}
