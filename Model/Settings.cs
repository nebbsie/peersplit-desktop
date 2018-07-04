using Newtonsoft.Json;
using System.IO;

namespace peersplit_desktop.Model
{
    public class Settings
    {
        /// <summary>
        /// Save the user settings to file.
        /// </summary>
        public void SaveToFile(object _savedInformation, string _settingsLocation)
        {
            string json = JsonConvert.SerializeObject(_savedInformation);
            File.WriteAllText(_settingsLocation, json);
        }

    }
}
