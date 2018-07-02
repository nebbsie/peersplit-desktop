using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using peersplit_desktop.Model;

namespace peersplit_desktop.Model
{
    public class User
    {
        public UserSettings _savedInformation { get; set; }
        public string _folderLocation { get; set; }
        public string _userSettingsLocation { get; set; }
        public bool _initalised { get; set; }

        public User()
        {
            // Location of the user settings folder and files.
            _folderLocation = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Peersplit");
            _userSettingsLocation = System.IO.Path.Combine(_folderLocation, "usersettings.json");

            // If the user has used the app before load the information if not create user information file.
            if (GetIfFirstTimeUse())
            {
                CreateAppDataFolder();
                _initalised = false;
            }
            else
            {
                _savedInformation = ReadUserSettings();
                _initalised = true;
            }
        }

        /// <summary>
        /// Check if the user has already used the application.
        /// </summary>
        private bool GetIfFirstTimeUse()
        {
            return !Directory.Exists(_folderLocation);
        }

        /// <summary>
        /// Create the application data folder at the given location.
        /// </summary>
        private void CreateAppDataFolder()
        {
            // Create the base folder.
            DirectoryInfo dir = Directory.CreateDirectory(_folderLocation);

            // Create the user settings file.
            File.Create(_userSettingsLocation);
        }

        /// <summary>
        /// Read the user settings from the file.
        /// </summary>
        private UserSettings ReadUserSettings()
        {
            string rawJSON = File.ReadAllText(_userSettingsLocation);
            return JsonConvert.DeserializeObject<UserSettings>(rawJSON);
        }

        /// <summary>
        /// Save the user settings to file.
        /// </summary>
        public void SaveToFile()
        {
            string json = JsonConvert.SerializeObject(_savedInformation);
            File.WriteAllText(_userSettingsLocation, json);
        }
    }

}
