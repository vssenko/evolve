using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve.Infrastructure
{
    public class FileManager : IFileManager
    {

        public string SaveImage(string hostUrl, Stream file)
        {
            var saveDirectory = Directory.GetCurrentDirectory();
            var newGuid = Guid.NewGuid();
            saveDirectory = saveDirectory + "/img/" + newGuid.ToString("N") + ".jpg";
            using (var fs = File.Create(saveDirectory))
            {
                file.Seek(0, SeekOrigin.Begin);
                file.CopyTo(fs);
            }
            var fileUrl = hostUrl + "/img/" + newGuid.ToString("N") + ".jpg";
            return fileUrl;
        }
    }
}
