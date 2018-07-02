using System;
using System.Windows;
using Newtonsoft.Json;
using peersplit_desktop.Model;
using peersplit_desktop.Model.APIResponse;
using Flurl.Http;

namespace peersplit_desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region variables
        private User user;
        #endregion

        /// <summary>
        /// Main insertion point of the program.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            // Initialise the user and get settings.
            user = new User();

            // If not initialised, ask for user to login.
            if (!user._initalised)
            {
                LoginWindow login = new LoginWindow();
                login.ShowDialog();

                // Check if the login was a success.
                if ((bool)login.DialogResult)
                {
                    // Logged in correctly, save the new infomration from API to the user object.
                    user._savedInformation = login._user;
                    user.SaveToFile();
                    LoggedIn();
                }

            }else
            {
                LoggedIn();
            }
        }

        private void LoggedIn()
        {
            // Set the users details.
            main_username_label.Content = user._savedInformation._username;
            main_email_label.Content = user._savedInformation._email;
            main_storage_label.Content = user._savedInformation._storageUsage + "MB/" +  user._savedInformation._storageTier + "MB";

            // Get all of the the uers files in the network.
            GetAllFilesInNetwork();
        }

        private async void GetAllFilesInNetwork()
        {
            // Call the login api.
            var res = await("http://localhost:3000/file/getAll")
                .PostUrlEncodedAsync(new { ownerID = user._savedInformation._id })
                .ReceiveString();

            FilmResponse filmRes = JsonConvert.DeserializeObject<FilmResponse>(res);
            main_files_listView.ItemsSource = filmRes.data;
        }

    }
}
