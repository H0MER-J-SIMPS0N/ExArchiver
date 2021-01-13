using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExArchiver
{
    class FilesProcessing
    {
        private readonly ILogger _logger;
        public FilesProcessing(ILogger logger)
        {
            _logger = logger;
        }
        internal void Process(IEnumerable<string> directories, DateTime dateBefore)
        {
            Parallel.ForEach(directories, (directory) =>
            {
                var groupedFiles = Directory.GetFiles(directory, "*", SearchOption.TopDirectoryOnly)
                    .Where(x => new FileInfo(x).LastWriteTime < dateBefore)
                    .Select(x => new FileInfo(x))
                    .GroupBy(x => new { x.LastWriteTime.Year, x.LastWriteTime.Month })
                    .ToList();
                if (!groupedFiles.Any())
                    return;
                foreach (var filesInCurrentGroup in groupedFiles)
                {
                    var selectedFiles = filesInCurrentGroup
                        .Where(fileInfo => !string.Equals(fileInfo.Extension, ".zip", StringComparison.OrdinalIgnoreCase))?
                        .ToList();
                    if (selectedFiles.Any())
                    {
                        new ArchiveFiles(_logger)
                            .Process(selectedFiles);
                    }
                }
            });
        }
    }
}
