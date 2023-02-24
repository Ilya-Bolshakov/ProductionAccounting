using ProductionAccounting.Models;
using ProductionAccounting.Services.Interfaces;
using ProductionAccounting.ViewModels;
using ProductionAccounting.Views.Windows;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace ProductionAccounting.Services
{
    public class CoefficientUserDialogService : IUserDialog<CoefficientModel>
    {
        private readonly IShowExceptionDialogService _showExceptionDialogService;
        public CoefficientUserDialogService(IShowExceptionDialogService showExceptionDialogService)
        {
            _showExceptionDialogService = showExceptionDialogService;
        }
        public bool ConfirmOperation(string info, string caption)
        {
            return MessageBox.Show(info,
                                  caption,
                                  MessageBoxButton.YesNo,
                                  MessageBoxImage.Warning,
                                  MessageBoxResult.Yes) == MessageBoxResult.Yes;
        }

        public bool Edit(CoefficientModel model)
        {
            try
            {
                var coeffVM = new CoefficientEditorViewModel(model);

                var coeffV = new CoefficientEditorWindow()
                {
                    DataContext = coeffVM,
                };

                if (coeffV.ShowDialog() != true) return false;

                model.CoefficientValue = coeffVM.CoefficientValue;
                model.Name = coeffVM.Name;
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
