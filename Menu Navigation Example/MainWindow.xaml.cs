using MaterialDesignThemes.Wpf;
using Menu_Navigation_Example.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Menu_Navigation_Example
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            MenuItems  =
                new List<MenuItemViewModel>
                {
                new MenuItemViewModel { Header = "menu1" },
                new MenuItemViewModel { Header = "menu2",
                    SubMenuItems = new List<MenuItemViewModel>
                    {
                    new MenuItemViewModel { Header = "menu2_1" },
                    new MenuItemViewModel { Header = "menu2_2",
                        SubMenuItems = new List<MenuItemViewModel>
                        {
                        new MenuItemViewModel { Header = "menu2_2_1" },
                        new MenuItemViewModel { Header = "menu2_2_2" },
                        new MenuItemViewModel { Header = "menu2_2_3" }
                        }
                    },
                    new MenuItemViewModel { Header = "menu2_3" }
                }
                },
                new MenuItemViewModel { Header = "menu3" }
            };


            InitializeComponent();

            this.DataContext = this;
        }

        public List<MenuItemViewModel> MenuItems { get; set; }

        private void MenuDarkModeButton_Click(object sender, RoutedEventArgs e)
            => ModifyTheme(DarkModeToggleButton.IsChecked == true);

        private static void ModifyTheme(bool isDarkTheme)
        {
            var paletteHelper = new PaletteHelper();
            var theme = paletteHelper.GetTheme();

            theme.SetBaseTheme(isDarkTheme ? Theme.Dark : Theme.Light);
            paletteHelper.SetTheme(theme);
        }
    }
}
