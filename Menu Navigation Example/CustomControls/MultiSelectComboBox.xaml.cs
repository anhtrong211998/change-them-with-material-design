using Menu_Navigation_Example.Models;
using System;
using System.Collections;
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

namespace Menu_Navigation_Example.CustomControls
{
    /// <summary>
    /// Interaction logic for MultiSelectComboBox.xaml
    /// </summary>
    public partial class MultiSelectComboBox : UserControl
    {
        private ObservableCollection<Node> _nodeList;

        public MultiSelectComboBox()
        {
            _nodeList = new ObservableCollection<Node>();
            InitializeComponent();
        }

        #region Dependency Properties
        public static readonly DependencyProperty ItemsSourceProperty =
                DependencyProperty.Register("ItemsSource", typeof(IList),
                    typeof(MultiSelectComboBox),
                    new FrameworkPropertyMetadata(null,
                    new PropertyChangedCallback(MultiSelectComboBox.OnItemsSourceChanged)));

        public static readonly DependencyProperty SelectedItemsProperty =
                DependencyProperty.Register("SelectedItems",
                    typeof(IList),
                    typeof(MultiSelectComboBox),
                    new FrameworkPropertyMetadata(null,
                        new PropertyChangedCallback(MultiSelectComboBox.OnSelectedItemsChanged)));

        public static readonly DependencyProperty SelectedValuesProperty =
                DependencyProperty.Register("SelectedValues",
                    typeof(string), 
                    typeof(MultiSelectComboBox), new
                    PropertyMetadata("", 
                        new PropertyChangedCallback(OnSelectedValuesChanged)));

        public static readonly DependencyProperty SelectedValuePathProperty =
                DependencyProperty.Register("SelectedValuePath",
                    typeof(string),
                    typeof(MultiSelectComboBox),
                    new UIPropertyMetadata(string.Empty));

        public static readonly DependencyProperty DisplayMemberPathProperty =
                DependencyProperty.Register("DisplayMemberPath",
                    typeof(string),
                    typeof(MultiSelectComboBox),
                    new UIPropertyMetadata(string.Empty));

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string),
                typeof(MultiSelectComboBox),
                new UIPropertyMetadata(string.Empty));
        #endregion

        #region Properites
        public string DisplayMemberPath
        {
            get { return (string)GetValue(DisplayMemberPathProperty); }
            set { SetValue(DisplayMemberPathProperty, value); }
        }

        public IList ItemsSource
        {
            get
            {
                return (IList)GetValue(ItemsSourceProperty);
            }
            set
            {
                SetValue(ItemsSourceProperty, value);
            }
        }

        public IList SelectedItems
        {
            get { return (IList)GetValue(SelectedItemsProperty); }
            set
            {
                SetValue(SelectedItemsProperty, value);
            }
        }

        public string SelectedValues
        {
            get { 
                var a= (string)GetValue(SelectedValuesProperty);
                return a;
            }
            set
            {
                SetValue(SelectedValuesProperty, value);
            }
        }

        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }
            set
            {
                SetValue(TextProperty, value);
            }
        }

        public string SelectedValuePath
        {
            get { return (string)GetValue(SelectedValuePathProperty); }
            set { SetValue(SelectedValuePathProperty, value); }
        }
        #endregion

        #region Events
        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MultiSelectComboBox control = (MultiSelectComboBox)d;
            control.DisplayInControl();

        }

        private static void OnSelectedItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MultiSelectComboBox control = (MultiSelectComboBox)d;
            control.SelectNodes();
            control.SetText();
        }

        private static void OnSelectedValuesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MultiSelectComboBox control = (MultiSelectComboBox)d;
            control.SelectValuesNodes();
            control.SetText();
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            SetSelectedItems();
            SetText();
        }
        #endregion

        #region Methods
        private void DisplayInControl()
        {
            if (_nodeList != null && this.ItemsSource != null)
            {
                _nodeList.Clear();
                foreach (var keyValue in this.ItemsSource)
                {
                    var displayTextKey = keyValue.GetType().GetProperties().FirstOrDefault(x => x.Name == DisplayMemberPath);
                    var displayTextKeyValue = displayTextKey?.GetValue(keyValue);

                    var idKey = keyValue.GetType().GetProperties().FirstOrDefault(x => x.Name == SelectedValuePath);
                    var idKeyValue = idKey?.GetValue(keyValue);

                    Node node = new Node(idKeyValue?.ToString(), displayTextKeyValue?.ToString());
                    _nodeList.Add(node);
                }
                MultiSelectCombo.ItemsSource = _nodeList;
            }
        }

        private void SelectNodes()
        {
            if (SelectedItems == null)
                return;
            foreach (var node in _nodeList)
            {
                node.IsSelected = SelectedItems.Cast<object>().Any(x => x.GetType()
                                            .GetProperties().FirstOrDefault(y => y.Name == SelectedValuePath)?
                                            .GetValue(x)?.ToString() == node.Key);
            }
        }

        private void SelectValuesNodes()
        {
            if (string.IsNullOrEmpty(SelectedValues?.ToString()) || string.IsNullOrWhiteSpace(SelectedValues?.ToString()))
                return;

            List<string?> selectedValueLst = SelectedValues?.ToString().Split(',').ToList();
            var selectedNodes = _nodeList.Where(x => selectedValueLst.Contains(x.Key)).ToList();

            if (SelectedItems == null)
            {
                var listType = this.ItemsSource.GetType();
                var instance = Activator.CreateInstance(listType);
                SelectedItems = (IList)instance;
            }

            SelectedItems.Clear();

            foreach (var node in selectedNodes)
            {
                node.IsSelected = true;
                var item = this.ItemsSource.Cast<object>().FirstOrDefault(x => x.GetType().GetProperties().FirstOrDefault(y => y.Name == SelectedValuePath).GetValue(x)?.ToString() == node.Key);
                SelectedItems.Add(item);
            }
        }

        private void SetText()
        {
            if (this.SelectedItems != null)
            {
                StringBuilder displayText = new StringBuilder();
                foreach (Node s in _nodeList)
                {
                    if (s.IsSelected == true)
                    {
                        displayText.Append(s.Title);
                        displayText.Append(',');
                    }
                }
                this.Text = displayText.ToString().TrimEnd(new char[] { ',' });
            }
        }

        private void SetSelectedItems()
        {
            if (SelectedItems == null)
            {
                var listType = this.ItemsSource.GetType();
                var instance = Activator.CreateInstance(listType);
                //typeof()
                SelectedItems = (IList)instance;
            }

            List<string?> selectedValueLst = new List<string?>();
            SelectedItems.Clear();
            foreach (Node node in _nodeList)
            {
                if (node.IsSelected)
                {
                    if (this.ItemsSource.Count > 0)
                    {
                        var item = this.ItemsSource.Cast<object>().FirstOrDefault(x => x.GetType().GetProperties().FirstOrDefault(y => y.Name == SelectedValuePath).GetValue(x)?.ToString() == node.Key);
                        SelectedItems.Add(item);
                        selectedValueLst.Add(node.Key);
                    }

                }
            }

            SelectedValues = string.Join(',',selectedValueLst);
        }
        #endregion
    }
}
