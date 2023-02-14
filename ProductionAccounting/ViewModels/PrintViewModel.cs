using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using ProductionAccounting.Models;
using System.Collections.Generic;
using System.Windows.Input;
using ProductionAccounting.Services;

namespace ProductionAccounting.ViewModels
{
    public class PrintViewModel : ViewModel
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

        private List<OperationModel> _operations;
        public List<OperationModel> Operations
        {
            get { return _operations; }
            set
            {
                Set(ref _operations, value);
            }
        }

        private OperationModel _selectedItem;
        public OperationModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                Set(ref _selectedItem, value);
            }
        }

        public PrintViewModel(TabelModel model)
        {
            Operations = new List<OperationModel>(model.Operations);
            Name = model.Name;
        }

        #region Команда вывода на печать
        private ICommand _toPrint;

        public ICommand ToPrint => _toPrint ??= new LambdaCommand(ToPrintExecuted, ToPrintExecute);

        private bool ToPrintExecute() => true;

        private void ToPrintExecuted()
        {
            using (var pdfCreator = new PdfCreator(Name))
            {
                pdfCreator.CreatePdf(Operations);
            }
        }
        #endregion
    }
}
