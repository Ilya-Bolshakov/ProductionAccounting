using ProductionAccounting.Models;
using ProductionAccounting.Services.Interfaces;
using ProductionAccounting.ViewModels;
using ProductionAccounting.Views.Windows;
using System;
using System.Text.RegularExpressions;
using System.Windows;

namespace ProductionAccounting.Services
{
    public class OperationUserDialogService : IUserDialog<OperationModel>
    {
        public bool ConfirmOperation(string info, string caption)
        {
            return MessageBox.Show(info,
                                  caption,
                                  MessageBoxButton.YesNo,
                                  MessageBoxImage.Warning,
                                  MessageBoxResult.Yes) == MessageBoxResult.Yes;
        }

        public bool Edit(OperationModel model)
        {
            var operationVM = new OperationEditorViewModel(model);

            var coeffV = new OperationEditorWindow()
            {
                DataContext = operationVM,
            };

            Func<bool> errorMessage = new Func<bool>(() =>
            {
                MessageBox.Show("Неправильный формат коэффициента. Необходимо ввести одно число с дробной частью через запятую",
                                "Ошибка",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error,
                                MessageBoxResult.Cancel);
                return false;
            });

            //if (coeffV.ShowDialog() != true) return false;
            //var regex = new Regex(@"\b\d+\.\d{2}\b");
            //var coeffValueString = operationVM.CoefficientValue.ToString();
            //var matches = regex.Matches(coeffValueString);
            //if (matches.Count != 1)
            //{
            //    return errorMessage();
            //}
            //else
            //{
            //    if (!String.IsNullOrEmpty(coeffValueString.Replace(matches.First().ToString(), ""))) return errorMessage();
            //}
            //model.Name = operationVM.Name;
            //model.CoefficientValue = operationVM.CoefficientValue;
            return true;
        }
    }
}
