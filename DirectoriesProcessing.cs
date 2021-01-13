using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExArchiver
{
    class DirectoriesProcessing
    {
        private readonly ILogger _logger;
        private readonly DateTime _dateBefore;

        public DirectoriesProcessing(ILogger logger, DateTime dateBefore)
        {
            _logger = logger;
            _dateBefore = dateBefore;
        }

        internal void Process(IEnumerable<string> rootDirectoriesFullPaths)
        {
            Parallel.ForEach(rootDirectoriesFullPaths, (fullPath) =>
            {
                _logger.Info($"Start processing directory {fullPath.ToUpper()}");
                var directories = Directory.GetDirectories(fullPath, "*", SearchOption.AllDirectories)
                    .Where(x => !x.EndsWith("dictionaries", StringComparison.OrdinalIgnoreCase)) ?? new string[0];
                new FilesProcessing(_logger)
                    .Process(directories, _dateBefore);
                _logger.Info($"End processing directory {fullPath.ToUpper()}");
            });
        }
    }
}
