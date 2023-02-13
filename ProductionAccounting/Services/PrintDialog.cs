using ProductionAccounting.Models;
using ProductionAccounting.Services.Interfaces;
using ProductionAccounting.ViewModels;
using ProductionAccounting.Views.Windows;

namespace ProductionAccounting.Services
{
    public class PrintDialog : IUserPrintDialog
    {
        public bool ShowPrintDialog(TabelModel model)
        {
            var printDialogViewModel = new PrintViewModel();
            //printDialogViewModel.Width = 100;
            var printDialog = new PrintWindow()
            {
                DataContext = printDialogViewModel
            };
            printDialog.ShowDialog();
            return true;
        }
    }
}
