using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Yatsugi.Views
{
    public class UserSettingsView : UserControl
    {
        public UserSettingsView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}