﻿using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using ProductionAccounting.DAL.Entities;
using ProductionAccounting.Interfaces;
using ProductionAccounting.Models;
using ProductionAccounting.Services.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProductionAccounting.ViewModels
{
    public class ProductsViewModel : ViewModel
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Operation> _operationRepository;
        private readonly IUserDialogWithRepository<ProductModel, Operation> _userDialogWithRepo;


        private ObservableCollection<ProductModel> _products;

        public ObservableCollection<ProductModel> Products
        {
            get { return _products; }
            private set
            {
                Set(ref _products, value);
            }
        }

        private Visibility _onLoading;
        public Visibility OnLoading
        {
            get { return _onLoading; }
            private set
            {
                Set(ref _onLoading, value);
            }
        }

        private ProductModel _selectedItem;
        public ProductModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                Set(ref _selectedItem, value);
            }
        }
        #region Команда загрузки изделий
        private ICommand _getProducts;

        public ICommand GetProducts => _getProducts ??= new LambdaCommand(GetProductsExecuted, GetProductsExecute);

        private bool GetProductsExecute() => true;

        private async void GetProductsExecuted()
        {
            OnLoading = Visibility.Visible;
            var task = Task.Run(() =>
            {
                return _productRepository.Items.ToList().Select(e => new ProductModel(e));
            });
            var products = await task;
            Products = products.ToObservableCollection();
            OnLoading = Visibility.Hidden;
        }
        #endregion

        #region Команда добавления изделия
        private ICommand _addProducts;

        public ICommand AddProducts => _addProducts ??= new LambdaCommand(AddProductsExecuted, AddProductsExecute);

        private bool AddProductsExecute() => true;

        private async void AddProductsExecuted()
        {
            ProductModel product = new();
            if (!_userDialogWithRepo.Edit(product, _operationRepository))
            {
                return;
            }
            Products.Add(product);
            _productRepository.Add(product.MapToOrm());
            await _operationRepository.SaveChangesAsync();

            SelectedItem = product;

        }
        #endregion

        #region Команда редактирования изделия
        private ICommand _editProducts;

        public ICommand EditProducts => _editProducts ??= new LambdaCommand(EditProductsExecuted, EditProductsExecute);

        private bool EditProductsExecute() => SelectedItem != null;

        private async void EditProductsExecuted()
        {
            if (!_userDialogWithRepo.Edit(SelectedItem, _operationRepository))
            {
                return;
            }
            var updatep = _productRepository.GetById(SelectedItem.Id);
            updatep.Name = SelectedItem.Name;
            updatep.Operations = SelectedItem.Operations.Select(o => o.MapToOrm()).ToList();
            _productRepository.Update(updatep);
            await _productRepository.SaveChangesAsync();
        }
        #endregion

        #region Команда удаления изделия
        private ICommand _deleteProducts;

        public ICommand DeleteProducts => _deleteProducts ??= new LambdaCommand(DeleteProductsExecuted, DeleteProductsExecute);

        private bool DeleteProductsExecute() => SelectedItem != null;

        private async void DeleteProductsExecuted()
        {
            var removeModel = SelectedItem;
            if (!_userDialogWithRepo.ConfirmOperation("Вы действительно хотите удалить этого сотрудника?", "Удаление сотрудника")) return;
            Products.Remove(removeModel);
            await _productRepository.DeleteAsync(removeModel.Id);
            await _productRepository.SaveChangesAsync();
            if (ReferenceEquals(SelectedItem, removeModel)) SelectedItem = null;
        }
        #endregion


        public ProductsViewModel(IRepository<Operation> operationRepository, IRepository<Product> productRepository,
            IUserDialogWithRepository<ProductModel, Operation> userDialog)
        {
            _operationRepository = operationRepository;
            _productRepository = productRepository;
            _userDialogWithRepo = userDialog;
        }
    }
}
