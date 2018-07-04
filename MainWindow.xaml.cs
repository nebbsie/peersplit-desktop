using System;
using System.Windows;
using Newtonsoft.Json;
using peersplit_desktop.Model;
using peersplit_desktop.Controller;
using peersplit_desktop.Model.APIResponse;
using Flurl.Http;
using System.Windows.Media;
using System.Windows.Threading;

namespace peersplit_desktop
{
    /// <summary>
    /// Attempts to register a new holder in the API.
    /// </summary>
    public partial class MainWindow : Window
    {
        #region variables
        private User user;
        private Holder holder;
        private string fileLocation;
        private DispatcherTimer jobTimer;
        #endregion

        /// <summary>
        /// Main insertion point of the program.
        /// </summary>
        public MainWindow()
        {
            user = new User();
            holder = new Holder(user._savedInformation._id);

            SetupNotifyIcon();
            InitializeComponent();
            InitialiseUI();
            SetupUI();
            SetupJobTimer();
        }

        /// <summary>
        /// Creates the notification bar for the application and links it to the double click functionality.
        /// </summary>
        private void SetupNotifyIcon()
        {
            System.Windows.Forms.NotifyIcon ni = new System.Windows.Forms.NotifyIcon();
            ni.Icon = new System.Drawing.Icon("icon.ico");
            ni.Visible = true;
            ni.MouseDoubleClick += DoubleClickNotificationIcon;
        }

        /// <summary>
        /// Called when the notification icon is double clicked.
        /// </summary>
        private void DoubleClickNotificationIcon(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.Show();
            this.WindowState = WindowState.Normal;
        }

        /// <summary>
        /// Called when a user minimises the application
        /// </summary>
        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == System.Windows.WindowState.Minimized)
            {
                this.Hide();
            }
            base.OnStateChanged(e);
        }

        /// <summary>
        /// Starts a timer for the holder jobs.
        /// </summary>
        private void SetupJobTimer()
        {
            jobTimer = new DispatcherTimer();
            jobTimer.Tick += new EventHandler(holder.DoHolderJobs);
            jobTimer.Interval = new TimeSpan(0, 0, 5);
            jobTimer.Start();
        }

        #region UI Buttons

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
                main_size_label.Content = MathUtilities.Truncate(MB, 2) + " MB";
            }
        }

        /// <summary>
        /// Update the user settings.
        /// </summary>
        private void UpdateSettings(object sender, RoutedEventArgs e)
        {
            // If active holder act as an update settings button.
            if (holder._savedInformation.activeHolder)
            {
                user._savedInformation._allowStorage = (bool)main_allowStorage_check.IsChecked;
                holder._savedInformation.storageAmount = Int32.Parse(main_storage_amount.Text);
                user.SaveToFile();
            }
            else
            {
                holder._savedInformation.activeHolder = true;
                holder.SaveToFile();
                UpdateSettingsPanel();
                holder.RegisterWithAPIAsync();
            }
            
        }

        /// <summary>
        /// Upload the file to the network.
        /// </summary>
        private async void UploadButton(object sender, RoutedEventArgs e)
        {
            bool uploaded = await FileAPIController.UploadFile(user._savedInformation._id, fileLocation);
            if (uploaded)
            {
                SetUploadMessage(Brushes.Green, "Uploaded file!");
                FilmResponse res = await FileAPIController.GetAllFilesInNetwork(user._savedInformation._id);
                main_files_listView.ItemsSource = res.data;
            }
            else
            {
                SetUploadMessage(Brushes.Red, "Failed to upload!");
            }
        }

        #endregion

        #region UI Helpers

        /// <summary>
        /// Initialise parts of the UI's visibilitiy.
        /// </summary>
        private void InitialiseUI()
        {
            main_filemanager_pane.Visibility = Visibility.Hidden;
            main_uploadMSG_label.Visibility = Visibility.Hidden; 
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
        /// Update the settings panel.
        /// </summary>
        private void UpdateSettingsPanel()
        {
            if (holder._savedInformation.activeHolder)
            {
                main_activate_button.Content = "Update";
                main_storage_amount.Text = holder._savedInformation.storageAmount.ToString();
            }
            else
            {
                main_activate_button.Content = "Activate";
                main_storage_amount.Text = "0";
            }
        }

        /// <summary>
        /// Called if the user had been logged in, it it downloads all users data.
        /// </summary>
        private async void SetupUI()
        {
            // Set the users details.
            main_username_label.Content = user._savedInformation._username;
            main_email_label.Content = user._savedInformation._email;
            main_storage_label.Content = user._savedInformation._storageUsage + "MB/" + user._savedInformation._storageTier + "MB";
            main_storage_amount.Text = holder._savedInformation.storageAmount.ToString();
            main_allowStorage_check.IsChecked = user._savedInformation._allowStorage;

            // Get all of the the uers files in the network.
            FilmResponse res = await FileAPIController.GetAllFilesInNetwork(user._savedInformation._id);
            main_files_listView.ItemsSource = res.data;

            // Update the settings panel, changes button from Activate to Update etc.
            UpdateSettingsPanel();
        }

        #endregion

    }
}
