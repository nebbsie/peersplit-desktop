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
                    LoggedIn();
                }

            }else
            {
                LoggedIn();
            }
        }

        /// <summary>
        /// Called if the user had been logged in, it it downloads all users data.
        /// </summary>
        private void LoggedIn()
        {
            // Set the users details.
            main_username_label.Content = user._savedInformation._username;
            main_email_label.Content = user._savedInformation._email;
            main_storage_label.Content = user._savedInformation._storageUsage + "MB/" +  user._savedInformation._storageTier + "MB";

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
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                main_filemanager_pane.Visibility = Visibility.Visible;
                fileLocation = dlg.FileName;
                double size = new System.IO.FileInfo(fileLocation).Length;

                main_filename_label.Content = dlg.SafeFileName;
                double a = (size / 1024) / 1024;
                Console.WriteLine(a);
                main_size_label.Content = Truncate(a, 2) + " MB";


            }
        }

        /// <summary>
        /// Upload the file to the network.
        /// </summary>
        private async void main_upload_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var res = await ("http://localhost:3000/file/new")
                    .PostMultipartAsync(mp => mp
                    .AddStringParts(new { ownerID = user._savedInformation._id })
                    .AddFile("file", fileLocation)
                    ).ReceiveString();

                main_uploadMSG_label.Visibility = Visibility.Visible;

                UploadResponse json = JsonConvert.DeserializeObject<UploadResponse>(res);

                if (json.success)
                {
                    main_uploadMSG_label.Foreground = Brushes.Green;
                    main_uploadMSG_label.Content = "Uploaded file!";
                    GetAllFilesInNetwork();
                }
                else
                {
                    main_uploadMSG_label.Foreground = Brushes.Red;
                    main_uploadMSG_label.Content = "Failed to upload!";
                }
            }
            catch
            {
                main_uploadMSG_label.Visibility = Visibility.Visible;
                main_uploadMSG_label.Foreground = Brushes.Red;
                main_uploadMSG_label.Content = "Failed to upload!";
            }

        }

        public static double Truncate(double val, int digits)
        {
            double mult = Math.Pow(10.0, digits);
            double result = Math.Truncate(mult * val) / mult;
            return result;
        }
    }
}
