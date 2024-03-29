﻿using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using ProductionAccounting.DAL.Entities;
using ProductionAccounting.Interfaces;
using ProductionAccounting.Models;
using ProductionAccounting.Services.Interfaces;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProductionAccounting.ViewModels
{
    public class CoefficientViewModel : ViewModel
    {
        private readonly IRepository<OperationСoefficient> _coefficientRepository;
        private readonly IUserDialog<CoefficientModel> _userDialog;
        private readonly IShowExceptionDialogService _showExceptionDialog;

        public CoefficientViewModel()
        {}

        public CoefficientViewModel(IRepository<OperationСoefficient> coeff, IUserDialog<CoefficientModel> userDialog, IShowExceptionDialogService showExceptionDialog)
        {
            _coefficientRepository = coeff;
            _userDialog = userDialog;
            _showExceptionDialog = showExceptionDialog;
        }

        private ObservableCollection<CoefficientModel> _coefficients;

        public ObservableCollection<CoefficientModel> Coefficients
        {
            get { return _coefficients; }
            private set
            {
                Set(ref _coefficients, value);
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

        private CoefficientModel _selectedItem;
        public CoefficientModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                Set(ref _selectedItem, value);
            }
        }

        #region Команда загрузки коэффициентов
        private ICommand _getCoeffsViewCommand;

        public ICommand GetCoeffsViewCommand => _getCoeffsViewCommand ??= new LambdaCommand(GetCoeffsViewCommandExecuted, GetCoeffsViewCommandExecute);

        private bool GetCoeffsViewCommandExecute() => !OnLoading;

        private async void GetCoeffsViewCommandExecuted()
        {
            OnLoading = true;
            try
            {
                var task = Task.Run(() =>
                {
                    return _coefficientRepository.Items.ToList().Select(e => new CoefficientModel(e));
                });
                var coefficients = await task;
                Coefficients = coefficients.ToObservableCollection();
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

        #region Команда добавления коэффициента
        private ICommand _addCoeffsViewCommand;

        public ICommand AddCoeffsViewCommand => _addCoeffsViewCommand ??= new LambdaCommand(AddCoeffsViewCommandExecuted, AddCoeffsViewCommandExecute);

        private bool AddCoeffsViewCommandExecute() => true;

        private async void AddCoeffsViewCommandExecuted()
        {
            try
            {
                CoefficientModel coeff = new();
                if (!_userDialog.Edit(coeff))
                {
                    return;
                }
                Coefficients.Add(coeff);
                var entity = coeff.MapToOrm();
                _coefficientRepository.Add(entity);
                await _coefficientRepository.SaveChangesAsync();
                coeff.Id = entity.Id;
                SelectedItem = coeff;
            }
            catch (System.Exception ex)
            {
                _showExceptionDialog.ShowDialog("В работе приложения произошла ошибка. Попробуйте еще раз.\nПоказать сообщения для разработчика?", ex.Message);
            }
        }
        #endregion

        #region Команда редактирования коэффициента
        private ICommand _editCoeffsViewCommand;

        public ICommand EditCoeffsViewCommand => _editCoeffsViewCommand ??= new LambdaCommand(EditCoeffsViewCommandExecuted, EditCoeffsViewCommandExecute);

        private bool EditCoeffsViewCommandExecute() => SelectedItem != null;

        private async void EditCoeffsViewCommandExecuted()
        {
            try
            {
                if (!_userDialog.Edit(SelectedItem))
                {
                    return;
                }
                var updateC = _coefficientRepository.GetById(SelectedItem.Id);
                updateC.Name = SelectedItem.Name;
                updateC.CoefficientValue = SelectedItem.CoefficientValue;
                _coefficientRepository.Update(updateC);
                await _coefficientRepository.SaveChangesAsync();
            }
            catch (System.Exception ex)
            {
                _showExceptionDialog.ShowDialog("В работе приложения произошла ошибка. Попробуйте еще раз.\nПоказать сообщения для разработчика?", ex.Message);
            }
        }
        #endregion

        #region Команда удаления сотрудников
        private ICommand _deleteCoeffsViewCommand;

        public ICommand DeleteCoeffsViewCommand => _deleteCoeffsViewCommand ??= new LambdaCommand(DeleteCoeffsViewCommandExecuted, DeleteCoeffsViewCommandExecute);

        private bool DeleteCoeffsViewCommandExecute() => SelectedItem != null;

        private async void DeleteCoeffsViewCommandExecuted()
        {
            try
            {
                var removeModel = SelectedItem;
                if (!_userDialog.ConfirmOperation("Вы действительно хотите удалить это значение коэффициента?", "Удаление элемента")) return;
                Coefficients.Remove(removeModel);
                await _coefficientRepository.DeleteAsync(removeModel.Id);
                await _coefficientRepository.SaveChangesAsync();
                if (ReferenceEquals(SelectedItem, removeModel)) SelectedItem = null;
            }
            catch (System.Exception ex)
            {
                _showExceptionDialog.ShowDialog("В работе приложения произошла ошибка. Попробуйте еще раз.\nПоказать сообщения для разработчика?", ex.Message);
            }
        }
        #endregion
    }
}
