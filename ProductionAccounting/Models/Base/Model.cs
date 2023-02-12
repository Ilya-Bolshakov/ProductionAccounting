using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProductionAccounting.Models.Base
{
    public abstract class Model : ICloneable, INotifyPropertyChanged
    {
        public int Id { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public abstract object Clone();
    }
}
