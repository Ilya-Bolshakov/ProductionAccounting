using MathCore.WPF.ViewModels;
using ProductionAccounting.DAL.Entities;
using ProductionAccounting.Interfaces;
using ProductionAccounting.Models;

namespace ProductionAccounting.ViewModels
{
    public class OperationEditorViewModel : ViewModel
    {
        public OperationModel Operation { get; set; }
        public OperationEditorViewModel(OperationModel operation)
        {

        }
    }
}
