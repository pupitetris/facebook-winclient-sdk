﻿namespace Facebook.Client.Controls
{
#if NETFX_CORE
    using System.ComponentModel;
    using Windows.UI.Xaml;
#endif
#if WINDOWS_PHONE || __MOBILE__
    using System.ComponentModel;
#endif
#if WINDOWS_PHONE
	using System.Windows;
#endif

#if !__MOBILE__
	internal class PickerItem<T> : DependencyObject, INotifyPropertyChanged
		where T : class
#else
	internal class PickerItem<T> : INotifyPropertyChanged
		where T : class
#endif
    {
        private object parent;

        internal PickerItem(object parent, T item)
        {
            this.parent = parent;
            this.item = item;
        }

        #region Properties

        #region Parent

        public object Parent
        {
            get
            {
                return this.parent;
            }
        }

        #endregion Parent

        #region Item

        private T item = null;

        public T Item
        {
            get
            {
                return this.item;
            }
        }

        #endregion Item

        #region IsSelected

        private bool isSelected = false;

        public bool IsSelected
        {
            get
            {
                return this.isSelected;
            }

            set
            {
                if (value != this.isSelected)
                {
                    this.isSelected = value;
                    this.NotifyPropertyChanged("IsSelected");
                }
            }
        }

        #endregion IsSelected

        #endregion Properties

        #region Implementation

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion Implementation
    }
}
