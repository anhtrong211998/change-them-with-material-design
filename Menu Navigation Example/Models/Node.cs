using System.ComponentModel;

namespace Menu_Navigation_Example.Models
{
    public class Node : INotifyPropertyChanged
    {
        #region ctor
        public Node(string? title)
        {

            Title = title;
        }

        public Node(string? key, string? title)
        {
            Title = title;
            Key = key;
        }
        #endregion

        #region Properties

        private string? _key;
        public string? Key
        {
            get
            {
                return _key;
            }
            set
            {
                _key = value;
                NotifyPropertyChanged("Key");
            }
        }

        private string? _title;
        public string? Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                NotifyPropertyChanged("Title");
            }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                _isSelected = value;
                NotifyPropertyChanged("IsSelected");
            }
        }
        #endregion

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
