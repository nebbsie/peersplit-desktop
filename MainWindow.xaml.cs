﻿using System;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region variables
        private User user;
        private string fileLocation;
        private DispatcherTimer jobTimer;
        #endregion

        /// <summary>
        /// Main insertion point of the program.
        /// </summary>
        public MainWindow()
        {
            user = new User();

            InitializeComponent();
            InitialiseVisibilities();
            SetupUI();
            SetupJobTimer();
        }


        /// <summary>
        /// Called if the user had been logged in, it it downloads all users data.
        /// </summary>
        private async void SetupUI()
        {
            // Set the users details.
            main_username_label.Content = user._savedInformation._username;
            main_email_label.Content = user._savedInformation._email;
            main_storage_label.Content = user._savedInformation._storageUsage + "MB/" +  user._savedInformation._storageTier + "MB";
            main_storage_amount.Text = user._savedInformation._storageAmount.ToString();
            main_allowStorage_check.IsChecked = user._savedInformation._allowStorage;

            // Get all of the the uers files in the network.
            FilmResponse res = await FileAPIController.GetAllFilesInNetwork(user._savedInformation._id);
            main_files_listView.ItemsSource = res.data;

            
        }

        /// <summary>
        /// Starts a timer for the holder jobs.
        /// </summary>
        private void SetupJobTimer()
        {
            jobTimer = new DispatcherTimer();
            jobTimer.Tick += new EventHandler(HolderAPIController.DoHolderJobs);
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
            user._savedInformation._allowStorage = (bool)main_allowStorage_check.IsChecked;
            user._savedInformation._storageAmount = Int32.Parse(main_storage_amount.Text);

            user.SaveToFile();
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
        private void InitialiseVisibilities()
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

        #endregion

    }
}
