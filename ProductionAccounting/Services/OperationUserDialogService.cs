using ProductionAccounting.DAL.Entities;
using ProductionAccounting.Interfaces;
using ProductionAccounting.Models;
using ProductionAccounting.Services.Interfaces;
using ProductionAccounting.ViewModels;
using ProductionAccounting.Views.Windows;
using System;
using System.Windows;

namespace ProductionAccounting.Services
{
    public class OperationUserDialogService : IUserDialogWithRepository<OperationModel, OperationСoefficient>
    {
        public bool ConfirmOperation(string info, string caption)
        {
            return MessageBox.Show(info,
                                  caption,
                                  MessageBoxButton.YesNo,
                                  MessageBoxImage.Warning,
                                  MessageBoxResult.Yes) == MessageBoxResult.Yes;
        }

        public bool Edit(OperationModel model, IRepository<OperationСoefficient> repository)
        {
            try
            {
                var operationVM = new OperationEditorViewModel(model, repository);

                var operationV = new OperationEditorWindow()
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

                if (operationV.ShowDialog() != true) return false;
                model.Name = operationVM.Name;
                model.Coefficient = operationVM.Coefficient;
                model.Cost = operationVM.Cost;
                model.OperationDuration = operationVM.OperationDuration;
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
