using MathCore.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionAccounting.Services.Interfaces
{
    public interface IViewModelFactory
    {
        public ViewModel CreateViewModel(int viewModelCode);
    }
}
