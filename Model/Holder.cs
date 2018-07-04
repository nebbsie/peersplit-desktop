using System;
using System.IO;
using Newtonsoft.Json;
using peersplit_desktop.Controller;
using peersplit_desktop.Model.APIResponse;

namespace peersplit_desktop.Model
{
    public class Holder
    {
        public HolderSettings _savedInformation { get; set; }
        public string _folderLocation { get; set; }
        public string _holderSettingsLocation { get; set; }
        public bool _initalised { get; set; }

        public Holder(int id)
        {
            // Location of the user settings folder and files.
            _folderLocation = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Peersplit");
            _holderSettingsLocation = System.IO.Path.Combine(_folderLocation, "holderinfo.json");


            // If the holder settings file has not been created yet, do it.
            if (GetIfFirstTimeUse())
            {
                _savedInformation = new HolderSettings();
                // Create the holder settings file.
                using (StreamWriter sr = File.CreateText(_holderSettingsLocation))
                {
                    string json = JsonConvert.SerializeObject(_savedInformation);
                    sr.WriteLine(json);
                }
            }
            else
            {
                // Read the holder settings gile.
                _savedInformation = ReadHolderSettings();
            }

            _savedInformation.id = id;
        }

        /// <summary>
        /// Do the jobs required by each holder every n seconds.
        /// </summary>
        public void DoHolderJobs(object sender, EventArgs e)
        {
            // Update last online time.
            // Check for jobs to do.
            // Do each job.
            Console.WriteLine("Doing Task");
        }

        /// <summary>
        /// Attempts to register a new holder in the API.
        /// </summary>
        public async void RegisterWithAPIAsync()
        {
            HolderCreateResponse res = await HolderAPIController.RegisterHolder(_savedInformation.id, _savedInformation.name, _savedInformation.storageAmount);
            if (res.success)
            {
                _savedInformation.id = res.data;
                SaveToFile();
            }
        }

        /// <summary>
        /// Read the holder settings from the file.
        /// </summary>
        private HolderSettings ReadHolderSettings()
        {
            var res = File.ReadAllText(_holderSettingsLocation);
            return JsonConvert.DeserializeObject<HolderSettings>(res);
        }

        /// <summary>
        /// Check if the user has already used the application.
        /// </summary>
        private bool GetIfFirstTimeUse()
        {
            return !File.Exists(_holderSettingsLocation);
        }

        /// <summary>
        /// Save the object to a file.
        /// </summary>
        public void SaveToFile()
        {
            _savedInformation.SaveToFile(_savedInformation, _holderSettingsLocation);
        }

    }
}
