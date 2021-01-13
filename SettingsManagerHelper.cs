using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ExArchiver
{
    class SettingsManagerHelper
    {
        protected static IEnumerable<string> GetDirectoriesWithExchangeDirectories(IEnumerable<string> directories)
        {
            foreach (var directory in directories)
                if (HasExchangeDirectory(directory))
                    yield return directory.ToUpper();
        }

        protected static bool HasExchangeDirectory(string directory)
        {
            var exchangeFolderNames = new[] { "in", "out" };
            return exchangeFolderNames
                .Any(exchangeFolderName =>
                    Directory.Exists(Path.Combine(directory, exchangeFolderName)));
        }
    }
}
