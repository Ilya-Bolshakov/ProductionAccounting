using ProductionAccounting.Models;
using ProductionAccounting.Services.Interfaces;
using ProductionAccounting.ViewModels;
using ProductionAccounting.Views.Windows;
using System.Windows;

namespace ProductionAccounting.Services
{
    public class PrintDialog : IUserPrintDialog
    {
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
            catch (System.Exception)
            {
                MessageBox.Show("Ошибка при работе программы, попробуйте еще раз",
                                  "Ошибка",
                                  MessageBoxButton.OK,
                                  MessageBoxImage.Error,
                                  MessageBoxResult.Yes);
                return false;
            }
            
        }
    }
}
