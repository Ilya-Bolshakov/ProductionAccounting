using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using ProductionAccounting.DAL.Entities;
using ProductionAccounting.Interfaces;
using ProductionAccounting.Models;
using ProductionAccounting.Services;
using ProductionAccounting.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace ProductionAccounting.ViewModels
{
    public class ProductsViewModel : ViewModel
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Operation> _operationRepository;
        private readonly IUserDialogWithRepository<ProductModel, Operation> _userDialogWithRepo;
        private readonly IUserPrintDialog _printDialog;
        private readonly IChangeSaveFolderService _changeSaveFolderService;
        private readonly IShowExceptionDialogService _showExceptionDialog;
        private readonly AppStateService _appStateService;


        private ObservableCollection<ProductModel> _products;

        public ObservableCollection<ProductModel> Products
        {
            get { return _products; }
            private set
            {
                Set(ref _products, value);
            }
        }

        private ICollectionView _productsView;
        public ICollectionView ProductsView
        {
            get { return _productsView; }
            set
            {
                Set(ref _productsView, value);
            }
        }

        private bool _onLoading;
        public bool OnLoading
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
                _appStateService.SelectedProductModel = value;
            }
        }
        #region Команда загрузки изделий
        private ICommand _getProducts;

        public ICommand GetProducts => _getProducts ??= new LambdaCommand(GetProductsExecuted, GetProductsExecute);

        private bool GetProductsExecute() => true;

        private async void GetProductsExecuted()
        {
            OnLoading = true;
            try
            {
                var task = Task.Run(() =>
                {
                    return _productRepository.Items.ToList().Select(e => new ProductModel(e));
                });
                var products = await task;
                Products = products.ToObservableCollection();
                ProductsView = CollectionViewSource.GetDefaultView(Products);
                ProductsView.Filter += _dataView_Filter;
            }
            catch (System.Exception ex)
            {
                _showExceptionDialog.ShowDialog("В работе приложения произошла ошибка. Попробуйте еще раз.\nПоказать сообщения для разработчика?", ex.Message);
            }
            finally
            {
                OnLoading = false;
            }            
        }
        #endregion


        #region Команда добавления изделия
        private ICommand _addProducts;

        public ICommand AddProducts => _addProducts ??= new LambdaCommand(AddProductsExecuted, AddProductsExecute);

        private bool AddProductsExecute() => !OnLoading;

        private async void AddProductsExecuted()
        {
            try
            {
                ProductModel product = new();
                if (!_userDialogWithRepo.Edit(product, _operationRepository))
                {
                    return;
                }
                Products.Add(product);
                var productDb = new Product();
                productDb.Name = product.Name;
                productDb.Operations = new List<Operation>();
                foreach (var item in product.Operations)
                {
                    var operDb = _operationRepository.GetById(item.Id);
                    productDb.Operations.Add(operDb);
                }
                _productRepository.Add(productDb);
                await _productRepository.SaveChangesAsync();
                product.Id = productDb.Id;
                SelectedItem = product;
            }
            catch (System.Exception ex)
            {
                _showExceptionDialog.ShowDialog("В работе приложения произошла ошибка. Попробуйте еще раз.\nПоказать сообщения для разработчика?", ex.Message);
            }
        }
        #endregion

        #region Команда редактирования изделия
        private ICommand _editProducts;

        public ICommand EditProducts => _editProducts ??= new LambdaCommand(EditProductsExecuted, EditProductsExecute);

        private bool EditProductsExecute() => SelectedItem != null;

        private async void EditProductsExecuted()
        {
            try
            {
                if (!_userDialogWithRepo.Edit(SelectedItem, _operationRepository))
                {
                    return;
                }
                var updatep = _productRepository.GetById(SelectedItem.Id);
                updatep.Name = SelectedItem.Name;
                updatep.Operations = new List<Operation>();
                foreach (var item in SelectedItem.Operations)
                {
                    var operDb = _operationRepository.GetById(item.Id);
                    updatep.Operations.Add(operDb);
                }
                _productRepository.Update(updatep);
                await _productRepository.SaveChangesAsync();
            }
            catch (System.Exception ex)
            {
                _showExceptionDialog.ShowDialog("В работе приложения произошла ошибка. Попробуйте еще раз.\nПоказать сообщения для разработчика?", ex.Message);
            }
        }
        #endregion

        #region Команда удаления изделия
        private ICommand _deleteProducts;

        public ICommand DeleteProducts => _deleteProducts ??= new LambdaCommand(DeleteProductsExecuted, DeleteProductsExecute);

        private bool DeleteProductsExecute() => SelectedItem != null;

        private async void DeleteProductsExecuted()
        {
            try
            {
                var removeModel = SelectedItem;
                if (!_userDialogWithRepo.ConfirmOperation("Вы действительно хотите удалить этого сотрудника?", "Удаление сотрудника")) return;
                Products.Remove(removeModel);
                await _productRepository.DeleteAsync(removeModel.Id);
                await _productRepository.SaveChangesAsync();
                if (ReferenceEquals(SelectedItem, removeModel)) SelectedItem = null;
            }
            catch (System.Exception ex)
            {
                _showExceptionDialog.ShowDialog("В работе приложения произошла ошибка. Попробуйте еще раз.\nПоказать сообщения для разработчика?", ex.Message);
            }
        }
        #endregion

        #region Команда печати
        private ICommand _print;

        public ICommand Print => _print ??= new LambdaCommand(PrintExecuted, PrintExecute);

        private bool PrintExecute() => SelectedItem != null;

        private void PrintExecuted()
        {
            TabelModel model = new(SelectedItem);
            _printDialog.ShowPrintDialog(model);
        }
        #endregion

        #region Команда изменения папки
        private ICommand _editFolder;

        public ICommand EditFolder => _editFolder ??= new LambdaCommand(EditFolderExecuted, EditFolderExecute);

        private bool EditFolderExecute() => true;

        private void EditFolderExecuted()
        {
            ChangeSaveFolderService service = new ChangeSaveFolderService();
            if (service.ShowDialog())
            {
                service.EditFolder();
            }
        }
        #endregion

        public ProductsViewModel(IRepository<Operation> operationRepository, IRepository<Product> productRepository,
            IUserDialogWithRepository<ProductModel, Operation> userDialog, IUserPrintDialog printDialog, IChangeSaveFolderService changeSaveFolderService, IShowExceptionDialogService showExceptionDialog,
            AppStateService appStateService)
        {
            _operationRepository = operationRepository;
            _productRepository = productRepository;
            _userDialogWithRepo = userDialog;
            _printDialog = printDialog;
            _changeSaveFolderService = changeSaveFolderService;
            _showExceptionDialog = showExceptionDialog;
            _appStateService = appStateService;
        }

        private string _nameFilter;
        public string NameFilter
        {
            get { return _nameFilter; }
            set
            {
                Set(ref _nameFilter, value);
                ProductsView.Refresh();
            }
        }

        private bool _dataView_Filter(object sender)
        {
            if (sender is ProductModel model)
            {
                return model.Name.Contains(NameFilter ?? String.Empty, StringComparison.OrdinalIgnoreCase)
                ;
            }
            return false;
        }
    }
}
