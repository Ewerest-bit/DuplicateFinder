using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuplicateFinder.Core
{
    public class FileScanner
    {
        public string[] FileScan(string rootPath)
        {
            if (!Directory.Exists(rootPath))
            {
                throw new DirectoryNotFoundException();
            }

            return Directory.GetFiles(rootPath, "*", SearchOption.AllDirectories);
           
        }
    }
}
