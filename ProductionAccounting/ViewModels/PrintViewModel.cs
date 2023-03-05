using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using ProductionAccounting.Models;
using System.Collections.Generic;
using System.Windows.Input;
using ProductionAccounting.Services;
using System.Diagnostics;
using System.Linq;

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

        private decimal _productPrice;
        public decimal ProductPrice
        {
            get { return _productPrice; }
            private set
            {
                Set(ref _productPrice, value);
            }
        }

        public TabelModel TabelModel { get; private set; }

        public PrintViewModel(TabelModel model)
        {
            TabelModel = model;
            Operations = new List<OperationModel>(model.Operations);
            Name = model.Name;
            ProductPrice = Operations.Select(i => i.Price).Sum();
        }

        #region Команда вывода на печать
        private ICommand _toPrint;

        public ICommand ToPrint => _toPrint ??= new LambdaCommand(ToPrintExecuted, ToPrintExecute);

        private bool ToPrintExecute() => true;

        private void ToPrintExecuted()
        {
            string path;
            try
            {
                using (var pdfCreator = new PdfCreator(TabelModel))
                {
                    path = pdfCreator.CreatePdf();
                }
            }
            catch (System.Exception)
            {
                throw;
            }
            ProcessStartInfo startInfo = new ProcessStartInfo(path)
            {
                UseShellExecute = true,
            };
            Process.Start(startInfo);
        }
        #endregion
    }
}
