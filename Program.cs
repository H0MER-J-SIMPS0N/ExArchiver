using NLog;
using System;

namespace ExArchiver
{
    class Program
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        public static void Main(string[] args)
        {
            _logger.Info("Start program");
            var mainProcessing = new MainProcessing(args, _logger);
            mainProcessing.Process();
            _logger.Info("End program");
        }
    }
}
