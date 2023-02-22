using ProductionAccounting.Models;
using ProductionAccounting.Services.Interfaces;
using ProductionAccounting.ViewModels;
using ProductionAccounting.Views.Windows;
using System.Windows;

namespace ProductionAccounting.Services
{
    public class PrintDialog : IUserPrintDialog
    {
        private readonly IShowExceptionDialogService _showExceptionDialogService;
        public PrintDialog(IShowExceptionDialogService showExceptionDialogService)
        {
            _showExceptionDialogService = showExceptionDialogService;
        }
        public bool ShowPrintDialog(TabelModel model)
        {
            try
            {
                var printDialogViewModel = new PrintViewModel(model);
                var printDialog = new PrintWindow()
                {
                    DataContext = printDialogViewModel
                };
                printDialog.ShowDialog();
                return true;
            }
            catch (System.Exception ex)
            {
                _showExceptionDialogService.ShowDialog("Ошибка при работе программы, попробуйте еще раз. Показать ошибку для разработчика?", ex.Message);
                return false;
            }
            
        }
    }
}
