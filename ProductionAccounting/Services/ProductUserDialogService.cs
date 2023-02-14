using ProductionAccounting.DAL.Entities;
using ProductionAccounting.Interfaces;
using ProductionAccounting.Models;
using ProductionAccounting.Services.Interfaces;
using ProductionAccounting.ViewModels;
using ProductionAccounting.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProductionAccounting.Services
{
    public class ProductUserDialogService : IUserDialogWithRepository<ProductModel, Operation>
    {
        public bool ConfirmOperation(string info, string caption)
        {
            return MessageBox.Show(info,
                                  caption,
                                  MessageBoxButton.YesNo,
                                  MessageBoxImage.Warning,
                                  MessageBoxResult.Yes) == MessageBoxResult.Yes;
        }

        public bool Edit(ProductModel model, IRepository<Operation> repository)
        {
            try
            {
                var productVM = new ProductEditorViewModel(repository, model);

                var productV = new ProductEditorWindow()
                {
                    DataContext = productVM,
                };

                Func<bool> errorMessage = new Func<bool>(() =>
                {
                    MessageBox.Show("Неправильный формат коэффициента. Необходимо ввести одно число с дробной частью через точку",
                                    "Ошибка",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error,
                                    MessageBoxResult.Cancel);
                    return false;
                });

                if (productV.ShowDialog() != true) return false;
                model.Name = productVM.Name;
                model.Operations = productVM.SelectedOperations;
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
