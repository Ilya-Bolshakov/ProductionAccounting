using MathCore.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionAccounting.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        private string? _title = "Test string";

        public string? Title
        {
            get { return _title; }
            set { Set(ref _title, value); }
        }

    }
}
