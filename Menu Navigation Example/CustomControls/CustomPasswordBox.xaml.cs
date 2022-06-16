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

namespace Menu_Navigation_Example.CustomControls
{
    /// <summary>
    /// Interaction logic for CustomPasswordBox.xaml
    /// </summary>
    public partial class CustomPasswordBox : UserControl
    {
        public CustomPasswordBox()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty SetTextProperty =
        DependencyProperty.Register("SetText", typeof(string), 
            typeof(CustomPasswordBox), new
           PropertyMetadata("", new PropertyChangedCallback(OnSetTextChanged)));

        public string SetText
        {
            get { return (string)GetValue(SetTextProperty); }
            set { SetValue(SetTextProperty, value); }
        }

        private static void OnSetTextChanged(DependencyObject d,
         DependencyPropertyChangedEventArgs e)
        {
            CustomPasswordBox UserControl1Control = d as CustomPasswordBox;
            UserControl1Control.OnSetTextChanged(e);
        }

        private void OnSetTextChanged(DependencyPropertyChangedEventArgs e)
        {
            txtPassword.Password = e.NewValue.ToString();
        }

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = (PasswordBox)sender;
            SetText = passwordBox.Password;
        }
    }
}
