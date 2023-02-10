using MathCore.WPF.Commands;
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

        public CoefficientViewModel()
        {}

        public CoefficientViewModel(IRepository<OperationСoefficient> coeff, IUserDialog<CoefficientModel> userDialog)
        {
            _coefficientRepository = coeff;
            _userDialog = userDialog;
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

        private Visibility _onLoading;
        public Visibility OnLoading
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

        private bool GetCoeffsViewCommandExecute() => true;

        private async void GetCoeffsViewCommandExecuted()
        {
            OnLoading = Visibility.Visible;
            var task = Task.Run(() =>
            {
                return _coefficientRepository.Items.ToList().Select(e => new CoefficientModel(e));
            });
            var coefficients = await task;
            Coefficients = coefficients.ToObservableCollection();
            OnLoading = Visibility.Hidden;
        }
        #endregion
    }
}
