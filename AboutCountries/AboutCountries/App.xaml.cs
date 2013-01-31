using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO.IsolatedStorage;
using System.Text;

namespace AboutCountries
{
    public partial class App : Application
    {
        //public static readonly int[] FavGroupsID = { 1, 5, 25, 60, 89, 120 };
       // public static readonly List<int> FavGroupsID = new List<int>{ 1, 5, 25, 60, 89, 120 };
        public static readonly List<int> FavGroupsID = new List<int>();
        private static IsolatedStorageSettings IDSettings = IsolatedStorageSettings.ApplicationSettings;

        /// <summary>
        /// Provides easy access to the root frame of the Phone Application.
        /// </summary>
        /// <returns>The root frame of the Phone Application.</returns>
        public PhoneApplicationFrame RootFrame { get; private set; }

        /// <summary>
        /// Constructor for the Application object.
        /// </summary>
        public App()
        {
            // Global handler for uncaught exceptions. 
            UnhandledException += Application_UnhandledException;

            // Standard Silverlight initialization
            InitializeComponent();

            // Phone-specific initialization
            InitializePhoneApplication();

            // Show graphics profiling information while debugging.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // Display the current frame rate counters.
                Application.Current.Host.Settings.EnableFrameRateCounter = false;// true;

                // Show the areas of the app that are being redrawn in each frame.
                //Application.Current.Host.Settings.EnableRedrawRegions = true;

                // Enable non-production analysis visualization mode, 
                // which shows areas of a page that are handed off to GPU with a colored overlay.
                //Application.Current.Host.Settings.EnableCacheVisualization = true;

                // Disable the application idle detection by setting the UserIdleDetectionMode property of the
                // application's PhoneApplicationService object to Disabled.
                // Caution:- Use this under debug mode only. Application that disables user idle detection will continue to run
                // and consume battery power when the user is not using the phone.
                PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
            }

        }

        // Code to execute when the application is launching (eg, from Start)
        // This code will not execute when the application is reactivated
        private void Application_Launching(object sender, LaunchingEventArgs e)
        {
            loadSettings();
        }

        public static void loadSettings()
        { // Retrieve and set user name.
            try
            {
                string IdsArray = (string)IDSettings["Ids"];
                if (IdsArray.Length != 0)
                {
                    string[] split = IdsArray.Split(new Char[] { ',' });

                    foreach (string s in split)
                    {
                        if (s.Trim() != "")
                            FavGroupsID.Add(Int16.Parse(s));

                    }
                }
            }
            catch (System.Collections.Generic.KeyNotFoundException)
            {
                // No preference is saved.                
            }
        }
        public static void saveSettings()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                int j = 0;
                foreach (int favCountry in App.FavGroupsID)
                {
                    if (j != 0)
                    {
                        sb.Append(",");
                    }
                    j++;

                    sb.Append(favCountry.ToString());
                }
                IDSettings.Add("Ids", sb.ToString());
            }
            catch (ArgumentException ex)
            {

            }
        }


        // Code to execute when the application is activated (brought to foreground)
        // This code will not execute when the application is first launched
        private void Application_Activated(object sender, ActivatedEventArgs e)
        {
            loadSettings();
        }

        // Code to execute when the application is deactivated (sent to background)
        // This code will not execute when the application is closing
        private void Application_Deactivated(object sender, DeactivatedEventArgs e)
        {
            saveSettings();
        }

        // Code to execute when the application is closing (eg, user hit Back)
        // This code will not execute when the application is deactivated
        private void Application_Closing(object sender, ClosingEventArgs e)
        {
            saveSettings();
        }

        // Code to execute if a navigation fails
        private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // A navigation has failed; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        // Code to execute on Unhandled Exceptions
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        #region Phone application initialization

        // Avoid double-initialization
        private bool phoneApplicationInitialized = false;

        // Do not add any additional code to this method
        private void InitializePhoneApplication()
        {
            if (phoneApplicationInitialized)
                return;

            // Create the frame but don't set it as RootVisual yet; this allows the splash
            // screen to remain active until the application is ready to render.
            RootFrame = new Microsoft.Phone.Controls.TransitionFrame();// new PhoneApplicationFrame();
            RootFrame.Navigated += CompleteInitializePhoneApplication;

            // Handle navigation failures
            RootFrame.NavigationFailed += RootFrame_NavigationFailed;

            // Ensure we don't initialize again
            phoneApplicationInitialized = true;
        }

        // Do not add any additional code to this method
        private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
        {
            // Set the root visual to allow the application to render
            if (RootVisual != RootFrame)
                RootVisual = RootFrame;

            // Remove this handler since it is no longer needed
            RootFrame.Navigated -= CompleteInitializePhoneApplication;
        }

        #endregion
    }
}


/*
 *  13: using System.IO.IsolatedStorage;
 14:
 15:
 16: namespace F5debugWp7IsolatedStorage
 17: {
 18:     public partial class MainPage : PhoneApplicationPage
 19:     {
 20:         // Constructor
 21:         public MainPage()
 22:         {
 23:             InitializeComponent();
 24:         }
 25:
 26:         private void button3_Click(object sender, RoutedEventArgs e)
 27:         {
 28:             IsolatedStorageSettings ISSetting = IsolatedStorageSettings.ApplicationSettings;
 29:
 30:             if (!ISSetting.Contains("DataKey"))
 31:             {
 32:                 ISSetting.Add("DataKey", txtSaveData.Text);
 33:             }
 34:             else
 35:             {
 36:                 ISSetting["DataKey"] = txtSaveData.Text;
 37:             }
 38:             ISSetting.Save();
 39:         }
 40:     }

*/