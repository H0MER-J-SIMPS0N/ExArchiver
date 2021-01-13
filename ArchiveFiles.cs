using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace ExArchiver
{
    class ArchiveFiles: ArchiveFilesHelper
    {
        private readonly ILogger _logger;

        public ArchiveFiles(ILogger logger)
        {
            _logger = logger;
        }
        internal void Process(IEnumerable<FileInfo> files)
        {
            string fullPathToZipFile = GetFullPathToZipFile(files.FirstOrDefault().FullName);
            try
            {
                ZipArchiveMode mode = File.Exists(fullPathToZipFile) ? ZipArchiveMode.Update : ZipArchiveMode.Create;
                using ZipArchive archive = ZipFile.Open(fullPathToZipFile, mode);
                foreach (var file in files)
                    if (TryArchiveCurrentFile(archive, file))
                        TryDeleteCurrentFile(file);
            }
            catch (Exception ex2)
            {
                _logger.Error($"Could not add files to archive {fullPathToZipFile}\n{ex2}");
            }
        }

        private bool TryArchiveCurrentFile(ZipArchive archive, FileInfo file)
        {
            try
            {
                archive.CreateEntryFromFile(file.FullName, file.Name);
            }
            catch (Exception ex)
            {
                _logger.Error($"Could not add file {file.FullName} to archive\n{ex}");
                return false;
            }
            return true;
            
        }

        private bool TryDeleteCurrentFile(FileInfo file)
        {
            try
            {
                File.Delete(file.FullName);
            }
            catch (Exception ex)
            {
                _logger.Warn($"Could not delete file {file.FullName}\n{ex}");
                return false;
            }
            return true;
        }
    }
}
