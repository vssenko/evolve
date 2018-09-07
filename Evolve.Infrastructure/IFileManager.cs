using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve.Infrastructure
{
    public interface IFileManager
    {
        string SaveImage(string hostUrl, Stream file);
    }
}
