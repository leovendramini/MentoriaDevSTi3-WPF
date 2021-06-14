using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MentoriaSTI3.ViewModel
{
        public class PropertyChange : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            protected void OnPropertyChanged(String name)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }

    
}
