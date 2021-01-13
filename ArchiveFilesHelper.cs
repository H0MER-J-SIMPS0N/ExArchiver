using System;
using System.IO;
using System.Linq;

namespace ExArchiver
{
    class ArchiveFilesHelper
    {
        protected static string GetFullPathToZipFile(string fullFilePath)
        {
            FileInfo fileInfo = new FileInfo(fullFilePath);
            if (fileInfo.DirectoryName.Split('\\', StringSplitOptions.RemoveEmptyEntries).Select(x => x.ToUpper()).Contains("ARCHIVE"))
            {
                return Path.Combine(fileInfo.DirectoryName, fileInfo.LastWriteTime.ToString("yyyy_MM(MMMM)") + ".zip");
            }
            else
            {
                Directory.CreateDirectory(Path.Combine(fileInfo.DirectoryName, "ARCHIVE"));
                return Path.Combine(fileInfo.DirectoryName, "ARCHIVE", fileInfo.LastWriteTime.ToString("yyyy_MM(MMMM)") + ".zip");
            }
        }
    }
}
