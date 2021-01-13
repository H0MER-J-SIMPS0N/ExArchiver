using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExArchiver
{
    internal class MainProcessing
    {
        private readonly ILogger _logger;
        private readonly IEnumerable<string> _args;
        private Settings _settings;
        private IEnumerable<string> _rootDirectoriesFullPaths;
        internal bool IsFirstArgumentInteger
        {
            get
            {
                int.TryParse(_args?.FirstOrDefault(), out int dateBefore);
                return dateBefore > 0;
            }
        }

        public MainProcessing(IEnumerable<string> args, ILogger logger)
        {
            _args = args;
            _logger = logger;
        }

        internal void Process()
        {
            try
            {
                _settings = new SettingsManager().InitializeSettings();                
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex.ToString());
                return;
            }            
            if (IsFirstArgumentInteger)
            {
                _settings.DateBefore = DateTime.Now.AddMonths(-int.Parse(_args.First())).Date;
            }
            _logger.Trace($"Set settings:\r\n{_settings}");
            _rootDirectoriesFullPaths = _settings.FoldersToArchive.Except(_settings.ExceptedFolders);
            new DirectoriesProcessing(_logger, _settings.DateBefore)
                .Process(_rootDirectoriesFullPaths);
        }

    }
}
