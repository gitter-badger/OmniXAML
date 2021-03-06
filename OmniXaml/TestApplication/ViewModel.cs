﻿namespace TestApplication
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using OmniXaml.Tests.Classes;
    using Properties;

    public abstract class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}