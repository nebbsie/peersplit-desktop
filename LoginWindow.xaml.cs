using System;
using System.Windows;
using Flurl.Http;
using Newtonsoft.Json;
using peersplit_desktop.Model.APIResponse;
using peersplit_desktop.Model;

namespace peersplit_desktop
{ 
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public UserSettings _user { get; set; }

        public LoginWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Called when the login button has been pressed.
        /// </summary>
        private async void LoginButton(object sender, RoutedEventArgs e)
        {
            login_invalidText_label.Visibility = Visibility.Hidden;

            string _username = login_username_input.Text;
            string _password = login_password_input.Password;

            // Check if the given username or password are empty.
            if (string.IsNullOrEmpty(_username) || string.IsNullOrEmpty(_password))
            {
                login_invalidText_label.Visibility = Visibility.Visible;
            }
            else
            {
                // Call the login api.
                var res = await ("http://localhost:3000/user/login")
                    .PostUrlEncodedAsync(new { log_username = _username, log_password = _password })
                    .ReceiveString();

                try
                {
                    LoginResponse json = JsonConvert.DeserializeObject<LoginResponse>(res);
                    if (json.success)
                    {
                        UserSettings u = new UserSettings();
                        u._username = json.data.username;
                        u._password = _password;
                        u._storageTier = json.data.storageTier;
                        u._storageUsage = json.data.storageUsage;
                        u._email = json.data.email;
                        u._id = json.data.id;
                        u._allowStorage = true;
                        u._storageAmount = 20;

                        // Return the settings from the API.
                        _user = u;
                        DialogResult = true;
                    }
                    else
                    {
                        login_invalidText_label.Visibility = Visibility.Visible;
                    }
                }
                catch
                {
                    login_invalidText_label.Visibility = Visibility.Visible;
                }
            }
        }
    }
}
