using MathCore.WPF.ViewModels;
using System;

namespace ProductionAccounting.ViewModels
{
    public class PrintViewModel : ViewModel
    {
        private Int32 _width;
        public Int32 Width
        {
            get { return _width; }
            set
            {
                Set(ref _width, value);
            }
        }
    }
}
