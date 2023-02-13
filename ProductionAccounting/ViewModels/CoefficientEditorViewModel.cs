using MathCore.WPF.ViewModels;
using ProductionAccounting.Models;

namespace ProductionAccounting.ViewModels
{
    public class CoefficientEditorViewModel : ViewModel
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                Set(ref _name, value);
            }
        }

        private decimal _coefficientValue;
        public decimal CoefficientValue
        {
            get { return _coefficientValue; }
            set
            {
                Set(ref _coefficientValue, value);
            }
        }

        public CoefficientEditorViewModel()
        {

        }

        public CoefficientEditorViewModel(CoefficientModel coefficientModel)
        {
            Name = coefficientModel.Name;
            CoefficientValue = coefficientModel.CoefficientValue;
        }


    }
}
