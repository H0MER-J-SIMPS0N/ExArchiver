using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ExArchiver
{
    class SettingsManager: SettingsManagerHelper
    {
        private Settings _settings;
        private string _settingsByText;
        public SettingsManager() { }

        internal Settings InitializeSettings()
        {            
            try
            {
                _settingsByText = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "settings.json"));
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not read settings file:\r\n{ex}");
            }
            try
            {
                _settings = JsonConvert.DeserializeObject<Settings>(_settingsByText);
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not deserialize settings:\r\n{ex}");
            }
            if (Directory.Exists(_settings.PathToFtp))
            {
                SetFoldersToArchive();
                SetExceptedFolders();                
            }
            else
            {
                throw new Exception($"Directory does not exist {_settings.PathToFtp}");
            }
            return _settings;
        }        

        private void SetFoldersToArchive()
        {
            if (_settings.FoldersToArchive.Any())
            {
                _settings.FoldersToArchive = GetDirectoriesWithExchangeDirectories
                    (
                        _settings.FoldersToArchive
                        .Select(x => Path.Combine(_settings.PathToFtp, x))
                    )
                    .ToList();
            }
            else
            {
                _settings.FoldersToArchive = GetDirectoriesWithExchangeDirectories
                    (
                        Directory.GetDirectories(_settings.PathToFtp, "*", SearchOption.TopDirectoryOnly)
                    )
                    .ToList();
            }
        }

        private void SetExceptedFolders()
        {
            if (_settings.ExceptedFolders.Any())
            {
                _settings.ExceptedFolders = _settings.ExceptedFolders
                    .Select(x => Path.Combine(_settings.PathToFtp, x).ToUpper())
                    .ToList();
            }
            else
            {
                _settings.ExceptedFolders = new List<string>();
            }
        }
    }
}
