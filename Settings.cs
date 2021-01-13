using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExArchiver
{
    class Settings
    {
        [JsonProperty("pathToFtp", Required = Required.Always)]
        public string PathToFtp { get; private set; }
        [JsonProperty("dateBefore")]
        public DateTime DateBefore { get; set; }
        [JsonProperty("foldersToArchive")]
        public List<string> FoldersToArchive { get; set; }
        [JsonProperty("exceptedFolders")]
        public List<string> ExceptedFolders { get; set; }

        public override string ToString()
        {
            return $"PathToFtp: {PathToFtp}\r\nDateBefore: {DateBefore}\r\nFoldersToArchive count: {FoldersToArchive.Count}\r\nExceptedFolders count: {ExceptedFolders.Count}";
        }
    }
}
