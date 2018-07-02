using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Newtonsoft.Json;
using peersplit_desktop.Model;

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
            Console.WriteLine(user._savedInformation._username);
        }

    }
}
