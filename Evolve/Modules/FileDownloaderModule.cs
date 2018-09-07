using Nancy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve.Modules
{
    public class FileDownloaderModule : NancyModule
    {
        public FileDownloaderModule()
        {
            Get["/img/{id}", true] = async (_, ct) =>
            {
                var saveFilePath = Directory.GetCurrentDirectory() + "/img/" + (string)_.id;
                return Response.AsImage(saveFilePath);
            };
        }
    }
}
