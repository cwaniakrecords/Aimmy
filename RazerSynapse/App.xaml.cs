using RazerSynapse.Theme;
using Class;
using System.Windows;

namespace RazerSynapse
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            InitializeTheme();
            ShutdownMode = ShutdownMode.OnExplicitShutdown;

#if DEBUG
            var _mainWindow = new MainWindow();
            MainWindow = _mainWindow;
            _mainWindow.Show();
            return;
#endif
            try
            {
                var startupWindow = new StartupWindow();
                startupWindow.Show();
                ShutdownMode = ShutdownMode.OnMainWindowClose;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Startup animation failed: {ex.Message}\nLaunching main application...",
                              "Razer Synapse", MessageBoxButton.OK, MessageBoxImage.Information);

                var mainWindow = new MainWindow();
                MainWindow = mainWindow;
                mainWindow.Show();

                ShutdownMode = ShutdownMode.OnMainWindowClose;
            }
        }

        private void InitializeTheme()
        {
            try
            {
                var colorState = new Dictionary<string, dynamic>
                {
                    { "Theme Color", "#FF722ED1" }
                };

                SaveDictionary.LoadJSON(colorState, "bin\\colors.cfg");

                if (colorState.TryGetValue("Theme Color", out var themeColor) && themeColor is string colorString)
                {
                    ThemeManager.SetThemeColor(colorString);
                }
                else
                {
                    ThemeManager.SetThemeColor("#FF722ED1");
                }
            }
            catch (Exception ex)
            {
                ThemeManager.SetThemeColor("#FF722ED1");
            }
        }
    }
}