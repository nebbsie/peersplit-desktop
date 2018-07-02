using System;
using System.Windows;
using Newtonsoft.Json;
using peersplit_desktop.Model;
using peersplit_desktop.Model.APIResponse;
using Flurl.Http;
using System.Windows.Media;

namespace peersplit_desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region variables
        private User user;
        private string fileLocation;
        #endregion

        /// <summary>
        /// Main insertion point of the program.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            main_filemanager_pane.Visibility = Visibility.Hidden;
            main_uploadMSG_label.Visibility = Visibility.Hidden;

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
                    UserLoggedIn();
                }

            }else
            {
                UserLoggedIn();
            }
        }

        /// <summary>
        /// Called if the user had been logged in, it it downloads all users data.
        /// </summary>
        private void UserLoggedIn()
        {
            // Set the users details.
            main_username_label.Content = user._savedInformation._username;
            main_email_label.Content = user._savedInformation._email;
            main_storage_label.Content = user._savedInformation._storageUsage + "MB/" +  user._savedInformation._storageTier + "MB";
            main_storage_amount.Text = user._savedInformation._storageAmount.ToString();
            main_allowStorage_check.IsChecked = user._savedInformation._allowStorage;

            // Get all of the the uers files in the network.
            GetAllFilesInNetwork();
        }

        /// <summary>
        /// Get all of the files the user has uploaded and present them on screen.
        /// </summary>
        private async void GetAllFilesInNetwork()
        {
            try
            {
                // Call the login api.
                var res = await ("http://localhost:3000/file/getAll")
                    .PostUrlEncodedAsync(new { ownerID = user._savedInformation._id })
                    .ReceiveString();

                FilmResponse filmRes = JsonConvert.DeserializeObject<FilmResponse>(res);
                main_files_listView.ItemsSource = filmRes.data;
            }
            catch
            {
                Console.WriteLine("Failed to get files");
            }
            
        }

        /// <summary>
        /// Present a file dialog screen.
        /// </summary>
        private void SelectFile(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                main_filemanager_pane.Visibility = Visibility.Visible;

                // Get the filename/location/size and present it on screen.
                fileLocation = dlg.FileName;
                double size = new System.IO.FileInfo(fileLocation).Length;
                double MB = (size / 1024) / 1024;

                main_filename_label.Content = dlg.SafeFileName;
                main_size_label.Content = Truncate(MB, 2) + " MB";
            }
        }

        /// <summary>
        /// Upload the file to the network.
        /// </summary>
        private async void UploadButton(object sender, RoutedEventArgs e)
        {
            try
            {
                var res = await ("http://localhost:3000/file/new")
                    .PostMultipartAsync(mp => mp
                    .AddStringParts(new { ownerID = user._savedInformation._id })
                    .AddFile("file", fileLocation)
                    ).ReceiveString();

                UploadResponse json = JsonConvert.DeserializeObject<UploadResponse>(res);

                if (json.success)
                {
                    SetUploadMessage(Brushes.Green, "Uploaded file!");
                    GetAllFilesInNetwork();
                }
                else
                {
                    SetUploadMessage(Brushes.Red, "Failed to upload!");
                }
            }
            catch
            {
                SetUploadMessage(Brushes.Red, "Failed to upload!");
            }
        }

        /// <summary>
        /// Sets the upload message parameters
        /// </summary>
        private void SetUploadMessage(Brush brush = null, string msg = "", Visibility vis = Visibility.Visible)
        {
            if (brush != null)
            {
                main_uploadMSG_label.Foreground = brush;
            }

            main_uploadMSG_label.Visibility = vis;
            main_uploadMSG_label.Content = msg;
        }

        /// <summary>
        /// Take a double and only keep up to a certain amount of digits.
        /// </summary>
        private static double Truncate(double val, int digits)
        {
            double mult = Math.Pow(10.0, digits);
            double result = Math.Truncate(mult * val) / mult;
            return result;
        }

        /// <summary>
        /// Update the user settings.
        /// </summary>
        private void UpdateSettings(object sender, RoutedEventArgs e)
        {
            user._savedInformation._allowStorage = (bool)main_allowStorage_check.IsChecked;
            user._savedInformation._storageAmount = Int32.Parse(main_storage_amount.Text);

            user.SaveToFile();
        }
    }
}
