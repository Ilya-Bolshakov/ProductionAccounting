using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using ProductionAccounting.DAL.Entities;
using ProductionAccounting.Interfaces;
using ProductionAccounting.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Linq;

namespace ProductionAccounting.ViewModels
{
    public class OperationEditorViewModel : ViewModel
    {
        private readonly IRepository<OperationСoefficient> _repository;

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                Set(ref _name, value);
            }
        }

        private int _operationDuration;
        public int OperationDuration
        {
            get { return _operationDuration; }
            set
            {
                Set(ref _operationDuration, value);
            }
        }

        private decimal _cost;
        public decimal Cost
        {
            get { return _cost; }
            set
            {
                Set(ref _cost, value);
            }
        }

        private CoefficientModel _coefficient;
        public CoefficientModel Coefficient
        {
            get { return _coefficient; }
            set
            {
                Set(ref _coefficient, value);
            }
        }

        private ObservableCollection<CoefficientModel> _coefficients;
        public ObservableCollection<CoefficientModel> Coefficients
        {
            get { return _coefficients; }
            set
            {
                Set(ref _coefficients, value);
            }
        }

        #region Команда загрузки коэффов
        private ICommand _getCoefficient;

        public ICommand GetCoefficient => _getCoefficient ??= new LambdaCommand(GetCoefficientsExecuted, GetCoefficientsExecute);

        private bool GetCoefficientsExecute() => true;

        private async void GetCoefficientsExecuted()
        {
            var task = Task.Run(() =>
            {
                return _repository.Items.ToList().Select(e => new CoefficientModel(e));
            });
            var coeffs = await task;
            Coefficients = coeffs.ToObservableCollection();
        }
        #endregion

        public OperationEditorViewModel(OperationModel operation, IRepository<OperationСoefficient> repository)
        {
            Name = operation.Name;
            Cost = operation.Cost;
            OperationDuration = operation.OperationDuration;
            Coefficient = (CoefficientModel)operation.Coefficient?.Clone() ?? new CoefficientModel();
            _repository = repository;
        }

        public OperationEditorViewModel()
        { }
    }
}
